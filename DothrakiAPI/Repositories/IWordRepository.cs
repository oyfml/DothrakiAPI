using DothrakiAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DothrakiAPI.Repositories
{
    // Repository between DbContext and API controller
    // a.k.a between (data access layer) and (code)
    // API ctrler <-> Repo <-> DbContext <-> Database(SQLite)

    // This is interface for the repository.
    public interface IWordRepository
    {
        // Interface describes operation that can be performed against the database
        Task<IEnumerable<Word>> Get();
        Task<Word> Get(string id);
        Task<Word> Create(Word word);
        Task Update(Word word);
        Task Delete(string id);
    }
}
