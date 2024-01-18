using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public abstract class AbstractRepository
    {
        internal readonly DbContext context;
        protected AbstractRepository(DbContext context)
        {
            this.context = context;
        }
    }
}
