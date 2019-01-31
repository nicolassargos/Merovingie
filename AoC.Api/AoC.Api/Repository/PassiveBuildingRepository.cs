using AoC.BLL;
using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Repository
{
    public class PassiveBuildingRepository : IRepository<Building>
    {
        public void Create(Building entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Building entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Building> GetAll()
        {
            throw new NotImplementedException();
        }

        public Building GetById(int index)
        {
            throw new NotImplementedException();
        }

        public Building Update(Building entity)
        {
            throw new NotImplementedException();
        }
    }
}
