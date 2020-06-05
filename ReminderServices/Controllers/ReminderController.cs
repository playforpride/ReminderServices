using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReminderServices.Models;
using ReminderServices.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ReminderServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReminderController : ControllerBase
    {
        IReminderRepository reminderRepository;
        public ReminderController(IReminderRepository _reminderRepository)
        {
            reminderRepository = _reminderRepository;
        }

        [HttpGet]
        [Route("GetCategories")]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                var categories = await reminderRepository.GetCategories();
                if (categories == null)
                {
                    return NotFound();
                }

                return Ok(categories);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpGet]
        [Route("GetReminders")]
        public async Task<IActionResult> GetReminders()
        {
            try
            {
                var reminders = await reminderRepository.GetReminders();
                if (reminders == null)
                {
                    return NotFound();
                }

                return Ok(reminders);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetReminder")]
        public async Task<IActionResult> GetReminder(int? reminderId)
        {
            if (reminderId == null)
            {
                return BadRequest();
            }

            try
            {
                var reminder = await reminderRepository.GetReminder(reminderId);

                if (reminder == null)
                {
                    return NotFound();
                }

                return Ok(reminder);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("AddReminder")]
        public async Task<IActionResult> AddReminder([FromBody]Reminder model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var reminderId = await reminderRepository.AddReminder(model);
                    if (reminderId > 0)
                    {
                        return Ok(reminderId);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception)
                {

                    return BadRequest();
                }

            }

            return BadRequest();
        }

        [HttpPost]
        [Route("DeleteReminder")]
        public async Task<IActionResult> DeleteReminder(int? reminderId)
        {
            int result = 0;

            if (reminderId == null)
            {
                return BadRequest();
            }

            try
            {
                result = await reminderRepository.DeleteReminder(reminderId);
                if (result == 0)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }


        [HttpPost]
        [Route("UpdateReminder")]
        public async Task<IActionResult> UpdateReminder([FromBody]Reminder model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await reminderRepository.UpdateReminder(model);

                    return Ok();
                }
                catch (Exception ex)
                {
                    if (ex.GetType().FullName == "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
                    {
                        return NotFound();
                    }

                    return BadRequest();
                }
            }

            return BadRequest();
        }

    }
}