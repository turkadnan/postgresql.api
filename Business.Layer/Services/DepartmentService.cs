using Data.Layer.Data;
using Data.Layer.Models;
using Data.Layer.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Layer.Services
{
    public class UserService : DataRepository<User>
    {
        public UserService(ApplicationDbContext dbContext) : base(dbContext) { }
    }
}
