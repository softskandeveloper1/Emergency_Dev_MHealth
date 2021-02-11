using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IService
{
    public interface IPsychiatricProgressNoteService
    {
        IQueryable<mp_psychiatric_progress_note> GetNote();
        Task<List<mp_psychiatric_progress_note>> Get();
        Task<mp_psychiatric_progress_note> Get(long id);
        Task<int> Add(mp_psychiatric_progress_note progess_note);
        int Update(mp_psychiatric_progress_note progess_note);
        Task<int> Remove(long id);
        bool Exists(long id);
        int AddPsychiatricOpd(mp_psychiatric_opd_evaluation evaluation);
        IQueryable<mp_psychiatric_opd_evaluation> GetPsychiatricOpd();
    }
}
