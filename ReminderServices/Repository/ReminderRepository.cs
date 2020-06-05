using ReminderServices.Models;
using ReminderServices.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReminderServices.Repository
{
    public class ReminderRepository : IReminderRepository
    {
        masterContext db;
        public ReminderRepository(masterContext _db)
        {
            db = _db;
        }

        public async Task<List<Category>> GetCategories()
        {
            if (db != null)
            {
                return await db.Category.ToListAsync();
            }

            return null;
        }

        public async Task<List<ReminderViewModel>> GetReminders()
        {
            if (db != null)
            {
                return await (from r in db.Reminder
                              from c in db.Category
                              where r.CategoryId == c.Id
                              select new ReminderViewModel
                              {
                                  ReminderId = r.ReminderId,
                                  Title = r.Title,
                                  Description = r.Description,
                                  CategoryId = r.CategoryId,
                                  CategoryName = c.Name,
                                  CreatedDate = r.CreatedDate
                              }).ToListAsync();
            }

            return null;
        }

        public async Task<ReminderViewModel> GetReminder(int? reminderId)
        {
            if (db != null)
            {
                return await (from r in db.Reminder
                              from c in db.Category
                              where r.ReminderId == reminderId
                              select new ReminderViewModel
                              {
                                  ReminderId = r.ReminderId,
                                  Title = r.Title,
                                  Description = r.Description,
                                  CategoryId = r.CategoryId,
                                  CategoryName = c.Name,
                                  CreatedDate = r.CreatedDate
                              }).FirstOrDefaultAsync();
            }

            return null;
        }

        public async Task<int> AddReminder(Reminder reminder)
        {
            if (db != null)
            {
                await db.Reminder.AddAsync(reminder);
                await db.SaveChangesAsync();

                return reminder.ReminderId;
            }

            return 0;
        }

        public async Task<int> DeleteReminder(int? reminderId)
        {
            int result = 0;

            if (db != null)
            {
                //Find the post for specific post id
                var reminder = await db.Reminder.FirstOrDefaultAsync(x => x.ReminderId == reminderId);

                if (reminder != null)
                {
                    //Delete that post
                    db.Reminder.Remove(reminder);

                    //Commit the transaction
                    result = await db.SaveChangesAsync();
                }
                return result;
            }

            return result;
        }


        public async Task UpdateReminder(Reminder reminder)
        {
            if (db != null)
            {
                //Delete that post
                db.Reminder.Update(reminder);

                //Commit the transaction
                await db.SaveChangesAsync();
            }
        }
    }
}