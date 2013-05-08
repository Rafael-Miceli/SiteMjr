using System.Data.Entity;
﻿using System;

namespace WebSiteMjr.Models
{
    public class EmployeeContext: DbContext
    {
        public EmployeeContext()
            :base("DefaultConnection")
        {
            
        }
    }

    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime Birth { get; set; }
        
    }
}
