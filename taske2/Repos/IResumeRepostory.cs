using taske2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
namespace taske2.Repos
{
    public interface IResumeRepostory
    {
        bool AddPersonalInformation(Person person, string file);
        string AddOrUpdateEducation(Education education, int idPerson);
        int GetIdPerson(string firstname, string lastname);
        string AddOrUpdateExperience(WorkExperience workExperience, int IdPerson);
        bool AddSkill(Skill skill, int Idperson);
        bool AddCertification(Certification certification, int IdPerson);
        bool AddLanguage(Language language, int IdPerson);
        Person GetPersonalInformation(int Idperson);

        IQueryable<Education> GetEducationByID(int IdPerson);
        IQueryable<Skill> GetSkillsById(int idPerson);
        IQueryable<Certification> GetCertificationsById(int IdPerson);
        IQueryable<Language> GetLanguagesById(int IdPerson);

    }
}
