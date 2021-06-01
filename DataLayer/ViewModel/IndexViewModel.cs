using System;
using System.Collections.Generic;
using System.Text;
using DataLayer.Models;

namespace DataLayer.ViewModel
{
    public class IndexViewModel
    {
        public IEnumerable<TblStoreCatagory> AllTopStoreCategory { get; set; }

        public IEnumerable<TblStore> AllStore { get; set; }
        public string Lat { get; set; } = "35.68351380631503";
        public string Lon { get; set; } = "51.392323397681";
    }
}
