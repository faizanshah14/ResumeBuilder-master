using AutoMapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using taske2.Models;
using taske2.Repos;
using taske2.ViewModel;

namespace taske2.Controllers
{
    public class PDFController : Controller
    {

        Repos.ResumeRepo _resumeRepository = new ResumeRepo();
        // GET: PDF
        public ActionResult standardPDF()
        {
            int id =(int)Session["IdSelected"];
            ////////////////////////////////////////
            Mapper.Initialize(cfg => cfg.CreateMap<Person, PersonVM>());
            Person p = _resumeRepository.GetPersonalInformation(id);
            PersonVM pvm = Mapper.Map<PersonVM>(p);
            pvm.Tel = p.TelephoneNumber;
            pvm.FaceBookProfil = p.FacebookProfile;
            pvm.LinkedInProdil = p.LinkedinProfile;
            pvm.ImagePath = p.ProfilePicture;
            ViewBag.Person = pvm;




            ////////////////////////////////////////
            List<Education> educations = _resumeRepository.GetEducationByID(id).ToList();
            int number = educations.Count;
            EducationVM[] OldArrayEdu = new EducationVM[number];
            for (int i = 0; i < number; i++)
            {
                Mapper.Initialize(cfg => cfg.CreateMap<Education, EducationVM>());

                OldArrayEdu[i] = Mapper.Map<EducationVM>(educations[i]);
                OldArrayEdu[i].TitleOfDiploma = educations[i].Title;
                OldArrayEdu[i].InstituteUniversity = educations[i].InstituteName;
            }
            ViewBag.educationArray = OldArrayEdu;



            ////////////////////////////////////////
            List<WorkExperience> works = _resumeRepository.GetWorkExperiencesById(id).ToList();
            number = works.Count;
            WorkExperienceVM[] OldArraywork = new WorkExperienceVM[number];
            for (int i = 0; i < number; i++)
            {
                Mapper.Initialize(cfg => cfg.CreateMap<WorkExperience, WorkExperienceVM>());
                OldArraywork[i] = Mapper.Map<WorkExperienceVM>(works[i]);
            }
            ViewBag.workArray = OldArraywork;



            ///////////////////////////////////////
            List<Skill> skills = _resumeRepository.GetSkillsById(id).ToList();
            number = skills.Count;
            SkillsVM[] OldSkillsArray = new SkillsVM[number];
            for (int i = 0; i < number; i++)
            {
                Mapper.Initialize(cfg => cfg.CreateMap<Skill, SkillsVM>());

                OldSkillsArray[i] = Mapper.Map<SkillsVM>(skills[i]);
            }
            ViewBag.skillsArray = OldSkillsArray;



            ///////////////////////////////////////
            List<Certification> allcertifications = _resumeRepository.GetCertificationsById(id).ToList();
            number = allcertifications.Count;
            CertificationsVM[] OldcertificationArray = new CertificationsVM[number];
            for (int i = 0; i < number; i++)
            {
                OldcertificationArray[i] = new CertificationsVM();
                OldcertificationArray[i].CertificationName = allcertifications[i].CertificateName;
                OldcertificationArray[i].CertificateAuthority = allcertifications[i].CertificationAuthority;
                OldcertificationArray[i].LevelCertification = allcertifications[i].CertificationLevel;
            }
            ViewBag.certificatesArray =OldcertificationArray;

            ///////////////////////////////////////
            List<Language> alllanguages = _resumeRepository.GetLanguagesById(id).ToList();
            number = alllanguages.Count;
            LanguageVM[] OldLanguagesArray = new LanguageVM[number];
            for (int i = 0; i < number; i++)
            {
                Mapper.Initialize(cfg => cfg.CreateMap<Language, LanguageVM>());
                OldLanguagesArray[i] = Mapper.Map<LanguageVM>(alllanguages[i]);
            }
            ViewBag.languagesArray =OldLanguagesArray;
            //string  s =Path.GetFileName(Request.Url.AbsolutePath);
            //NReco.PdfGenerator.HtmlToPdfConverter htmlTo = new NReco.PdfGenerator.HtmlToPdfConverter();

            //string html = gethtml("localhost:57666/PDF/standardPDF");
            //var pdfBytes = (new NReco.PdfGenerator.HtmlToPdfConverter()).GeneratePdf(html);
            //System.IO.File.WriteAllBytes("hello.pdf", pdfBytes);
            return View();
        }
        public ActionResult compactPDF()
        {
            int id = (int)Session["IdSelected"];
            ////////////////////////////////////////
            Mapper.Initialize(cfg => cfg.CreateMap<Person, PersonVM>());
            Person p = _resumeRepository.GetPersonalInformation(id);
            PersonVM pvm = Mapper.Map<PersonVM>(p);
            pvm.Tel = p.TelephoneNumber;
            pvm.FaceBookProfil = p.FacebookProfile;
            pvm.LinkedInProdil = p.LinkedinProfile;
            pvm.ImagePath = p.ProfilePicture;
            ViewBag.Person = pvm;




            ////////////////////////////////////////
            List<Education> educations = _resumeRepository.GetEducationByID(id).ToList();
            int number = educations.Count;
            EducationVM[] OldArrayEdu = new EducationVM[number];
            for (int i = 0; i < number; i++)
            {
                Mapper.Initialize(cfg => cfg.CreateMap<Education, EducationVM>());

                OldArrayEdu[i] = Mapper.Map<EducationVM>(educations[i]);
                OldArrayEdu[i].TitleOfDiploma = educations[i].Title;
                OldArrayEdu[i].InstituteUniversity = educations[i].InstituteName;
            }
            ViewBag.educationArray = OldArrayEdu;



            ////////////////////////////////////////
            List<WorkExperience> works = _resumeRepository.GetWorkExperiencesById(id).ToList();
            number = works.Count;
            WorkExperienceVM[] OldArraywork = new WorkExperienceVM[number];
            for (int i = 0; i < number; i++)
            {
                Mapper.Initialize(cfg => cfg.CreateMap<WorkExperience, WorkExperienceVM>());
                OldArraywork[i] = Mapper.Map<WorkExperienceVM>(works[i]);
            }
            ViewBag.workArray = OldArraywork;



            ///////////////////////////////////////
            List<Skill> skills = _resumeRepository.GetSkillsById(id).ToList();
            number = skills.Count;
            SkillsVM[] OldSkillsArray = new SkillsVM[number];
            for (int i = 0; i < number; i++)
            {
                Mapper.Initialize(cfg => cfg.CreateMap<Skill, SkillsVM>());

                OldSkillsArray[i] = Mapper.Map<SkillsVM>(skills[i]);
            }
            ViewBag.skillsArray = OldSkillsArray;



            ///////////////////////////////////////
            List<Certification> allcertifications = _resumeRepository.GetCertificationsById(id).ToList();
            number = allcertifications.Count;
            CertificationsVM[] OldcertificationArray = new CertificationsVM[number];
            for (int i = 0; i < number; i++)
            {
                OldcertificationArray[i] = new CertificationsVM();
                OldcertificationArray[i].CertificationName = allcertifications[i].CertificateName;
                OldcertificationArray[i].CertificateAuthority = allcertifications[i].CertificationAuthority;
                OldcertificationArray[i].LevelCertification = allcertifications[i].CertificationLevel;
            }
            ViewBag.certificatesArray = OldcertificationArray;

            ///////////////////////////////////////
            List<Language> alllanguages = _resumeRepository.GetLanguagesById(id).ToList();
            number = alllanguages.Count;
            LanguageVM[] OldLanguagesArray = new LanguageVM[number];
            for (int i = 0; i < number; i++)
            {
                Mapper.Initialize(cfg => cfg.CreateMap<Language, LanguageVM>());
                OldLanguagesArray[i] = Mapper.Map<LanguageVM>(alllanguages[i]);
            }
            ViewBag.languagesArray = OldLanguagesArray;
            return View();
        }




        public string gethtml(string url)
        {
            string siteContent = string.Empty;  
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())  // Go query google
            using (Stream responseStream = response.GetResponseStream())               // Load the response stream
            using (StreamReader streamReader = new StreamReader(responseStream))       // Load the stream reader to read the response
            {
                siteContent = streamReader.ReadToEnd(); // Read the entire response and store it in the siteContent variable
            }
            return siteContent;
        }
    }
}