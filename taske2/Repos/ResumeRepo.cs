using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using taske2.Models;


namespace taske2.Repos
{
    public class ResumeRepo:IResumeRepostory
    {
        //DB
        readonly resumerdatabaseEntities DB = new resumerdatabaseEntities();

        public bool AddCertification(Certification certification, int IdPerson)
        {
            try
            {
                int countRecords = 0;
                Person person = DB.People.Find(IdPerson);

                if(person !=null && certification != null)
                {
                    person.Certifications.Add(certification);
                    countRecords = DB.SaveChanges();
                }

                return countRecords > 0 ? true : false;
            }
            catch(DbEntityValidationException dbex)
            {
                throw;
            }
        }

        public bool AddLanguage(Language language, int IdPerson)
        {
            int countrecords = 0;
            Person person = DB.People.Find(IdPerson);
            if(person != null && language != null)
            {
                person.Languages.Add(language);
                countrecords = DB.SaveChanges();

            }

            return countrecords > 0 ? true : false;

            //throw new NotImplementedException();
        }
        public string AddOrUpdateEducation(Education education, int idPerson)
        {
            string msg = "";
            try
            {
                Person person = DB.People.Find(idPerson);
                if (person != null)
                {
                    if (education.IDEducation > 0)
                    {
                        DB.Entry(education).State = System.Data.Entity.EntityState.Modified;
                        DB.SaveChanges();
                        msg = "Education updated";
                    }
                    else
                    {
                        education.IDPerson = idPerson;
                        person.Educations.Add(education);
                        DB.SaveChanges();
                        msg = "Work Updated";
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
            }

            return msg;
            //throw new NotImplementedException();
        }

        public string AddOrUpdateExperience(WorkExperience workExperience, int IdPerson)
        {

            string msg = "";

            Person person = DB.People.Find(IdPerson);
            if(person != null)
            {
                if(workExperience.IDexperience>0)
                {
                    DB.Entry(workExperience).State = System.Data.Entity.EntityState.Modified;
                    DB.SaveChanges();
                    msg = "Work Experience Updated";

                }
                else
                {
                    person.WorkExperiences.Add(workExperience);
                    DB.SaveChanges();
                    msg = "work Experience Added";
                }


            }

            return msg;
            //throw new NotImplementedException();
        }

        public bool AddPersonalInformation(Person person, string file)
        {
            try
            {
                int numberofRecords = 0;
                if(person != null && file != null)
                {
                    person.ProfilePicture = file;
                    DB.People.Add(person);
                    numberofRecords = DB.SaveChanges();
                }
                return numberofRecords > 0 ? true : false;
            }
            catch (DbEntityValidationException dbx)
            {
                Exception raise = dbx;

                foreach(var validations in dbx.EntityValidationErrors)
                {
                    foreach(var val in validations.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validations.Entry.Entity.ToString(),
                            val.ErrorMessage);

                        raise = new InvalidOperationException(message, raise);
                    }
                }
            }
            return false;
            //throw new NotImplementedException();
        }

        public List<Person> GetAllPeople()
        {
            return DB.People.ToList();
        }
        public bool AddSkill(Skill skill, int Idperson)
        {

            int count = 0;

            Person person = DB.People.Find(Idperson);
            if(person != null && skill !=null)
            {
                person.Skills.Add(skill);
                count = DB.SaveChanges();
            }

            return count > 0 ? true : false;
            //throw new NotImplementedException();
        }
        public void deleteperson(int id)
        {
            Person p = DB.People.Where(x => x.IDperson == id).FirstOrDefault();
            List<Certification> c = DB.Certifications.Where(x => x.IDPerson == id).ToList();
            foreach(Certification certification in c)
            {
                DB.Certifications.Remove(certification);
            }
            List<Education> e = DB.Educations.Where(x => x.IDPerson == id).ToList();
            foreach(Education ed in e)
            {
                DB.Educations.Remove(ed);
            }
            List<Language> l = DB.Languages.Where(x => x.IDPerson == id).ToList();
            foreach(Language language in l)
            {
                DB.Languages.Remove(language);
            }
            List<Skill> s = DB.Skills.Where(x => x.IDPerson == id).ToList();
            foreach(Skill skill in s)
            {
                DB.Skills.Remove(skill);
            }
            List<WorkExperience> we= DB.WorkExperiences.Where(x => x.IDPerson == id).ToList();
            foreach(WorkExperience work in we)
            {
                DB.WorkExperiences.Remove(work);
            }
            DB.People.Remove(p);
            DB.SaveChanges();
        }
        public bool userLogin(string username,string password)
        {
            int i = DB.UserPassword.Where(x => x.username == username).Where(y => y.password == password).Count();
            return i == 1 ? true : false;
        }
        public IQueryable<Certification> GetCertificationsById(int IdPerson)
        {
            var languageList = DB.Certifications.Where(w => w.IDPerson == IdPerson);
            return languageList;
        }

        public IQueryable<Education> GetEducationByID(int IdPerson)
        {
            var List = DB.Educations.Where(x => x.IDPerson == IdPerson);
            return List;
            //throw new NotImplementedException();
        }

        public int GetIdPerson(string firstname, string lastname)
        {
            int idSelected = DB.People.Where(p => p.FirstName.ToLower().Equals(firstname.ToLower()))
                                                 .Where(p => p.LastName.ToLower().Equals(lastname.ToLower()))
                                                 .Select(p => p.IDperson).FirstOrDefault();

            return idSelected;
        }
        public int GetIdPersonLast()
        {
            int idSelected = DB.People.OrderByDescending(p => p.IDperson).FirstOrDefault().IDperson;

            return idSelected;
        }

        public IQueryable<Language> GetLanguagesById(int IdPerson)
        {
            var langlist = DB.Languages.Where(x => x.IDPerson == IdPerson);
            return langlist;
        }

        public Person GetPersonalInformation(int Idperson)
        {
            Person per = DB.People.Find(Idperson);
            return per;
        }

        public IQueryable<Skill> GetSkillsById(int idPerson)
        {
            var list = DB.Skills.Where(w => w.IDPerson == idPerson);
            return list;
        }
        public IQueryable<WorkExperience> GetWorkExperiencesById(int idPerson)
        {
            return DB.WorkExperiences.Where(x => x.IDPerson == idPerson);
        }
        private byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
        }

