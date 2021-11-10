using AirBnBUnique.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirBnBUnique.Interfaces
{
    public interface IProfile
    {
        List<Profile> GetAll();
    }
}
