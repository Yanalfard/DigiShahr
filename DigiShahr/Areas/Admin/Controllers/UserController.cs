using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.ViewModel;

namespace DigiShahr.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult p_Info(int id)
        {
            return ViewComponent("UserInfo", new { id = id });
        }

        public IActionResult List(Paging paging)
        {
            return ViewComponent("UserList", new { Paging = paging });
        }
    }
}
