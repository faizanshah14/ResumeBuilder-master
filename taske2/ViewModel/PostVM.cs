using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using taske2.Models;

namespace taske2.ViewModel
{
    public class PostVM
    {
        public int ID { get; set; }


        [DisplayName("Description")]
        [Required(ErrorMessage = "Please Discription")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }


        [DisplayName("Deadline")]
        [Required(ErrorMessage = "Please Enter Ending Time")]
        public string End_time { get; set; }


        public string Post_time { get; set; }

        [DisplayName("Title")]
        [Required(ErrorMessage = "Please Enter Title")]
        public string Title { get; set; }

        public List<SelectListItem> ListOfPosts { get; set; }


        //populate it with people who have applied
        public PersonVM[] ApplicationsArray { get; set; }

    }
}