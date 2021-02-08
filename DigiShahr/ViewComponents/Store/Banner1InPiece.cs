using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.ViewModel;

namespace DigiShahr.ViewComponents.Store
{
    public class Banner1InPiece : ViewComponent
    {
        Core _core = new Core();

        public async Task<IViewComponentResult> InvokeAsync(int Id)
        {

            return await Task.FromResult((IViewComponentResult)
                View("/Views/Store/Components/Banner1InPiece.cshtml", new UploadBannerViewModel()
                {
                    Ability = _core.Ability.GetById(Id),
                    BannerFile = null,
                    BannerLink = null
                }));
        }
    }
}
