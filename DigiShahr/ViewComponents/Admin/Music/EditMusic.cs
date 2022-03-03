using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace DigiShahr.ViewComponents.Admin.Music
{
    public class EditMusic : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            Core _core = new Core();
            return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/Music/Components/EditMusic.cshtml", _core.Music.GetById(id)));
        }
    }
}
