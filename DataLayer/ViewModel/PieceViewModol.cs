using System;
using System.Collections.Generic;
using System.Text;
using DataLayer.Models;

namespace DataLayer.ViewModel
{
   public class PieceViewModol
    {
        public TblStore Store { get; set; }

        public IEnumerable<TblProduct> Products { get; set; }
    }
}
