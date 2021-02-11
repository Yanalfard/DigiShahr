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
    public class ProductsInPiece : ViewComponent
    {
        Core _core = new Core();

        public async Task<IViewComponentResult> InvokeAsync(int CatId, int StoreId)
        {
            if (CatId == 0)
            {
                IEnumerable<TblProduct> products = _core.Product.Get(p => p.StoreId == StoreId);
                return await Task.FromResult((IViewComponentResult)View("/Views/Home/Components/ProductsInPiece.cshtml", products));
            }
            else
            {
                IEnumerable<TblProduct> products = _core.Product.Get(p => p.StoreId == StoreId && p.StoreCatagoryId == CatId);
                return await Task.FromResult((IViewComponentResult)View("/Views/Home/Components/ProductsInPiece.cshtml", products));
            }

           
        }
    }
}
