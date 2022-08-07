using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace pdfTrial.Models
{
    public partial class MerchantAgreement
    {
        [Display(Name = "Merchants Legal Name")]
        public string MerchantsLegalName { get; set; }
        [Display(Name = "DBA")]
        public string DBA { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Cell Phone")]
        public string CellPhone { get; set; }
        [Display(Name = "Limited Liability Company")]
        public bool LimitedLiabilityCompany { get; set; }
        [Display(Name = "Limited Partnership")]
        public bool LimitedPartnership { get; set; }
        [Display(Name = "Limited Liability Partnership")]
        public bool LimitedLiabilityPartnership { get; set; }
        [Display(Name = "Sole Proprietor")]
        public bool SoleProprietor { get; set; }
        [Display(Name = "Physical Address")]
        public string PhysicalAddress { get; set; }
        [Display(Name = "City")]
        public string PhysicalAddressCity { get; set; }
        [Display(Name = "State")]
        public string PhysicalAddressState { get; set; }
        [Display(Name = "Zip")]
        public string PhysicalAddressZip { get; set; }
        [Display(Name = "Mailing Address")]
        public string MailingAddress { get; set; }
        [Display(Name = "City_2")]
        public string MailingAddressCity { get; set; }
        [Display(Name = "State_2")]
        public string MailingAddressState { get; set; }
        [Display(Name = "Zip_2")]
        public string MailingAddressZip { get; set; }
        [Display(Name = "Purchase Price")]
        public string PurchasePrice { get; set; }
        [Display(Name = "Specified Percentage")]
        public string SpecifiedPercentage { get; set; }
        [Display(Name = "Purchased Amount")]
        public string PurchasedAmount { get; set; }
        [Display(Name = "Print Name and Title")]
        public string Merchant1PrintNameAndTitle { get; set; }
        [Display(Name = "Print Name and Title.1")]
        public string Merchant1PrintNameAndTitle_1 { get; set; }
        [Display(Name = "Print Name and Title_2")]
        public string Merchant2PrintNameAndTitle { get; set; }
        [Display(Name = "Print Name and Title_2.1")]
        public string Merchant2PrintNameAndTitle_1 { get; set; }
        [Display(Name = "Print Name and TitleO3")]
        public string Merchant3PrintNameAndTitle { get; set; }
        [Display(Name = "Print Name and Title_2O4")]
        public string Merchant4PrintNameAndTitle { get; set; }
        [Display(Name = "Print Name")]
        public string Owner1PrintName { get; set; }
        [Display(Name = "Print Name.1")]
        public string Owner1PrintName_1 { get; set; }
        [Display(Name = "Print Name_2")]
        public string Owner2PrintName { get; set; }
        [Display(Name = "Print NameO3")]
        public string Owner3PrintName { get; set; }
        [Display(Name = "Print Name_2O4")]
        public string Owner4PrintName { get; set; }
        [Display(Name = "Company Officer")]
        public string CompanyOfficer { get; set; }
        [Display(Name = "Company Officer.1")]
        public string CompanyOfficer_1 { get; set; }
        [Display(Name = "Merchants Legal Name_2")]
        public string AppendixA_MerchantsLegalName { get; set; }
        [Display(Name = "DBA_2")]
        public string AppendixA_DBA { get; set; }
        [Display(Name = "Physical Address_2")]
        public string AppendixA_PhysicalAddress { get; set; }
        [Display(Name = "City_3")]
        public string AppendixA_PhysicalAddressCity { get; set; }
        [Display(Name = "State_3")]
        public string AppendixA_PhysicalAddressState { get; set; }
        [Display(Name = "Zip_3")]
        public string AppendixA_PhysicalAddressZip { get; set; }
        [Display(Name = "Federal ID")]
        public string FederalID { get; set; }
        [Display(Name = "the Additional Collateral Owner understands that Purchaser will have a security interest in the aforesaid Additional")]
        public string SecurityIntrestIn { get; set; }
        [Display(Name = "Print Name and Title_3")]
        public string AppendixA_Merchant1PrintNameAndTitle { get; set; }
        [Display(Name = "SS")]
        public string Merchant1SSNumber { get; set; }
        [Display(Name = "Print Name and Title_4")]
        public string AppendixA_Merchant2PrintNameAndTitle { get; set; }
        [Display(Name = "Print Name and Title_3O3")]
        public string AppendixA_Merchant3PrintNameAndTitle { get; set; }
        [Display(Name = "Print Name and Title_4O4")]
        public string AppendixA_Merchant4PrintNameAndTitle { get; set; }
        [Display(Name = "SS_2")]
        public string Merchant2SSNumber { get; set; }
        [Display(Name = "SSO3")]
        public string Merchant3SSNumber { get; set; }
        [Display(Name = "SS_2O4")]
        public string Merchant4SSNumber { get; set; }
        [Display(Name = "Print Name_3")]
        public string AppendixA_Owner1PrintName { get; set; }
        [Display(Name = "SS_3")]
        public string Owner1SSNumber { get; set; }
        [Display(Name = "Print Name_4")]
        public string AppendixA_Owner2PrintName { get; set; }
        [Display(Name = "Print Name_3O3")]
        public string AppendixA_Owner3PrintName { get; set; }
        [Display(Name = "Print Name_4O4")]
        public string AppendixA_Owner4PrintName { get; set; }
        [Display(Name = "SS_4")]
        public string Owner2SSNumber { get; set; }
        [Display(Name = "SS_3O3")]
        public string Owner3SSNumber { get; set; }
        [Display(Name = "SS_4O4")]
        public string Owner4SSNumber { get; set; }
        [Display(Name = "Drivers License Number")]
        public string Merchant1DriversLicenseNumber { get; set; }
        [Display(Name = "Drivers License Number_2")]
        public string Merchant2DriversLicenseNumber { get; set; }
        [Display(Name = "Drivers License NumberO3")]
        public string Merchant3DriversLicenseNumber { get; set; }
        [Display(Name = "Drivers License Number_2O4")]
        public string Merchant4DriversLicenseNumber { get; set; }
        [Display(Name = "Drivers License Number_3")]
        public string Owner1DriversLicenseNumber { get; set; }
        [Display(Name = "Drivers License Number_4")]
        public string Owner2DriversLicenseNumber { get; set; }
        [Display(Name = "Drivers License Number_3O3")]
        public string Owner3DriversLicenseNumber { get; set; }
        [Display(Name = "Drivers License Number_4O4")]
        public string Owner4DriversLicenseNumber { get; set; }
        [Display(Name = "Routing Number")]
        public string RoutingNumber { get; set; }
        [Display(Name = "Account Number")]
        public string AccountNumber { get; set; }
        [Display(Name = "Account Name")]
        public string AccountName { get; set; }
        [Display(Name = "Merchants Legal Name 1")]
        public string AppendixB_MerchantsLegalName { get; set; }
        [Display(Name = "Printed Name")]
        public string PrintedName { get; set; }
        [Display(Name = "Login")]
        public string Login { get; set; }
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Display(Name = "Date")]
        public string Date { get; set; }
        [Display(Name = "Coproration")]
        public bool Corporation { get; set; }
        [Display(Name = "Agreement dated")]
        public string AgreementDate { get; set; }
        [Display(Name = "merchant email")]
        public string MerchantEmail { get; set; }
        [Display(Name = "AgreementDate2")]
        public string AppendixB_AgreementDate { get; set; }
        [Display(Name = "AppFee")]
        public string AppFee { get; set; }
        [Display(Name = "OriginatingFeeText")]
        public string OriginatingFeeText { get; set; }
        [Display(Name = "OriginatingFeeNum")]
        public string OriginatingFeeNum { get; set; }
        [Display(Name = "MerchantAddress")]
        public string MerchantAddress { get; set; }
        [Display(Name = "MerchantName")]
        public string MerchantName { get; set; }
        [Display(Name = "US")]
        public string US { get; set; }
        [Display(Name = "Merch2")]
        public string MerchantName2 { get; set; }
        public string MerchantName3 { get; set; }
        public string MerchantName4 { get; set; }
        [Display(Name = "Date2")]
        public string Date2 { get; set; }
        [Display(Name = "Daily")]
        public string Daily { get; set; }
        [Display(Name = "US1")]
        public string US1 { get; set; }
        [Display(Name = "US2")]
        public string US2 { get; set; }
        [Display(Name = "PIRS CAPITAL LLC")]
        public string PirsCapitalLLC { get; set; }
        [Display(Name = "Ownerss")]
        public string Owner1 { get; set; }
        [Display(Name = "NAME OF MERCHANT")]
        public string NameOfMerchant { get; set; }
        [Display(Name = "undefined")]
        public string Owner2 { get; set; }
        [Display(Name = "OwnerssO3")]
        public string NameOfMerchant2 { get; set; }
        [Display(Name = "undefined_2")]
        public string Owner3 { get; set; }
        [Display(Name = "OwnerssO4")]
        public string NameOfMerchant3 { get; set; }
        [Display(Name = "US3")]
        public string US3 { get; set; }
        [Display(Name = "By")]
        public string MerchantBy { get; set; }
        [Display(Name = "Date1")]
        public string AppendixB_Date { get; set; }
        [Display(Name = "Signature")]
        public string Merchant1Signature { get; set; }
        [Display(Name = "Signature.1")]
        public string Merchant1Signature_1 { get; set; }
        [Display(Name = "Signature_2")]
        public string Merchant2Signature { get; set; }
        [Display(Name = "SignatureO3")]
        public string Merchant3Signature { get; set; }
        [Display(Name = "Signature_2O4")]
        public string Merchant4Signature { get; set; }
        [Display(Name = "Signature_3")]
        public string Owner1Signature { get; set; }
        [Display(Name = "Signature_4")]
        public string Owner2Signature { get; set; }
        [Display(Name = "Signature_3O3")]
        public string Owner3Signature { get; set; }
        [Display(Name = "Signature_4O4")]
        public string Owner4Signature { get; set; }
        [Display(Name = "Signature_5")]
        public string CompanyOfficerSignature { get; set; }
        [Display(Name = "Signature_6")]
        public string AppendixA_Merchant1Signature { get; set; }
        [Display(Name = "Signature_7")]
        public string AppendixA_Merchant2Signature { get; set; }
        [Display(Name = "Signature_6O3")]
        public string AppendixA_Merchant3Signature { get; set; }
        [Display(Name = "Signature_7O4")]
        public string AppendixA_Merchant4Signature { get; set; }
        [Display(Name = "Signature_8")]
        public string AppendixA_Owner1Signature { get; set; }
        [Display(Name = "Signature_9")]
        public string AppendixA_Owner2Signature { get; set; }
        [Display(Name = "Signature_8O3")]
        public string AppendixA_Owner3Signature { get; set; }
        [Display(Name = "Signature_9O4")]
        public string AppendixA_Owner4Signature { get; set; }
        [Display(Name = "Signature_10")]
        public string AppendixB_MerchantSignature { get; set; }
        [Display(Name = "PrintName2")]
        public string MerchantPrintName { get; set; }
        [Display(Name = "Bank Name")]
        public string BankName { get; set; }
        [Display(Name = "Savings")]
        public string Savings { get; set; }
        [Display(Name = "Checking")]
        public string Checking { get; set; }
        [Display(Name = "Merchant Address")]
        public string AppendixB_MerchantAddress { get; set; }
        [Display(Name = "Legal Name 6")]
        public string LegalName { get; set; }
        [Display(Name = "Funding Provided")]
        public string FundingProvided { get; set; }
        [Display(Name = "Funding Disbursed")]
        public string FundingDisbursed { get; set; }
        [Display(Name = "Payout")]
        public string Payout { get; set; }
        [Display(Name = "approximately")]
        public string Approximately { get; set; }
        [Display(Name = "Payment")]
        public string Payment { get; set; }
        [Display(Name = "Period")]
        public string Period { get; set; }
        [Display(Name = "Fees")]
        public string Fees { get; set; }
        [Display(Name = "white label partner")]
        public string WhiteLabelPartner { get; set; }
        [Display(Name = "Program Manger")]
        public string ProgramManger { get; set; }
        [Display(Name = "Program Manger.1")]
        public string ProgramManger_1 { get; set; }
        [Display(Name = "partner")]
        public string Partner { get; set; }
        [Display(Name = "NameOfPartner")]
        public string NameOfPartner { get; set; }
        [Display(Name = "partnername3")]
        public string AppendixB_PartnerName { get; set; }
        [Display(Name = "partnername1")]
        public string PartnerName { get; set; }
        [Display(Name = "partnername2")]
        public string AppendixB_PartnerName_2 { get; set; }
        [Display(Name = "partnername2.1")]
        public string AppendixB_PartnerName_2_1 { get; set; }
        [Display(Name = "partnername2.2")]
        public string AppendixB_PartnerName_2_2 { get; set; }
        public bool IsWhiteLabeled { get; set; }
        public bool IsVirginia { get; set; }
        public bool FourOwnersAndMerchants { get; set; }
        public MerchantAgreementTypes MerchantAgreementTypes { get; set; }
        public string TemplatePath { get; set; }
        public int MerchantAgreementType { get; set; }
    }
}