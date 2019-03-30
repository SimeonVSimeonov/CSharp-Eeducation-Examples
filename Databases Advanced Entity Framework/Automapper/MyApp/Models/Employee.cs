﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyApp.Models
{
    public class Employee
    {
        //first name, last name, salary, birthday and address Only firstname, lastname and salary are required.
        public Employee()
        {
            this.ManagedEmployees = new List<Employee>();
        }

        public int Id { get; set; }

        [Required]
        public string  FirstName { get; set; }

        [Required]
        public string  LastName { get; set; }

        [Range(typeof(decimal), "0.00", "79228162514264337593543950335")]
        public decimal Salary { get; set; }

        public DateTime? Birthday { get; set; }

        public string Address { get; set; }

        public int? ManagerId { get; set; }
        public Employee Manager { get; set; }

        public List<Employee> ManagedEmployees { get; set; }

    }
}