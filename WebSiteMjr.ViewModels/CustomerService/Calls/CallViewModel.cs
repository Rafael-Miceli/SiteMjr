using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteMjr.Domain.CustomerService.Model;

namespace WebSiteMjr.ViewModels.CustomerService.Calls
{
    public class IndexCallViewModel
    {
        public IEnumerable<Call> CallsIntoService { get; set; }
        public IEnumerable<Call> CallsAwaiting { get; set; }
    }
}
