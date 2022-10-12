using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using myteam.holiday.Domain.Models;
using myteam.holiday.Domain.Services;

namespace myteam.holiday.EntityFramework.Services
{
    public class BaseRepository : IBaseRepository
    {
        public Task<int> CreateBase(Base baseModel)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteBase(string guId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Base>> GetBases()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateBase(Base baseModel)
        {
            throw new NotImplementedException();
        }
    }
}