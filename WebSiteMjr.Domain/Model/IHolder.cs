using System.Collections.Generic;

namespace WebSiteMjr.Domain.Model
{
    public interface IHolder
    {
        IEnumerator<Stuff> Stuff { get; set; }
    }
}
