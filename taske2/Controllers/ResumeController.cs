using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using taske2.ViewModel;
using taske2.Repos;
using taske2.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.IO;

namespace taske2.Controllers
{
    public class ResumeController : Controller
    {
        int currentPerson = 0;

        Repos.ResumeRepo _resumeRepository = new ResumeRepo();
        // GET: Resume
        public ActionResult Index()
        {
            return View();
        }


       
        /// GET requests /////////////////////////////////////////////////////////
      
        //STEP 1 GET 
        [HttpGet]
        public ActionResult PersonnalInformtion(PersonVM model)
        {
            
            List<SelectListItem> nationalality = new List<SelectListItem>()
            {
                new SelectListItem{ Text = "Pakistan" , Value="Pakistan", Selected=true},
            };
            List<SelectListItem> educationLevel = new List<SelectListItem>()
            {
                new SelectListItem { Text = "High School", Value = "High School", Selected = true},
                new SelectListItem { Text = "Diploma", Value = "Diploma"},
                new SelectListItem { Text = "Bachelor's degree", Value = "Bachelor's degree"},
                new SelectListItem { Text = "Master's degree", Value = "Master's degree"},
                new SelectListItem { Text = "Doctorate", Value = "Doctorate"},
            };
            model.ListEducationalLevel = educationLevel;
            model.ListNationality = nationalality;
            currentPerson= model.IDPers;
            return View(model);

        }

        //STEP 2 GET
        [HttpGet]
        public ActionResult Education(EducationVM education)
        {

            if (education == null)
            {
                education = new EducationVM();
            }
            else
            {
                int id = (int)Session["IdSelected"];
                List<Education> educations = _resumeRepository.GetEducationByID(id).ToList();
                int number = educations.Count;
                EducationVM[] OldArray = new EducationVM[number];
                for(int i=0;i<number;i++)
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<Education, EducationVM>());

                    OldArray[i] = Mapper.Map<EducationVM>(educations[i]);
                    OldArray[i].TitleOfDiploma = educations[i].Title;
                    OldArray[i].InstituteUniversity = educations[i].InstituteName;
                }
                //Fetch All previous EDUCATION DETAILS From Database and STORE IT IN "OLDArray"
                //IF DB returns == 0 then OldArray = null
                

                ViewData["History"] = OldArray;
            }

