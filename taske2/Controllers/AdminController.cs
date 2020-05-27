using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using taske2.Models;
using taske2.Repos;
using taske2.ViewModel;

namespace taske2.Controllers
{
    public class AdminController : Controller
    {
        Repos.ResumeRepo _resumeRepository = new ResumeRepo();
        // GET: Admin
        public ActionResult AdminPanel()
        {
            if (Session["Admin"] != null)
            { 
                    List<Person> allpersons = new List<Person>();
                    allpersons = _resumeRepository.GetAllPeople();
                    List<PersonVM> personVMs = new List<PersonVM>();

                    for (int i = 0; i < allpersons.Count; i++)
                    {
                        personVMs.Add(new PersonVM());
                        personVMs[i].IDPers = allpersons[i].IDperson;
                        personVMs[i].Email = allpersons[i].Email;
                        personVMs[i].FirstName = allpersons[i].FirstName;
                        personVMs[i].LastName = allpersons[i].LastName;
                        Education e=_resumeRepository.GetEducationByID(allpersons[i].IDperson).FirstOrDefault();
                        personVMs[i].CGPA = e.CGPA;
                    }
                    ViewBag.personArray = personVMs.ToArray();
                    return View();
            }
            else
            {
                return RedirectToAction("Login");
            }           
        }

        public ActionResult Login()
        {
            if (Session["Admin"] != null)
            {
                RedirectToAction("AdminPanel");
            }
            if (TempData["ErrorMessage"] != null)
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"];
            }
            return View();
        }


        public ActionResult LoginPost(FormCollection form)
        {
            string Email = form["email"];
            string Password = form["password"];
            if(_resumeRepository.userLogin(Email, Password))
            {
                Session["Admin"] = true;
                Session["AdminEmail"] = Email;
                bool flag = (bool)Session["Admin"];
                return RedirectToAction("AdminPanel");
            }
            TempData["ErrorMessage"] = "Incorrect Email and Password!";
            return RedirectToAction("Login");

        }


        public ActionResult SignOut(FormCollection form)
        {

            Session["Admin"] = false;
            Session["AdminEmail"] = "";
            return RedirectToAction("Login");

        }


        public ActionResult DeleteRecord(FormCollection form)
        {
            int Idpers = int.Parse(form["IDPers"]);
            _resumeRepository.deleteperson(Idpers);
            return RedirectToAction("AdminPanel");
        }

        public ActionResult ViewPDF(FormCollection form)
        {
            Session["IdSelected"] = int.Parse(form["IDPers"]);


            return RedirectToAction("standardPDF", "PDF");
        }





        public ActionResult addAdmin()
        {
            if (TempData["ErrorMessage"] != null)
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"];
            }
            else if (TempData["successMessage"] != null)
            {
                ViewBag.successMessage = TempData["successMessage"];
            }
            return View();
        }

        public ActionResult addAdminPost(FormCollection form)
        {
            string Email = form["email"];
            string Password = form["password"];

            bool if_everything_Goes_Accorording_To_Plan = _resumeRepository.addadmin(Email, Password); ;
            if (if_everything_Goes_Accorording_To_Plan)
            {
                TempData["successMessage"] = "New Admin Added!";
                return RedirectToAction("Login");
            }
            else
            {
                
                TempData["ErrorMessage"] = "Email Already Taken!";
                return RedirectToAction("addAdmin");
            }
            
        }
       
        
        
        
        public ActionResult CreatePost(PostVM post)
        {
            if(Session["edit"]!=null)
            {
                bool edit = (bool)Session["edit"];
                if (edit)
                {
                    string edittitle = Session["mytitle"].ToString();
                    string editdes = Session["mydesc"].ToString();
                    string edittime = Session["myendtime"].ToString();
                    post.Title = edittitle;
                    post.End_time = edittime;
                    post.Description = editdes;
                    Session["edit"] = false;
                    int id = int.Parse(Session["myid"].ToString());
                    _resumeRepository.deletepost(id);

                }
            }
            
            return View(post);
        }
        public ActionResult CreatePostPOST(FormCollection form)
        {
            


            string title = form["title"];
            string endtime = DateTime.Parse(form["End_time"].ToString()).ToShortDateString();
            string desc = form["description"];

           
            string adminname = Session["AdminEmail"].ToString();
            if(adminname!=null)
            {
                bool flag = _resumeRepository.addpost(title, desc, endtime, adminname);
                if(flag)
                return RedirectToAction("AdminPanel");
            }
            return RedirectToAction("CreatePost");
        }
        public ActionResult AllPosts()
        {
            List<PostVM> posts = new List<PostVM>();
            foreach(internship_post ip in _resumeRepository.GetInternship_Posts())
            {
                List<PersonVM> pvm = new List<PersonVM>();
                List<Person> p = _resumeRepository.GetApplicants(ip.Id);
                for(int i =0;i<p.Count;i++)
                {
                    pvm.Add(new PersonVM());
                    pvm[i].Email = p[i].Email;
                    pvm[i].CGPA = p[i].Address;
                    pvm[i].FirstName = p[i].FirstName;
                    pvm[i].LastName = p[i].LastName;
                }
                posts.Add(new PostVM()
                {
                    Description = ip.Description,
                    Title = ip.Title,
                    End_time = ip.End_time,
                    Post_time = ip.Post_time,
                    ID = ip.Id,
                    ApplicationsArray = pvm.ToArray()
                }
                );
                
            }
            ViewBag.All_Posts = posts.ToArray();
            ViewBag.user_admin = Session["Admin"];
            return View();
        }

        public ActionResult DeletePOST(FormCollection form)
        {
            int id = int.Parse( form["ID"]);
            _resumeRepository.deletepost(id);
            return RedirectToAction("AllPosts");
        }

        public ActionResult EditPOST(FormCollection form)
        {
            int id = int.Parse(form["ID"]);
            internship_post currentpost= _resumeRepository.GetPostById(id);
            Session["mytitle"] = currentpost.Title;
            Session["mydesc"] = currentpost.Description;
            Session["myendtime"] = currentpost.End_time;
            Session["edit"] = true;
            Session["myid"] = currentpost.Id;
            return RedirectToAction("CreatePost");
        }
    }
}