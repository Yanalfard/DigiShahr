using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.ViewModel
{
    public class AdminOrderSearch
    {
        public int? orderId { get; set; }

        public string phoneNumber { get; set; }

        public DateTime? fromDate { get; set; }

        public DateTime? toDate { get; set; }
    }
}
