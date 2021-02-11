using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL.IService
{
    public interface IPsychosocialService
    {
        Task<List<mp_psychosocial>> GetPsychosocials();
        Task<mp_psychosocial> GetPsychosocial(int id);
        int AddPsychosocial(mp_psychosocial psychosocial);
        int UpdatePsychosocial(mp_psychosocial psychosocial);
        Task<int> RemovePsychosocial(int id);
        bool PsychosocialExists(long id);
        int AddMedicalHistory(mp_medical_history history);
        int AddMedication(List<mp_medication> medications);
        int AddSurgicalHistories(List<mp_surgical_history> histories);
        int AddFamilyHistory(mp_family_history history);
        int AddSocialRelationship(mp_social_relationship relationship);
        int AddChildren(List<mp_children> childrens);
        int AddEmployment(mp_employment employment);
        int AddSubstanceUse(mp_substance_use substance_use);
        int AddEducationHistory(mp_education_history history);
    }
}