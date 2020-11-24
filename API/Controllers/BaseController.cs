using Data.Layer.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BaseController : Controller
    {

        protected readonly ApplicationDbContext _dbContext;
        public BaseController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
