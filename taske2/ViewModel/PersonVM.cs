using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace taske2.ViewModel
{
    public class PersonVM
    {
        public int IDPers { get; set; }

        [DisplayName("First Name")]
        [Required(ErrorMessage = "Please Your First Name ")]
        public string FirstName { get; set; }




        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Please Your Last Name ")]
        public string LastName { get; set; }


        [DisplayName("Date Of Birth")]
        [Required(ErrorMessage = "Please Your Date Of Birth ")]
        public Nullable<System.DateTime> DateOfBirth { get; set; }



        [DisplayName("Nationality")]
        [Required(ErrorMessage = "Please Your Nationality ")]
        public string Nationality { get; set; }


        [DisplayName("Education Level")]
        [Required(ErrorMessage = "Select Your Educational Level ")]
        public string EducationalLevel { get; set; }


        [DisplayName("Address")]
        [Required(ErrorMessage = "Please Your Address ")]
        public string Address { get; set; }


        [DisplayName("Contact Information")]
        [Required(ErrorMessage = "Please Your Phone Number ")]
        public string Tel { get; set; }


        [DisplayName("Email")]
        [Required(ErrorMessage = "Please Your Email Address ")]
        public string Email { get; set; }


        [DisplayName("Summary")]
        [Required(ErrorMessage = "Please Your Summary")]
        [DataType(DataType.MultilineText)]
        public string Summary { get; set; }


        [DisplayName("linkedin Profile")]
        [Required(ErrorMessage = "Please Your LinekedIn Profil")]
        [DataType(DataType.Url)]
        public string LinkedInProdil { get; set; }


        [DisplayName("Facebook Profile")]
        [Required(ErrorMessage = "Please Your Facebook Profil")]
        [DataType(DataType.Url)]
        public string FaceBookProfil { get; set; }


       // [DisplayName("C# Corner Profile")]
        //[Required(ErrorMessage = "Please Your C# Corner Profil")]
        //[DataType(DataType.Url)]
        //public string C_CornerProfil { get; set; }



        public string CGPA { get; set; }

        public string ImagePath { get; set; }
        public HttpPostedFileBase ImageUpload { get; set; }


        public List<SelectListItem> ListNationality { get; set; }
        public List<SelectListItem> ListEducationalLevel { get; set; }
    }
}