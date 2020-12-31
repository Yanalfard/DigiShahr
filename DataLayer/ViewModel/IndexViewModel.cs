using System;
using System.Collections.Generic;
using System.Text;
using DataLayer.Models;

namespace DataLayer.ViewModel
{
    public class IndexViewModel
    {
        public IEnumerable<TblStoreCatagory> AllStoreCategoryParent { get; set; }

        public IEnumerable<TblStoreCatagory> AllTopStoreCategory { get; set; }

        public IEnumerable<TblStore> AllStore { get; set; }

    }
}
