using Microsoft.AspNetCore.Mvc;
using DataLayer.ViewModel;
using DataLayer.Models;
using Services.Services;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace DigiShahr.Controllers
{
    public class HomeController : Controller
    {
        Core _core = new Core();
        public IActionResult Index()
        {
            IndexViewModel indexViewModel = new IndexViewModel();
            indexViewModel.AllStore = _core.Store.Get();
            indexViewModel.AllTopStoreCategory = _core.StoreCatagory.Get().OrderByDescending(o => o.Id);
            return View(indexViewModel);
        }

        [Route("Piece/{Id}")]
        public async Task<IActionResult> Piece(int Id)
        {
            return await Task.FromResult(View(_core.Store.GetById(Id)));
        }

    }
}
