/*
The MIT License (MIT)

Copyright (c) 2015 Natan Hoppe

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using AspNetWithAngularJS.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspNetWithAngularJS.Models
{
    public class VolunteerScheduleRepo
    {
        public VolunteerScheduleRepo()
        {
        }

        public VolunteerScheduleVM GetVolunteersAndSchedules() {
            VolunteerSchedulerEntities db = new VolunteerSchedulerEntities();

            var schedules = db.Schedules.ToList().OrderBy(x => Convert.ToDateTime(x.monthAssigned));

            var volunteers = (from v in db.Volunteers
                             select v).ToList();

            Dictionary<string, SelectList> volunteerAssignment = new Dictionary<string,SelectList>();

            foreach(var schedule in schedules) {
                List<SelectListItem> volunteerSelection = new List<SelectListItem>();
                volunteerSelection.Add(new SelectListItem()
                                            {
                                                Value = "",
                                                Text = "- Select a Volunteer -"
                                            });

                string volunteerIdSelected = "";

                foreach(var volunteer in volunteers) {
                    var selectItem = new SelectListItem() {
                        Value = volunteer.volunteerID.ToString(),
                        Text = volunteer.firstName + " " + volunteer.lastName
                    };

                    if (volunteer.Schedules.Any(s => s.monthAssigned == schedule.monthAssigned)) {
                        volunteerIdSelected = volunteer.volunteerID.ToString();
                    }
                    
                    volunteerSelection.Add(selectItem);
                }

                volunteerAssignment[schedule.monthAssigned] = new SelectList(volunteerSelection, "Value", "Text", volunteerIdSelected);
            }

            VolunteerScheduleVM viewModel = new VolunteerScheduleVM();
            viewModel.schedules = schedules.ToList();
            viewModel.volunteers = volunteerAssignment;

            return viewModel;
        }

        public VolunteerErrorVM GetVolunteerError(Volunteer volunteer)
        {
            VolunteerSchedulerEntities db = new VolunteerSchedulerEntities();
            var volunteerError = (from v in db.Volunteers
                                 from s in v.Schedules
                                 where v.volunteerID == volunteer.volunteerID
                                 select new VolunteerErrorVM
                                 {
                                     VolunteerID = v.volunteerID,
                                     FirstName = v.firstName,
                                     LastName = v.lastName,
                                     MonthAssigned = s.monthAssigned
                                 })
                                 .FirstOrDefault();

            return volunteerError;
        }

        public void AssignVolunteers(Dictionary<string, string> assignment)
        {
            if (assignment != null)
            {
                VolunteerSchedulerEntities db = new VolunteerSchedulerEntities();
                var schedules = from s in db.Schedules
                                select s;

                foreach (var schedule in schedules) {
                
                    if (assignment[schedule.monthAssigned].Length != 0)
                    {
                        int volunteerID = Int32.Parse(assignment[schedule.monthAssigned]);

                        schedule.Volunteers.Clear();

                        var volunteer = (from v in db.Volunteers
                                        where v.volunteerID == volunteerID
                                        select v).FirstOrDefault();
                
                        schedule.Volunteers.Add(volunteer);
                    }
                }
                db.SaveChanges();
            }
        }
    }
}