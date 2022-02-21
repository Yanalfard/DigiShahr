using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.ViewModel
{
    public class InfoVisitViewModel
    {
        public string date { get; set; }
        public DateTime DateVisit { get; set; }
        public int StorId{ get; set; }
        public int Price { get; set; }
        public int QueueId { get; set; }
        public long RefId { get; set; }
        public string ServiceName { get; set; }
        public string CategoryName { get; set; }
        public string Address { get; set; }
        public string Tell { get; set; }
        public bool IsFinaly { get; set; }
    }
}
