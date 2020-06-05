using System;
using System.Collections.Generic;

namespace ReminderServices.Models
{
    public partial class Reminder
    {
        public int ReminderId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? CategoryId { get; set; }
        public DateTime? CreatedDate { get; set; }

        public Category Category { get; set; }
    }
}
