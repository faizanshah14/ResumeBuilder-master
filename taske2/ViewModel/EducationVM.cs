using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace taske2.ViewModel
{
    public class EducationVM
    {
        public int IDEdu { get; set; }

        [DisplayName("Institution")]
        public string InstituteUniversity { get; set; }


        [DisplayName("Title")]
        public string TitleOfDiploma { get; set; }

        
        public string Degree { get; set; }

        [DisplayName("From Year")]
        public Nullable<System.DateTime> FromYear { get; set; }

        [DisplayName("To Year")]
        public Nullable<System.DateTime> ToYear { get; set; }
        
        
        public string City { get; set; }
        
        public string Country { get; set; }

        [DisplayName("CGPA")]
        public string CGPA { get; set; }

        public List<SelectListItem> ListOfCountry { get; set; }
        public List<SelectListItem> ListOfCity { get; set; }
    }
}