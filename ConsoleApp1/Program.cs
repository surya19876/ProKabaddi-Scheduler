using ConsoleApp1.BusinessLogic.ScheduleManager;
using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            int N = int.Parse(Console.ReadLine());
            IScheduleManager manager = new ScheduleManager();
            var schedules = manager.GenerateSchedules(N);
            
            foreach(var schedule in schedules)
            {
                Console.WriteLine("Match: " + schedule.Team1 + " vs " + schedule.Team2 + "Slot: " + schedule.Slot.Location);
            }

            schedules = manager.AssignDates(schedules);

            foreach (var schedule in schedules)
            {
                Console.WriteLine("Match: " + schedule.Team1 + "(Home) vs " + schedule.Team2 + "(Away), Schedule: " + schedule.Slot.Day.ToString("dd/MM/yyyy") + ", Slot = " +schedule.Slot.Match);
            }

        }
    }


}
