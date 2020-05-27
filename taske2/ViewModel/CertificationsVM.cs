using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace taske2.ViewModel
{
    public class CertificationsVM
    {
        [Required (ErrorMessage="Please Enter Certificate Name")]
        public string CertificationName { get; set; }



        [Required(ErrorMessage ="Please Enter Certificate Authority name ")]
        public string CertificateAuthority { get; set; }
        [Required(ErrorMessage = "Please select Certification Level")]
        public string LevelCertification { get; set; }

        public List<SelectListItem> ListOfLevel { get; set; }

    }
}