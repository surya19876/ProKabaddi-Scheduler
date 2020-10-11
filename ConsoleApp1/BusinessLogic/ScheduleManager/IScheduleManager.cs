using ConsoleApp1.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.BusinessLogic.ScheduleManager
{
    interface IScheduleManager
    {
        List<Schedule> GenerateSchedules(int n);
        List<Schedule> AssignDates(List<Schedule> schedules);
    }
}
