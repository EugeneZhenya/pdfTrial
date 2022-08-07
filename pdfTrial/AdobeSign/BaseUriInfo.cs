using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace pdfTrial.AdobeSign
{
    [DataContract]
    public class BaseUriInfo
    {
        [DataMember(EmitDefaultValue = false)]
        public string api_access_point { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string web_access_point { get; set; }
    }
}