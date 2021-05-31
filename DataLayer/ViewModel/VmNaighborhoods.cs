using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.ViewModel
{
    public class VmNaighborhoods
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public VmNaighborhoods()
        {

        }

        public VmNaighborhoods(TblNaighborhood i)
        {
            this.Id = i.Id;
            this.Name = i.Name;
        }
    }
}
