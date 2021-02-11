using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL.IService
{
    public interface IProgressNoteService
    {
        IQueryable<mp_progress_note> Get();
        Task<List<mp_progress_note>> GetProgressNotes();
        Task<mp_progress_note> GetProgressNote(int id);
        Task<int> AddProgressNote(mp_progress_note note);
        int UpdateProgressNote(mp_progress_note note);
        Task<int> RemoveProgressNote(long id);
        bool ProgressNoteExists(long id);
    }
}