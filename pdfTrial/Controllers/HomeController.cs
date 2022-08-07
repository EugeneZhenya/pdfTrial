using iText.Forms;
using iText.Forms.Fields;
using iText.Html2pdf;
using iText.Kernel.Colors;
using iText.Kernel.Exceptions;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Data;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Layout;
using iText.Layout.Element;
using iText.Signatures;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.X509;
using pdfTrial.AdobeSign;
using pdfTrial.Helpers;
using pdfTrial.Models;
using RazorEngine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace pdfTrial.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

            MerchantAgreement test = new MerchantAgreement();
            string DisplayName = MetaDataHelper.GetDisplayName<MerchantAgreement>(t => t.MerchantsLegalName);

            return View();
        }

        public ActionResult W9()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

            return View();
        }

        [HttpPost]
        public ActionResult MerchantAgreement(FormCollection form)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

            MerchantAgreement newAgreement = new MerchantAgreement();

            bool isWhiteLabeled = form["IsWhiteLabeled"].ToLower() == "true,false";
            bool isVirginia = form["IsVirginia"].ToLower() == "true,false";
            var agreementType = form["MerchantAgreementTypes"];
            bool fourOwnersAndMerchants = form["FourOwnersAndMerchants"].ToLower() == "true,false";

            string templateName = MetaDataHelper.GetDescriptionFromEnum((MerchantAgreementTypes)Convert.ToInt16(agreementType));
            if (fourOwnersAndMerchants)
            {
                templateName = templateName + "_4";
            }
            string src = System.IO.Path.Combine(Server.MapPath("~/UploadedFiles"), "PIRS Capital - " + templateName + ".pdf");
            if (isVirginia && isWhiteLabeled)
            {
                src = System.IO.Path.Combine(Server.MapPath("~/UploadedFiles/Virginia/whitelabeled"), "PIRS Capital - " + templateName + ".pdf");
            }
            if (isVirginia && !isWhiteLabeled)
            {
                src = System.IO.Path.Combine(Server.MapPath("~/UploadedFiles/Virginia"), "PIRS Capital - " + templateName + ".pdf");
            }
            if (!isVirginia && isWhiteLabeled)
            {
                src = System.IO.Path.Combine(Server.MapPath("~/UploadedFiles/whitelabeled"), "PIRS Capital - " + templateName + ".pdf");
            }

            newAgreement.TemplatePath = src;
            newAgreement.IsVirginia = isVirginia;
            newAgreement.IsWhiteLabeled = isWhiteLabeled;
            newAgreement.FourOwnersAndMerchants = fourOwnersAndMerchants;
            newAgreement.MerchantAgreementType = Convert.ToInt16(agreementType);
            newAgreement.AgreementDate = DateTime.Now.ToString("d");
            newAgreement.Date = newAgreement.AgreementDate;

            return View(newAgreement);
        }

        public ActionResult MerchantAgreementSubmit(MerchantAgreement agreement)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

            string src = agreement.TemplatePath;
            string destFileName = System.IO.Path.GetFileName(src) + "_Filled.pdf";
            string dest = System.IO.Path.Combine(Server.MapPath("~/Content"), destFileName);

            PdfReader reader = new PdfReader(src);
            reader.SetUnethicalReading(true);
            PdfDocument pdfDoc = new PdfDocument(reader, new PdfWriter(dest));

            PdfAcroForm form = PdfAcroForm.GetAcroForm(pdfDoc, true);
            IDictionary<String, PdfFormField> fields = form.GetFormFields();

            agreement.LegalName = agreement.MerchantsLegalName;
            agreement.MerchantAddress = agreement.PhysicalAddress + ", " + agreement.PhysicalAddressCity + ", " + agreement.PhysicalAddressState + " " + agreement.PhysicalAddressZip;
            agreement.AppendixA_MerchantsLegalName = agreement.MerchantsLegalName;
            agreement.AppendixA_DBA = agreement.DBA;
            agreement.AppendixA_PhysicalAddress = agreement.PhysicalAddress;
            agreement.AppendixA_PhysicalAddressCity = agreement.PhysicalAddressCity;
            agreement.AppendixA_PhysicalAddressState = agreement.PhysicalAddressState;
            agreement.AppendixA_PhysicalAddressZip = agreement.PhysicalAddressZip;
            agreement.Merchant1PrintNameAndTitle = agreement.MerchantName + " (" + agreement.Merchant1PrintNameAndTitle + ")";
            agreement.Merchant1PrintNameAndTitle_1 = agreement.Merchant1PrintNameAndTitle;
            agreement.Merchant2PrintNameAndTitle = agreement.MerchantName2 + " (" + agreement.Merchant2PrintNameAndTitle + ")";
            agreement.AppendixA_Merchant1PrintNameAndTitle = agreement.Merchant1PrintNameAndTitle;
            agreement.AppendixA_Merchant2PrintNameAndTitle = agreement.Merchant2PrintNameAndTitle;
            agreement.AppendixA_Owner1PrintName = agreement.Owner1PrintName;
            agreement.AppendixA_Owner2PrintName = agreement.Owner2PrintName;
            agreement.AppendixB_MerchantsLegalName = agreement.MerchantsLegalName;
            agreement.AppendixB_AgreementDate = agreement.AgreementDate;
            agreement.AppendixB_Date = agreement.AgreementDate;
            agreement.AppendixB_MerchantAddress = agreement.MerchantAddress;
            agreement.MerchantPrintName = agreement.MerchantName;
            agreement.PrintedName = agreement.MerchantName;
            agreement.Date2 = agreement.AgreementDate;
            agreement.PirsCapitalLLC = agreement.CompanyOfficer;
            agreement.NameOfMerchant = agreement.MerchantsLegalName;
            agreement.MerchantBy = agreement.MerchantName;
            agreement.MerchantPrintName = agreement.MerchantName2;
            agreement.Owner1 = agreement.Owner1PrintName;
            agreement.Owner2 = agreement.Owner2PrintName;
            if (agreement.FourOwnersAndMerchants)
            {
                agreement.Merchant3PrintNameAndTitle = agreement.MerchantName3 + " (" + agreement.Merchant3PrintNameAndTitle + ")";
                agreement.Merchant4PrintNameAndTitle = agreement.MerchantName4 + " (" + agreement.Merchant4PrintNameAndTitle + ")";
                agreement.AppendixA_Merchant3PrintNameAndTitle = agreement.Merchant3PrintNameAndTitle;
                agreement.AppendixA_Merchant4PrintNameAndTitle = agreement.Merchant4PrintNameAndTitle;
                agreement.AppendixA_Owner3PrintName = agreement.Owner3PrintName;
                agreement.AppendixA_Owner4PrintName = agreement.Owner4PrintName;
                agreement.NameOfMerchant2 = agreement.Owner2PrintName;
                agreement.NameOfMerchant3 = agreement.Owner3PrintName;
            }
            if (agreement.IsWhiteLabeled)
            {
                agreement.Partner = agreement.WhiteLabelPartner;
                agreement.PartnerName = agreement.WhiteLabelPartner;
                agreement.AppendixB_PartnerName = agreement.WhiteLabelPartner;
                agreement.AppendixB_PartnerName_2 = agreement.WhiteLabelPartner;
            }

            foreach (var prop in agreement.GetType().GetProperties())
            {
                if (prop.GetValue(agreement, null) != null)
                {
                    var x = Expression.Parameter(typeof(MerchantAgreement), "x");
                    var body = Expression.PropertyOrField(x, prop.Name);
                    if (body.Type.Name == "String")
                    {
                        var lambda = Expression.Lambda<Func<MerchantAgreement, object>>(body, x);
                        string FieldName = MetaDataHelper.GetDisplayName(lambda);
                        var fieldToChange = form.GetField(FieldName);
                        if (fieldToChange != null)
                        {
                            fieldToChange.SetValue(prop.GetValue(agreement, null).ToString());
                        }
                    }
                    if (body.Type.Name == "Boolean")
                    {
                        if (prop.Name == "Corporation")
                        {
                            if (agreement.Corporation)
                            {
                                PdfFormField setCorporation;
                                string FieldName = MetaDataHelper.GetDisplayName<MerchantAgreement>(t => t.Corporation);
                                fields.TryGetValue(FieldName, out setCorporation);
                                setCorporation.SetCheckType(PdfFormField.TYPE_CHECK);
                                setCorporation.SetValue(PdfName.ON.ToString());
                            }
                        }
                        if (prop.Name == "LimitedLiabilityCompany")
                        {
                            if (agreement.LimitedLiabilityCompany)
                            {
                                PdfFormField setLLC;
                                string FieldName = MetaDataHelper.GetDisplayName<MerchantAgreement>(t => t.LimitedLiabilityCompany);
                                fields.TryGetValue(FieldName, out setLLC);
                                setLLC.SetCheckType(PdfFormField.TYPE_CHECK);
                                setLLC.SetValue(PdfName.ON.ToString());
                            }
                        }
                        if (prop.Name == "LimitedPartnership")
                        {
                            if (agreement.LimitedPartnership)
                            {
                                PdfFormField setLP;
                                string FieldName = MetaDataHelper.GetDisplayName<MerchantAgreement>(t => t.LimitedPartnership);
                                fields.TryGetValue(FieldName, out setLP);
                                setLP.SetCheckType(PdfFormField.TYPE_CHECK);
                                setLP.SetValue(PdfName.ON.ToString());
                            }
                        }
                        if (prop.Name == "LimitedLiabilityPartnership")
                        {
                            if (agreement.LimitedLiabilityPartnership)
                            {
                                PdfFormField setLLP;
                                string FieldName = MetaDataHelper.GetDisplayName<MerchantAgreement>(t => t.LimitedLiabilityPartnership);
                                fields.TryGetValue(FieldName, out setLLP);
                                setLLP.SetCheckType(PdfFormField.TYPE_CHECK);
                                setLLP.SetValue(PdfName.ON.ToString());
                            }
                        }
                        if (prop.Name == "SoleProprietor")
                        {
                            if (agreement.SoleProprietor)
                            {
                                PdfFormField setSP;
                                string FieldName = MetaDataHelper.GetDisplayName<MerchantAgreement>(t => t.SoleProprietor);
                                fields.TryGetValue(FieldName, out setSP);
                                setSP.SetCheckType(PdfFormField.TYPE_CHECK);
                                setSP.SetValue(PdfName.ON.ToString());
                            }
                        }
                    }
                }
            }

            pdfDoc.Close();

            agreement.TemplatePath = destFileName;

            return View(agreement);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult SubmitW9(W9Form formDatas)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

            string src = System.IO.Path.Combine(Server.MapPath("~/UploadedFiles"), "W-9 Form.pdf");
            string dest = System.IO.Path.Combine(Server.MapPath("~/Content"), "W-9 Form Filled.pdf");

            PdfReader reader = new PdfReader(src);
            reader.SetUnethicalReading(true);
            PdfDocument pdfDoc = new PdfDocument(reader, new PdfWriter(dest));

            PdfAcroForm form = PdfAcroForm.GetAcroForm(pdfDoc, true);
            form.RemoveXfaForm();
            IDictionary<String, PdfFormField> fields = form.GetFormFields();

            PdfFormField setName;
            PdfFormField setBusinessName;
            PdfFormField setIndividual;
            PdfFormField setCCorporation;
            PdfFormField setSCorporation;
            PdfFormField setPartnership;
            PdfFormField setTrustOrEstate;
            PdfFormField setLimited;
            PdfFormField setOther;
            PdfFormField setPayeeCode;
            PdfFormField setReportingCode;
            PdfFormField setAddress;
            PdfFormField setCityAndState;
            PdfFormField setAccountNumber;
            PdfFormField setRequesterInfo;
            PdfFormField setSocialNumberStart1;
            PdfFormField setSocialNumberStart2;
            PdfFormField setSocialNumberStart3;
            PdfFormField setSocialNumberMiddle1;
            PdfFormField setSocialNumberMiddle2;
            PdfFormField setSocialNumberEnd1;
            PdfFormField setSocialNumberEnd2;
            PdfFormField setSocialNumberEnd3;
            PdfFormField setSocialNumberEnd4;
            PdfFormField setEmployerIdStart1;
            PdfFormField setEmployerIdStart2;
            PdfFormField setEmployerIdEnd1;
            PdfFormField setEmployerIdEnd2;
            PdfFormField setEmployerIdEnd3;
            PdfFormField setEmployerIdEnd4;
            PdfFormField setEmployerIdEnd5;
            PdfFormField setEmployerIdEnd6;
            PdfFormField setEmployerIdEnd7;
            PdfFormField setSignDate;

            if (formDatas.Name != null)
            {
                fields.TryGetValue("Text1", out setName);
                setName.SetValue(formDatas.Name);
            }

            if (formDatas.BusinessName != null)
            {
                fields.TryGetValue("Text2", out setBusinessName);
                setBusinessName.SetValue(formDatas.BusinessName);
            }

            if (formDatas.Individual)
            {
                fields.TryGetValue("Check Box3", out setIndividual);
                setIndividual.SetCheckType(PdfFormField.TYPE_CHECK);
                setIndividual.SetValue(PdfName.ON.ToString());
            }

            if (formDatas.CCorporation)
            {
                fields.TryGetValue("Check Box4", out setCCorporation);
                setCCorporation.SetCheckType(PdfFormField.TYPE_CHECK);
                setCCorporation.SetValue(PdfName.ON.ToString());
            }

            if (formDatas.SCorporation)
            {
                fields.TryGetValue("Check Box5", out setSCorporation);
                setSCorporation.SetCheckType(PdfFormField.TYPE_CHECK);
                setSCorporation.SetValue(PdfName.ON.ToString());
            }

            if (formDatas.Partnership)
            {
                fields.TryGetValue("Check Box6", out setPartnership);
                setPartnership.SetCheckType(PdfFormField.TYPE_CHECK);
                setPartnership.SetValue(PdfName.ON.ToString());
            }

            if (formDatas.TrustOrEstate)
            {
                fields.TryGetValue("Check Box7", out setTrustOrEstate);
                setTrustOrEstate.SetCheckType(PdfFormField.TYPE_CHECK);
                setTrustOrEstate.SetValue(PdfName.ON.ToString());
            }

            if (formDatas.Limited)
            {
                fields.TryGetValue("Check Box8", out setLimited);
                setLimited.SetCheckType(PdfFormField.TYPE_CHECK);
                setLimited.SetValue(PdfName.ON.ToString());
            }

            if (formDatas.Other)
            {
                fields.TryGetValue("Check Box9", out setOther);
                setOther.SetCheckType(PdfFormField.TYPE_CHECK);
                setOther.SetValue(PdfName.ON.ToString());
            }

            if (formDatas.PayeeCode != null)
            {
                fields.TryGetValue("Text10", out setPayeeCode);
                setPayeeCode.SetValue(formDatas.PayeeCode);
            }

            if (formDatas.ReportingCode != null)
            {
                fields.TryGetValue("Text11", out setReportingCode);
                setReportingCode.SetValue(formDatas.ReportingCode);
            }

            if (formDatas.Address != null)
            {
                fields.TryGetValue("Text12", out setAddress);
                setAddress.SetValue(formDatas.Address);
            }

            if (formDatas.CityAndState != null)
            {
                fields.TryGetValue("Text13", out setCityAndState);
                setCityAndState.SetValue(formDatas.CityAndState);
            }

            if (formDatas.AccountNumber != null)
            {
                fields.TryGetValue("Text14", out setAccountNumber);
                setAccountNumber.SetValue(formDatas.AccountNumber);
            }

            if (formDatas.RequesterInfo != null)
            {
                fields.TryGetValue("Text15", out setRequesterInfo);
                setRequesterInfo.SetValue(formDatas.RequesterInfo);
            }

            if (formDatas.SocialNumberStart != null && formDatas.SocialNumberStart.Length == 3)
            {
                char[] chars = formDatas.SocialNumberStart.ToCharArray();
                fields.TryGetValue("Text16", out setSocialNumberStart1);
                setSocialNumberStart1.SetValue(chars[0].ToString());
                fields.TryGetValue("Text17", out setSocialNumberStart2);
                setSocialNumberStart2.SetValue(chars[1].ToString());
                fields.TryGetValue("Text18", out setSocialNumberStart3);
                setSocialNumberStart3.SetValue(chars[2].ToString());
            }

            if (formDatas.SocialNumberMiddle != null && formDatas.SocialNumberMiddle.Length == 2)
            {
                char[] chars = formDatas.SocialNumberMiddle.ToCharArray();
                fields.TryGetValue("Text19", out setSocialNumberMiddle1);
                setSocialNumberMiddle1.SetValue(chars[0].ToString());
                fields.TryGetValue("Text20", out setSocialNumberMiddle2);
                setSocialNumberMiddle2.SetValue(chars[1].ToString());
            }

            if (formDatas.SocialNumberEnd != null && formDatas.SocialNumberEnd.Length == 4)
            {
                char[] chars = formDatas.SocialNumberEnd.ToCharArray();
                fields.TryGetValue("Text21", out setSocialNumberEnd1);
                setSocialNumberEnd1.SetValue(chars[0].ToString());
                fields.TryGetValue("Text22", out setSocialNumberEnd2);
                setSocialNumberEnd2.SetValue(chars[1].ToString());
                fields.TryGetValue("Text23", out setSocialNumberEnd3);
                setSocialNumberEnd3.SetValue(chars[2].ToString());
                fields.TryGetValue("Text24", out setSocialNumberEnd4);
                setSocialNumberEnd4.SetValue(chars[3].ToString());
            }

            if (formDatas.EmployerIdStart != null && formDatas.EmployerIdStart.Length == 2)
            {
                char[] chars = formDatas.EmployerIdStart.ToCharArray();
                fields.TryGetValue("Text25", out setEmployerIdStart1);
                setEmployerIdStart1.SetValue(chars[0].ToString());
                fields.TryGetValue("Text26", out setEmployerIdStart2);
                setEmployerIdStart2.SetValue(chars[1].ToString());
            }

            if (formDatas.EmployerIdEnd != null && formDatas.EmployerIdEnd.Length == 7)
            {
                char[] chars = formDatas.EmployerIdEnd.ToCharArray();
                fields.TryGetValue("Text27", out setEmployerIdEnd1);
                setEmployerIdEnd1.SetValue(chars[0].ToString());
                fields.TryGetValue("Text28", out setEmployerIdEnd2);
                setEmployerIdEnd2.SetValue(chars[1].ToString());
                fields.TryGetValue("Text29", out setEmployerIdEnd3);
                setEmployerIdEnd3.SetValue(chars[2].ToString());
                fields.TryGetValue("Text30", out setEmployerIdEnd4);
                setEmployerIdEnd4.SetValue(chars[3].ToString());
                fields.TryGetValue("Text31", out setEmployerIdEnd5);
                setEmployerIdEnd5.SetValue(chars[4].ToString());
                fields.TryGetValue("Text32", out setEmployerIdEnd6);
                setEmployerIdEnd6.SetValue(chars[5].ToString());
                fields.TryGetValue("Text33", out setEmployerIdEnd7);
                setEmployerIdEnd7.SetValue(chars[6].ToString());
            }

            fields.TryGetValue("Date19_af_date", out setSignDate);
            setSignDate.SetValue(DateTime.Today.ToString("M/d/yy"));

            foreach (var fld in fields)
            {
                
            }

            pdfDoc.Close();

            return View();
        }

        public ActionResult SignPDF()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

            string KEYSTORE = System.IO.Path.Combine(Server.MapPath("~/UploadedFiles"), "cert.pfx");
            char[] PASSWORD = "Tiamat+".ToCharArray();
            Pkcs12Store pk12 = new Pkcs12Store(new FileStream(KEYSTORE, FileMode.Open, FileAccess.Read), PASSWORD);
            string alias = null;
            foreach (object a in pk12.Aliases)
            {
                alias = ((string)a);
                if (pk12.IsKeyEntry(alias))
                {
                    break;
                }
            }
            ICipherParameters pk = pk12.GetKey(alias).Key;

            X509CertificateEntry[] ce = pk12.GetCertificateChain(alias);
            X509Certificate[] chain = new X509Certificate[ce.Length];
            for (int k = 0; k < ce.Length; ++k)
            {
                chain[k] = ce[k].Certificate;
            }

            string DEST = System.IO.Path.Combine(Server.MapPath("~/Content"), "W-9 Form Signed.pdf");
            string SRC = System.IO.Path.Combine(Server.MapPath("~/Content"), "W-9 Form Filled.pdf");

            PdfReader reader = new PdfReader(SRC);
            PdfReader srcReader = new PdfReader(SRC);
            PdfSigner signer = new PdfSigner(reader, new FileStream(DEST, FileMode.Create), new StampingProperties());

            PdfDocument pdfDoc = new PdfDocument(srcReader);
            PdfAcroForm form = PdfAcroForm.GetAcroForm(pdfDoc, true);

            string fieldName = "";
            var allField = form.GetFormFields();
            foreach (var fild in allField)
            {
                if (fild.Key.Contains("Signature"))
                {
                    fieldName = fild.Key;
                    break;
                }
            }

            PdfSignatureAppearance appearance = signer.GetSignatureAppearance();
            // string fld = signer.GetFieldName();
            signer.SetFieldName(fieldName);
            // var position = form.GetField(fld).GetWidgets().First().GetRectangle().ToList();

            appearance.SetReason("Test of X509 Certificate...")
                .SetLocation("Dnipro");
                // .SetPageRect(new iText.Kernel.Geom.Rectangle((float)Convert.ToDouble(position[0].ToString()), (float)Convert.ToDouble(position[1].ToString()), (float)Convert.ToDouble(position[2].ToString()), (float)Convert.ToDouble(position[3].ToString())))
                // .SetPageNumber(1);

            IExternalSignature pks = new PrivateKeySignature(pk, DigestAlgorithms.SHA256);
            signer.SignDetached(pks, chain, null, null, null, 0, PdfSigner.CryptoStandard.CMS);

            return View();
        }

        public async Task<ActionResult> AdobeESign()
        {
            var ApiURL = "https://api.na3.echosign.com/api/rest/v5/";
            var accessToken = "3AAABLblqZhDN_PF5pPcKGMEOgPMrsrGiZNfd8_oMHMWC0NglRMdtqVUq5rO2nS1L3fKBv_ySkjiN9CjSPS5u48TSNNZ2b6aU";

            RestAPI api = new RestAPI(ApiURL, accessToken);
            AdobeObject obj = new AdobeObject(api);

            string filePath = System.IO.Path.Combine(Server.MapPath("~/Content"), "W-9 Form Filled.pdf");
            AgreementCreationResponse agreement = await SendDocument(ApiURL, accessToken, filePath, "W9 Form for mcdnu@ua.fm", "mcdnu@ua.fm");

            await Task.Delay(10000);
            SigningUrlSet redirectUrl = await obj.GetAgreementSigningUrls(agreement.agreementId);

            return Redirect(redirectUrl.signingUrlSetInfos[0].signingUrls[0].esignUrl);
        }

        public async Task<AgreementCreationResponse> SendDocument(string APIURL, string Access_Token, string fullFilePath, string agreementName, string recipientEmail)
        {
            try
            {
                //APIURL - FUll URL address, including "api/rest/v5/". e.g. https://api.eu1.echosign.com/api/rest/v5/                

                RestAPI api = new RestAPI(APIURL, Access_Token);
                AdobeObject obj = new AdobeObject(api);


                //Create trasient document
                var fileData = System.IO.File.ReadAllBytes(fullFilePath);
                var transientDocumentResponse = await obj.AddDocument("MyFileName", fileData);


                AgreementCreationInfo creationInfo = new AgreementCreationInfo();
                creationInfo.documentCreationInfo = new DocumentCreationInfo();

                //Document Creation Info
                var documentCreationInfo = creationInfo.documentCreationInfo;
                documentCreationInfo.name = agreementName;
                documentCreationInfo.signatureType = SignatureTypeEnum.ESIGN;
                documentCreationInfo.signatureFlow = "";

                //FileInfo
                documentCreationInfo.fileInfos = new List<AdobeSign.FileInfo>();
                var fileInfos = documentCreationInfo.fileInfos;
                AdobeSign.FileInfo fileInfo = new AdobeSign.FileInfo(transientDocumentResponse.transientDocumentId);
                fileInfos.Add(fileInfo);

                //RecipientSetInfo
                documentCreationInfo.recipientSetInfos = new List<RecipientSetInfo>();
                var recipientSetInfos = documentCreationInfo.recipientSetInfos;

                RecipientSetInfo recipientSetInfo = new RecipientSetInfo();
                recipientSetInfo.recipientSetRole = RecipientRoleEnum.SIGNER;
                recipientSetInfo.signingOrder = 1;

                //RecipientSetMemberInfo https://www.adobe.io/apis/documentcloud/sign/docs/step-by-step-guide/get-the-access-token.html
                RecipientSetMemberInfo setMemberInfo = new RecipientSetMemberInfo();
                setMemberInfo.email = recipientEmail;

                recipientSetInfo.recipientSetMemberInfos.Add(setMemberInfo);

                recipientSetInfos.Add(recipientSetInfo);

                var agreementCreationResponse = await obj.CreateAgreement(creationInfo);

                return agreementCreationResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        [Obsolete]
        public ActionResult Template()
        {
            dynamic expando = new ExpandoObject();
            var marksModel = expando as IDictionary<string, object>;
            marksModel.Add("FirstName", "User #" + Guid.NewGuid().ToString());
            marksModel.Add("PasswordLink", "http://klio.dp.ua/");

            var html = GetMailBody(marksModel);

            ConverterProperties converterProperties = new ConverterProperties();

            // Create a Temp PDF file temporary, remove this when the whole invoice is created to manaqge it as stream byte[]
            string file = System.IO.Path.Combine(Server.MapPath("~/Content"), "PDFfromCSHTML.pdf");

            PdfWriter writer = new PdfWriter(file);
            PdfDocument pdf = new PdfDocument(writer);
            pdf.SetDefaultPageSize(PageSize.LEGAL);
            var document = HtmlConverter.ConvertToDocument(html, pdf, converterProperties);
            document.Close();

            return View();
        }

        [Obsolete]
        private string GetMailBody(dynamic modelData)
        {
            string mailBody = null;

            var localUrl = System.IO.Path.Combine(Server.MapPath("~/Views"), "Sample.cshtml");
            var comtext = System.IO.File.ReadAllText(localUrl);
            mailBody = Razor.Parse(comtext, modelData);

            return mailBody;
        }

        public ActionResult Contact()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

            string pdfTemplate = System.IO.Path.Combine(Server.MapPath("~/UploadedFiles/whitelabeled"), "PIRS Capital - Merchant Agreement_4.pdf");

            PdfReader pdfReader = new PdfReader(pdfTemplate);
            PdfDocument pdfDoc = new PdfDocument(pdfReader);
            // Get the fields from the reader (read-only!!!)
            PdfAcroForm form = PdfAcroForm.GetAcroForm(pdfDoc, true);
            var field = form.GetField("Text1");
            var allField = form.GetFormFields();

            var size = pdfDoc.GetDefaultPageSize();
            var pdfPage = pdfDoc.GetFirstPage();
            var box1 = pdfPage.GetMediaBox();
            var box2 = pdfPage.GetCropBox();
            var box3 = pdfPage.GetBleedBox();
            var box4 = pdfPage.GetTrimBox();

            var fields = new List<PDFFieldFullInfo>();
            foreach (var fld in allField)
            {
                var position = form.GetField(fld.Key).GetWidgets().First().GetRectangle().ToList();
                int llX = (int)Math.Round(Convert.ToDouble(position[0].ToString()), 0);
                int llY = (int)Math.Round(Convert.ToDouble(position[1].ToString()), 0);
                int urX = (int)Math.Round(Convert.ToDouble(position[2].ToString()), 0);
                int urY = (int)Math.Round(Convert.ToDouble(position[3].ToString()), 0);
                var newField = new PDFFieldFullInfo()
                {
                    Name = fld.Value.GetFieldName().GetValue(),
                    Font = fld.Value.GetFont().GetFontProgram().ToString(),
                    BorderWidth = (int)Math.Round(fld.Value.GetBorderWidth(), 0),
                    FontSize = (int)fld.Value.GetFontSize(),
                    Type = fld.Value.GetType().Name,
                    Value = fld.Value.GetValue(),
                    OffsetX = llX,
                    OffsetY = llY,
                    Width = urX - llX,
                    Height = urY - llY
                };
                fields.Add(newField);
            }

            var text = PdfTextExtractor.GetTextFromPage(pdfDoc.GetFirstPage(), new LocationTextExtractionStrategy());

            using (PdfDocument pdfDocument = new PdfDocument(new PdfReader(pdfTemplate)))
            {
                var pageNumbers = pdfDocument.GetNumberOfPages();
                for (int i = 1; i <= pageNumbers; i++)
                {
                    // LocationTextExtractionStrategy strategy = new LocationTextExtractionStrategy();
                    // strategy.SetUseActualText(true);
                    // var pageText = strategy.GetResultantText();
                }
            }

            IRenderingStrategy strategy = new RenderListener();
            // LocationTextExtractionStrategy strategy = new LocationTextExtractionStrategy();
            PdfCanvasProcessor parser = new PdfCanvasProcessor(strategy);
            parser.ProcessPageContent(pdfDoc.GetFirstPage());
            var results = strategy.GetResultantRender();
            var font = pdfDoc.GetDefaultFont();

            PDFViewModel dataModel = new PDFViewModel()
            {
                DefaultFont = font.GetFontProgram().ToString(),
                Texts = results,
                Fields = fields,
                PageHeight = (int)size.GetHeight()
            };

            return View(dataModel);
        }
    }
}