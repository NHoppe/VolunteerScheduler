using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspNetWithAngularJS.Models
{
    public class VolunteerSelectorModel
    {
        public int SelectedVolunteerID { get; set; }
        public IEnumerable<SelectListItem> Volunteers { get; set; }
    }
}