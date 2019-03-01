﻿using System;
using System.Collections.Generic;

namespace SoftUni.Models
{
    public class Project
    {
        public Project()
        {
            this.EmployeesProjects = new HashSet<EmployeeProject>();
        }

        public int ProjectId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public  ICollection<EmployeeProject> EmployeesProjects { get; set; }
    }
}
