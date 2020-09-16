using Jellow2._0.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jellow2._0.ViewModels
{
    public class CompanyViewModel
    {
        public Company company { get; set; }
        public List<Job> allJobs { get; set; }
        public List<Project> allProjects { get; set; }

    }
}