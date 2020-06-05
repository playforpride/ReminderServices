using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReminderServices.ViewModel
{

    public class ReminderViewModel
    {
        public int ReminderId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? CategoryId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CategoryName { get; set; }
    }
}
