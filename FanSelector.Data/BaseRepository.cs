using System;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using FanSelector.Cache;
using FanSelector.Cache.Impl;
using FanSelector.Data.Data;

namespace FanSelector.Data
{
    public class BaseRepository
    {
        public readonly FsDataContext Db;
        public IRedisRepository RedisRepository;

        public BaseRepository()
        {
            Db = new FsDataContext();
            RedisRepository = new RedisRepository();
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