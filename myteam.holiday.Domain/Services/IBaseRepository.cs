using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using myteam.holiday.Domain.Models;

namespace myteam.holiday.Domain.Services
{
    public interface IBaseRepository
    {
        Task<int> CreateBase(Base baseModel);
        Task<bool> DeleteBase(string guId);
        Task<bool> UpdateBase(Base baseModel);
        Task<IEnumerable<Base>> GetBases();
    }
}