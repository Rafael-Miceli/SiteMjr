using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime Birth { get; set; }
        
    }
}
