﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebSiteMjr.Domain.Model.Person;

namespace WebSiteMjr.Domain.Model
{
    public class StuffManufacture: IntId, IMjrException, IObjectWithState
    {
        [Required(ErrorMessage = "Nome é um campo obrigatório")]
        public string Name { get; set; }

        public string ObjectName
        {
            get
            {
                return "Fabricante do material";
            }
        }

        [NotMapped]
        public State State { get; set; }
    }
}