using System.Collections.Generic;

namespace WebSiteMjr.Domain.Model
{
    public interface IHolder
    {
        string Name { get; set; }
        IEnumerable<Stuff> Stuff { get; set; }
    }
}
