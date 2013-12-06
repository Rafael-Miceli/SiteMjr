﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebSiteMjr.Domain.Model
{
    public interface IHolder
    {
        string Name { get; set; }
        IEnumerable<Stuff> Stuff { get; set; }
    }
}
