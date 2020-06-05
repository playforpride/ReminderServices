using ReminderServices.Models;
using ReminderServices.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReminderServices.Repository
{
    public interface IReminderRepository
    {
        Task<List<Category>> GetCategories();

        Task<List<ReminderViewModel>> GetReminders();

        Task<ReminderViewModel> GetReminder(int? reminderId);

        Task<int> AddReminder(Reminder reminder);

        Task<int> DeleteReminder(int? reminderId);

        Task UpdateReminder(Reminder reminder);
    }
}