            education.ListOfCountry = GetCountries();
            return View(education);
        }

        //STEP 3 GET
        [HttpGet]
        public ActionResult WorkExperience(WorkExperienceVM experience)
        {

           if(experience==null)
            {
                experience = new WorkExperienceVM();
            }
           else
            {


                //experience.Title = "CEO";
                int id = (int)Session["IdSelected"];//(int)Session["IdSelected"];
                List<WorkExperience> works = _resumeRepository.GetWorkExperiencesById(id).ToList();
                int number = works.Count;
                WorkExperienceVM[] OldArray = new WorkExperienceVM[number];
                for (int i = 0; i < number; i++)
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<WorkExperience, WorkExperienceVM>());
                    OldArray[i] = Mapper.Map<WorkExperienceVM>(works[i]);

                }
                //FETCH A LIST FROM DB OTHERWISE SEND NULL

                ViewData["WorkHistory"] = OldArray;
            }
            experience.ListeOfCountries = GetCountries();

            return View(experience);
  
        }

        //STEP 4 GET
        [HttpGet]
        public ActionResult SkiCerfLang()
        {

            //Skills History FETCH FROM DATABASE if NOTHING KEEP AS NULL

            SkillsVM vm = new SkillsVM();
            

            int id = (int)Session["IdSelected"];
            List<Skill> skills = _resumeRepository.GetSkillsById(id).ToList();
            int number = skills.Count;
            SkillsVM[] OldSkillsArray = new SkillsVM[number];
            for (int i = 0; i < number; i++)
            {
                Mapper.Initialize(cfg => cfg.CreateMap<Skill, SkillsVM>());

                OldSkillsArray[i] = Mapper.Map<SkillsVM>(skills[i]);
            }



           
            TempData["SkillsHistory"] = OldSkillsArray;

            //Certificaton History  FROM DATABASE IF NOTHING KEEP AS NULL
            
            List<Certification> allcertifications = _resumeRepository.GetCertificationsById(id).ToList();
            number = allcertifications.Count;
            List<CertificationsVM> OldcertificationArray = new List<CertificationsVM>();
            for (int i = 0; i < number; i++)
            {
                OldcertificationArray.Add(new CertificationsVM());
                OldcertificationArray[i].CertificationName = allcertifications[i].CertificateName;
                OldcertificationArray[i].CertificateAuthority = allcertifications[i].CertificationAuthority;
                OldcertificationArray[i].LevelCertification = allcertifications[i].CertificationLevel;
            }
            
            TempData["CertificatesHistory"] = OldcertificationArray.ToArray();

            //Languages History FROM DATABASE IF NOTHING KEEP AS NULL   
          

           
            List<Language> alllanguages = _resumeRepository.GetLanguagesById(id).ToList();
            number = alllanguages.Count;
            LanguageVM[] OldLanguagesArray = new LanguageVM[number];
            for (int i = 0; i < number; i++)
            {
                Mapper.Initialize(cfg => cfg.CreateMap<Language, LanguageVM>());
                OldLanguagesArray[i] = Mapper.Map<LanguageVM>(alllanguages[i]);
            }
            TempData["LanguageHistory"] = OldLanguagesArray;
            return View();
        }

        //STEP 4 GET
        public PartialViewResult SkillsPartial()
        {
            return PartialView("~/Views/Shared/SkiCerfLang/_MySkills.cshtml");
        }
        //Step 5 GET
        public PartialViewResult CertificationsPartial(CertificationsVM certification)
        {
            List<SelectListItem> certificationLevel = new List<SelectListItem>()
        {
            new SelectListItem { Text = "Beginner", Value = "Beginner", Selected = true},
            new SelectListItem { Text = "Intermediate", Value = "Intermediate"},
            new SelectListItem { Text = "Advanced", Value = "Advanced"}
        };

            certification.ListOfLevel = certificationLevel;

            return PartialView("~/Views/Shared/SkiCerfLang/_MyCertifications.cshtml", certification);
        }

        //STEP 6 GET
        public PartialViewResult LanguagePartial(LanguageVM language)
        {
            List<SelectListItem> languageLevel = new List<SelectListItem>()
            {
                new SelectListItem { Text = "Elementary Proficiency", Value = "Elementary Proficiency", Selected = true},
                new SelectListItem { Text = "LimitedWorking Proficiency", Value = "LimitedWorking Proficiency"},
                new SelectListItem { Text = "Professional working Proficiency", Value = "Professional working Proficiency"},
                new SelectListItem { Text = "Full Professional Proficiency", Value = "Full Professional Proficiency"},
                new SelectListItem { Text = "Native Or Bilingual Proficiency", Value = "Native Or Bilingual Proficiency"}
            };

            language.ListOfProficiency = languageLevel;

            return PartialView("~/Views/Shared/SkiCerfLang/_MyLanguage.cshtml", language);
        }

        //STEP 7 GET
        public ActionResult Options(PostVM post)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            List<internship_post> li = _resumeRepository.GetInternship_Posts();
            for(int i =0;i<li.Count;i++)
            {
                list.Add(new SelectListItem());
                list[i].Text = li[i].Title;
            }

            post.ListOfPosts = list;

            return View(post);
        }



        // POST REQUESTS ///////////////////////////////////////////////////////////


        //STEP 1 POST
        [HttpPost]
        public ActionResult PersonnalInformtionPOST(PersonVM person)
        {

            if (ModelState.IsValid)
            {  
                //Repos.ResumeRepo _resumeRepository = new ResumeRepo();
                //Creating Mapping  
                Mapper.Initialize(cfg => cfg.CreateMap<PersonVM, Person>());
                
                Person personEntity = Mapper.Map<Person>(person);
                personEntity.TelephoneNumber = person.Tel;
                personEntity.FacebookProfile = person.FaceBookProfil;
                personEntity.LinkedinProfile = person.LinkedInProdil;
                personEntity.DateOfBirth = person.DateOfBirth.Value.ToShortDateString();

                string fileName = Path.GetFileNameWithoutExtension(person.ImageUpload.FileName);
                string extension = Path.GetExtension(person.ImageUpload.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                person.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Resources/Images/"), fileName));
                person.ImagePath = "../Resources/Images/"+ fileName;
                fileName = person.ImagePath;
               
                bool result = _resumeRepository.AddPersonalInformation(personEntity, fileName);
                
                if (result)
                {
                    Session["IdSelected"] = _resumeRepository.GetIdPersonLast();
                    return RedirectToAction("Education");
                }
                else
                {
                    ViewBag.Message = "Something Wrong !";
                    return View(person);
                }

            }

            ViewBag.MessageForm = "Please Check your form before submit !";
            return RedirectToAction("PersonnalInformtion");

        }
        
        //STEP 2 POST
        [HttpPost]
        public ActionResult AddOrUpdateEducation(EducationVM education)
        {
            int id = int.Parse(Session["IdSelected"].ToString());
            //HERE we will save the Object to DATABASE
            //Based upon some conditions
            Mapper.Initialize(cfg => cfg.CreateMap<EducationVM, Education>());

            Education EdutionEntity = Mapper.Map<Education>(education);
            EdutionEntity.Title = education.TitleOfDiploma;
            EdutionEntity.InstituteName = education.InstituteUniversity;
            EdutionEntity.FromYear = education.FromYear.Value.ToShortDateString();
            EdutionEntity.ToYear = education.ToYear.Value.ToShortDateString();
            _resumeRepository.AddOrUpdateEducation(EdutionEntity, id);
            return RedirectToAction("Education");
        
        }

        //Step 3 POST
        public ActionResult AddOrUpdateExperience(WorkExperienceVM workExperience)
        {

            string msg = string.Empty;

            if (workExperience != null)
            {
                //Creating Mapping  
                Mapper.Initialize(cfg => cfg.CreateMap<WorkExperienceVM, WorkExperience>());
                WorkExperience workExperienceEntity = Mapper.Map<WorkExperience>(workExperience);
                workExperienceEntity.FromYear = workExperience.FromYear.Value.ToShortDateString();
                workExperienceEntity.ToYear = workExperience.ToYear.Value.ToShortDateString();
                int idPer = (int)Session["IdSelected"];


                msg = _resumeRepository.AddOrUpdateExperience(workExperienceEntity, idPer);

            }
            else
            {
                msg = "Please re try the operation";
            }

            return RedirectToAction("WorkExperience");
        }



        //STEP 4.1 POST //SkillsHistory
        public ActionResult AddSkill(SkillsVM skill)
        {
            int idPer = (int)Session["IdSelected"];
            string msg = string.Empty;

            //Creating Mapping  
            Mapper.Initialize(cfg => cfg.CreateMap<SkillsVM, Skill>());
            Skill skillEntity = Mapper.Map<Skill>(skill);
            if (_resumeRepository.AddSkill(skillEntity, idPer))
            {
                msg = "skill added successfully";
            }
            else
            {
                msg = "something Wrong";
            }

            return RedirectToAction("SkiCerfLang");
        }

        //STEP 4.2 POST 
        public ActionResult AddCertification(CertificationsVM certification)
        {
            int idPer = (int)Session["IdSelected"];
            string msg = string.Empty;

            //Creating Mapping  
            Certification certificationEntity = new Certification();
            certificationEntity.CertificateName = certification.CertificationName;
            certificationEntity.CertificationAuthority = certification.CertificateAuthority;
            certificationEntity.CertificationLevel = certification.LevelCertification;
            
            certificationEntity.IDPerson = idPer;

            if (_resumeRepository.AddCertification(certificationEntity, idPer))
            {
                msg = "Certification added successfully";
            }
            else
            {
                msg = "something Wrong";
            }

            return RedirectToAction("SkiCerfLang");
        }
        // STEP 4.3 POST
        public ActionResult AddLanguage(LanguageVM language)
        {
            int idPer = (int)Session["IdSelected"];
            string msg = string.Empty;

            //Creating Mapping  
            Mapper.Initialize(cfg => cfg.CreateMap<LanguageVM, Language>());
            Language languageEntity = Mapper.Map<Language>(language);

            if (_resumeRepository.AddLanguage(languageEntity, idPer))
            {
                msg = "Language added successfully";
            }
            else
            {
                msg = "something Wrong";
            }

            return RedirectToAction("SkiCerfLang");
        }


        //Step 7 POST

       public ActionResult SelectOption(FormCollection form)
        {
            string selectedOption = form["option"];

           
            if (selectedOption == "standard")
                return RedirectToAction("standardPDF", "PDF");
            else
                return RedirectToAction("compactPDF", "PDF");


        }

        public ActionResult PositionApply(FormCollection form)
        {
            string selectedOption = form["ListOfPosts"];
            int idperson =(int)Session["IdSelected"];
            _resumeRepository.applyOnPost(selectedOption, idperson);
            return RedirectToAction("SelectOption");
        }

        //others//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        
        
        
        [HttpGet]
        public PartialViewResult EducationPartial(EducationVM education)
        {
            
            education.ListOfCountry = GetCountries();
            return PartialView("~/Views/Shared/_MyEducation.cshtml", education);
        }
       

        public PartialViewResult WorkExperiencePartial(WorkExperienceVM workExperience)
        {
            workExperience.ListeOfCountries = GetCountries();

            return PartialView("~/Views/Shared/_MyWorkExperience.cshtml", workExperience);
        }

      


       

       

        
        

       
       

      
        
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public ActionResult CV()
        {
            return View();
        }
        public PartialViewResult GetPersonnalInfoPartial()
        {
            //          int idPer = 0;
            //          Person person = _resumeRepository.GetPersonalInformation(idPer);

            //Creating Mapping  
            //         Mapper.Initialize(cfg => cfg.CreateMap<Person, PersonVM>());
            //         PersonVM personVM = Mapper.Map<PersonVM>(person);
            PersonVM model = new PersonVM();
            List<SelectListItem> nationalality = new List<SelectListItem>()
            {
                new SelectListItem{ Text = "Pakistan" , Value="Pakistan", Selected=true},
            };
            List<SelectListItem> educationLevel = new List<SelectListItem>()
            {
                new SelectListItem { Text = "Hight School", Value = "Hight School", Selected = true},
                new SelectListItem { Text = "Diploma", Value = "Diploma"},
                new SelectListItem { Text = "Bachelor's degree", Value = "Bachelor's degree"},
                new SelectListItem { Text = "Master's degree", Value = "Master's degree"},
                new SelectListItem { Text = "Doctorate", Value = "Doctorate"},
            };
            model.ListEducationalLevel = educationLevel;
            model.ListNationality = nationalality;


            return PartialView("~/Views/Shared/CV/_MyPersonnalInfo.cshtml", model);
        }
        public PartialViewResult GetEducationCVPartial()
        {
            //        int idPer = 0;// (int)Session["IdSelected"];

            //Creating Mapping  
            //        Mapper.Initialize(cfg => cfg.CreateMap<Education, EducationVM>());
            //        IQueryable<EducationVM> educationList = _resumeRepository.GetEducationByID(idPer).ProjectTo<EducationVM>().AsQueryable();
           
              EducationVM  education = new EducationVM();




            education.ListOfCountry = GetCountries();

            return PartialView("~/Views/Shared/CV/_MyEducationCV.cshtml", education);
        }
        public PartialViewResult WorkExperienceCVPartial()
        {
            //      int idPer = 0;//(int)Session["IdSelected"];

            //Creating Mapping  
            //       Mapper.Initialize(cfg => cfg.CreateMap<WorkExperience, WorkExperienceVM>());
            //       IQueryable<WorkExperienceVM> workExperienceList = _resumeRepository.GetWorkExperiencesById(idPer).ProjectTo<WorkExperienceVM>().AsQueryable();


            WorkExperienceVM experience = new WorkExperienceVM();
            experience.ListeOfCountries = GetCountries();
            return PartialView("~/Views/Shared/CV/_WorkExperienceCV.cshtml", experience);
        }
        public PartialViewResult SkillsCVPartial()
        {
    //        int idPer = 0;//(int)Session["IdSelected"];

            //Creating Mapping  
   //         Mapper.Initialize(cfg => cfg.CreateMap<Skill, SkillsVM>());
   //         IQueryable<SkillsVM> skillsList = _resumeRepository.GetSkillsById(idPer).ProjectTo<SkillsVM>().AsQueryable();


            return PartialView("~/Views/Shared/CV/_MySkillsCV.cshtml");
        }
        public PartialViewResult CertificationsCVPartial()
        {
            int idPer = 0;// (int)Session["IdSelected"];

            //Creating Mapping  
            //       Mapper.Initialize(cfg => cfg.CreateMap<Certification, CertificationsVM>());
            //      IQueryable<CertificationsVM> certificationList = _resumeRepository.GetCertificationsById(idPer).ProjectTo<CertificationsVM>().AsQueryable();
            List<SelectListItem> certificationLevel = new List<SelectListItem>()
        {
            new SelectListItem { Text = "Beginner", Value = "Beginner", Selected = true},
            new SelectListItem { Text = "Intermediate", Value = "Intermediate"},
            new SelectListItem { Text = "Advanced", Value = "Advanced"}
        };
            CertificationsVM certification = new CertificationsVM();
            certification.ListOfLevel = certificationLevel;

            return PartialView("~/Views/Shared/CV/_MyCertificationCV.cshtml", certification);
        }
        public PartialViewResult LanguageCVPartial()
        {
            //int idPer = 0;//(int)Session["IdSelected"];

            //Creating Mapping  
            //Mapper.Initialize(cfg => cfg.CreateMap<Language, LanguageVM>());
            //IQueryable<LanguageVM> languageList = _resumeRepository.GetLanguagesById(idPer).ProjectTo<LanguageVM>().AsQueryable();
            LanguageVM language = new LanguageVM();
        
                List<SelectListItem> languageLevel = new List<SelectListItem>()
            {
                new SelectListItem { Text = "Elementary Proficiency", Value = "Elementary Proficiency", Selected = true},
                new SelectListItem { Text = "LimitedWorking Proficiency", Value = "LimitedWorking Proficiency"},
                new SelectListItem { Text = "Professional working Proficiency", Value = "Professional working Proficiency"},
                new SelectListItem { Text = "Full Professional Proficiency", Value = "Full Professional Proficiency"},
                new SelectListItem { Text = "Native Or Bilingual Proficiency", Value = "Native Or Bilingual Proficiency"}
            };

                language.ListOfProficiency = languageLevel;

                return PartialView("~/Views/Shared/CV/_MyLanguageCV.cshtml", language);
        }



        public List<SelectListItem> GetCountries()
        {
            List<SelectListItem> nationalality = new List<SelectListItem>()
            {
                new SelectListItem{ Text = "Pakistan" , Value="Pakistan", Selected=true},
            };
            return nationalality;
        }
    }
}