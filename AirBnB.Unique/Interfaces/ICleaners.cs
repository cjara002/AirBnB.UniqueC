using AirBnB.Unique.Models.Domain;
using AirBnB.Unique.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirBnB.Unique.Interfaces
{
    public interface ICleaners
    {
        int Add(CleanersAddRequest model);
        List<Cleaners> GetAll();
        Paged<Cleaners> Paginate(int pageIndex, int pageSize);
        Paged<Cleaners> SearchPaginate(int pageIndex, int pageSize, string query);
        List<Cleaners> GetById(int Id);
        void Update(CleanersUpdateRequest model, int Id);
        void DeleteById(int Id);
    }
}
