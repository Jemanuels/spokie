using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace spokie.Controllers
{
    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
        [HttpGet, Authorize(Roles = "Manager")]
        public IEnumerable<string> Get()
        {
            return new string[] { "Spokie Himself", "Baka Boi" };
        }
    }
}