        public bool addadmin(string username,string password)
        {
            UserPassword user = new UserPassword();
            user.username = username;
            user.password = password;
            DB.UserPassword.Add(user);
            int count = DB.SaveChanges();

            return count > 0 ? true : false;
        }

        public bool addpost(string title, string des,string endtime,string admin)
        {
            internship_post post = new internship_post();
            post.Description = des;
            post.End_time = endtime;
            post.Title = title;
            post.Post_time = DateTime.Now.ToShortDateString();
            DB.UserPassword.Where(X => X.username == admin).FirstOrDefault().internship_post.Add(post);
            int count = DB.SaveChanges();

            return count > 0 ? true : false;
            
        }
        public List<internship_post> GetInternship_Posts()
        {
            return DB.internship_post.ToList();
        }
        public bool deletepost(int id)
        {
            internship_post post = DB.internship_post.Where(x => x.Id == id).FirstOrDefault();
            DB.internship_post.Remove(post);
            int count = DB.SaveChanges();
            return count > 0 ? true : false;
        }
        public internship_post GetPostById(int id)
        {
           return DB.internship_post.Where(X => X.Id == id).FirstOrDefault();
        }
        public void applyOnPost(string selectedoption,int idperson)
        {
            Person p =  DB.People.Where(X => X.IDperson == idperson).FirstOrDefault();
            internship_post ip = DB.internship_post.Where(X => X.Title==selectedoption).FirstOrDefault();
            applied_post ap = new applied_post();
            ap.internship_post = ip;
            ap.Person = p;
            DB.applied_post.Add(ap);
            DB.SaveChanges();
        }
        public List<Person> GetApplicants(int id)
        {
            List<applied_post> _Posts = DB.applied_post.Where(X => X.internship_post.Id == id).ToList();
            List<Person> p = new List<Person>();
            for ( int i=0;i<_Posts.Count;i++)
            {
                p.Add(new Person());
                int ids = _Posts[i].Person.IDperson;
                p[i] = DB.People.Where(X => X.IDperson == ids).FirstOrDefault();
                Education e = _Posts[i].Person.Educations.Where(x => x.IDPerson == _Posts[i].Person.IDperson).FirstOrDefault();
                p[i].Address = e.CGPA;
            }
            return p;
        }
    }
}