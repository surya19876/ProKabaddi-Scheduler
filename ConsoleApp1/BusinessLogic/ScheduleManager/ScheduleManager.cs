using ConsoleApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp1.BusinessLogic.ScheduleManager
{
    public class ScheduleManager : IScheduleManager
    {
        public List<Schedule> AssignDates(List<Schedule> schedules)
        {
            var date = DateTime.Today.AddDays(1);
            
            int count = schedules.Count;
            int slot = 1;
            while (schedules.Where(e=>e.Slot.Match==null).ToList().Count !=0)
            {
                TryAssignSlot(schedules, date, slot);
                slot = slot == 1 ? 2 : 1;
                if (slot == 1)
                    date = date.AddDays(1);
            }

            return schedules;
        }

        public List<Schedule> GenerateSchedules(int n)
        {
            var schedules = new List<Schedule>();
            for(int i =1; i<=n; i++)
            {
                int team2 = (i + 1) % n; 
                for(int j=1; j<n; j++)
                {
                   
                    team2 = team2 == 0? n : team2;
                    var homeSchedule = new Schedule
                    {
                        Team1 = i,
                        Team2 = team2,
                        Slot = new Slot
                        {
                            Location = Location.Home,

                        }
                    };

                    schedules.Add(homeSchedule);
                    team2 = (team2+1)%n;
                }
            }
            return schedules;
        }

        public void TryAssignSlot(List<Schedule> schedules, DateTime date, int slot)
        {
            var today = schedules.Where(e => e.Slot.Day == date).ToList();
            foreach (var schedule in schedules.Where(e=>e.Slot.Match == null).ToList())
            {
                //First slot
                var yesterday = schedules.Where(e => e.Slot.Day == date.AddDays(-1)).ToList();
                var tomorrow = schedules.Where(e => e.Slot.Day == date.AddDays(1)).ToList();
                if (yesterday.Count == 0 && today.Count == 0 && tomorrow.Count == 0)
                {
                    schedule.Slot = new Slot
                                    {
                                        Day = date,
                                        Location = schedule.Slot.Location,
                                        Match = slot.ToString()
                                    };
                    return;
                }

                if (today.Count != 0 && yesterday.Count!=0 && tomorrow.Count!=0)
                {
                    var slot1 = today.First();
                    var td = today.Where(e => e.Team1 == schedule.Team1 ||
                                        e.Team1 != schedule.Team2 ||
                                        e.Team2 != schedule.Team1 ||
                                        e.Team2 != schedule.Team2).ToList().Count;
                    var y = yesterday.Where(e => e.Team1 == schedule.Team1 ||
                                        e.Team1 != schedule.Team2 ||
                                        e.Team2 != schedule.Team1 ||
                                        e.Team2 != schedule.Team2).ToList().Count;
                    var tm = tomorrow.Where(e => e.Team1 == schedule.Team1 ||
                                        e.Team1 != schedule.Team2 ||
                                        e.Team2 != schedule.Team1 ||
                                        e.Team2 != schedule.Team2).ToList().Count;
                    if (td==0 && y==0 && tm==0)
                    {
                        schedule.Slot = new Slot
                        {
                            Day = date,
                            Location = schedule.Slot.Location,
                            Match = slot.ToString()
                        };
                        return;
                    }
                    return;
                }

                if(yesterday.Count != 0 && tomorrow.Count != 0)
                {
                    var y = yesterday.Where(e => e.Team1 == schedule.Team1 ||
                                       e.Team1 != schedule.Team2 ||
                                       e.Team2 != schedule.Team1 ||
                                       e.Team2 != schedule.Team2).ToList().Count;
                    var tm = tomorrow.Where(e => e.Team1 == schedule.Team1 ||
                                        e.Team1 != schedule.Team2 ||
                                        e.Team2 != schedule.Team1 ||
                                        e.Team2 != schedule.Team2).ToList().Count;
                    if (y == 0 && tm == 0)
                    {
                        schedule.Slot = new Slot
                        {
                            Day = date,
                            Location = schedule.Slot.Location,
                            Match = slot.ToString()
                        };
                        return;
                    }
                    return;
                }

                if (today.Count != 0 && tomorrow.Count != 0)
                {
                    var td = today.Where(e => e.Team1 == schedule.Team1 ||
                                       e.Team1 != schedule.Team2 ||
                                       e.Team2 != schedule.Team1 ||
                                       e.Team2 != schedule.Team2).ToList().Count;
                    var tm = tomorrow.Where(e => e.Team1 == schedule.Team1 ||
                                        e.Team1 != schedule.Team2 ||
                                        e.Team2 != schedule.Team1 ||
                                        e.Team2 != schedule.Team2).ToList().Count;
                    if (td == 0 && tm == 0)
                    {
                        schedule.Slot = new Slot
                        {
                            Day = date,
                            Location = schedule.Slot.Location,
                            Match = slot.ToString()
                        };
                        return;
                    }
                    return;
                }

                if (today.Count != 0 && yesterday.Count != 0)
                {
                    var td = today.Where(e => e.Team1 == schedule.Team1 ||
                                       e.Team1 != schedule.Team2 ||
                                       e.Team2 != schedule.Team1 ||
                                       e.Team2 != schedule.Team2).ToList().Count;
                    var y = yesterday.Where(e => e.Team1 == schedule.Team1 ||
                                        e.Team1 != schedule.Team2 ||
                                        e.Team2 != schedule.Team1 ||
                                        e.Team2 != schedule.Team2).ToList().Count;
                    if (td == 0 && y == 0)
                    {
                        schedule.Slot = new Slot
                        {
                            Day = date,
                            Location = schedule.Slot.Location,
                            Match = slot.ToString()
                        };
                        return;
                    }
                    return;
                }
                if (today.Count != 0)
                {
                    var slot1 = today.First();
                    if(slot1.Team1 != schedule.Team1 && slot1.Team1 != schedule.Team2
                        && slot1.Team2 != schedule.Team1 && slot1.Team2 != schedule.Team2)
                    {
                        schedule.Slot = new Slot
                        {
                            Day = date,
                            Location = schedule.Slot.Location,
                            Match = slot.ToString()
                        };
                        return;
                    }
                }

                if(yesterday.Count!=0)
                {
                    if (today.Count != 0 && yesterday.Count != 0)
                    {
                        var y = yesterday.Where(e => e.Team1 == schedule.Team1 ||
                                            e.Team1 != schedule.Team2 ||
                                            e.Team2 != schedule.Team1 ||
                                            e.Team2 != schedule.Team2).ToList().Count;
                        if (y == 0)
                        {
                            schedule.Slot = new Slot
                            {
                                Day = date,
                                Location = schedule.Slot.Location,
                                Match = slot.ToString()
                            };
                            return;
                        }
                        return;
                    }

                }
                if (tomorrow.Count != 0)
                {
                    if (today.Count != 0 && yesterday.Count != 0)
                    {
                        var tm = tomorrow.Where(e => e.Team1 == schedule.Team1 ||
                                            e.Team1 != schedule.Team2 ||
                                            e.Team2 != schedule.Team1 ||
                                            e.Team2 != schedule.Team2).ToList().Count;
                        if (tm == 0)
                        {
                            schedule.Slot = new Slot
                            {
                                Day = date,
                                Location = schedule.Slot.Location,
                                Match = slot.ToString()
                            };
                            return;
                        }
                        return;
                    }

                }





            }
        }
    }
}
