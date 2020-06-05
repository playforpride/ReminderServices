using System;
using System.Collections.Generic;

namespace ReminderServices.Models
{
    public partial class Category
    {
        public Category()
        {
            Reminder = new HashSet<Reminder>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }

        public ICollection<Reminder> Reminder { get; set; }
    }
}
