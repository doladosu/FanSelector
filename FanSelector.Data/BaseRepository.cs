using System;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using FanSelector.Data.Data;

namespace FanSelector.Data
{
    public class BaseRepository
    {
        public readonly FsDataContext Db;

        public BaseRepository()
        {
            Db = new FsDataContext();
        }

        public async Task<int> SaveChangesAsyncTask()
        {
            do
            {
                try
                {
                    return await Db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    ex.Entries.Single().Reload();
                    throw;
                }
                catch (DbEntityValidationException e)
                {
                    var dbEntityValidationResult = e.EntityValidationErrors.FirstOrDefault();
                    if (dbEntityValidationResult == null) continue;
                    var dbValidationError = dbEntityValidationResult.ValidationErrors.FirstOrDefault();
                    if (dbValidationError != null)
                        throw new Exception(dbValidationError.ErrorMessage);
                    throw;
                }
            } while (false);
            return -1;
        }
    }
}