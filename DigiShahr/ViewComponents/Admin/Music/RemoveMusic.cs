using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiShahr.ViewComponents.Admin.Music
{
    public class RemoveMusic:ViewComponent
    {
        Core _core = new Core();

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/Music/Components/RemoveMusic.cshtml", _core.Music.GetById(id)));
        }
    }
}
