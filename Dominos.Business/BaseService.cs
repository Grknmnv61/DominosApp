using Dominos.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominos.Business
{
    public class BaseService<T>
    {
        public IRepository<T> Repository { get; set; }

        public BaseService(IRepository<T> repository)
        {
            Repository = repository;
        }
    }
}
