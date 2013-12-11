using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSiteMjr.Domain
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class MaskPropertieAttribute : Attribute
    {
        public string MaskType
        {
            get;
            set;
        }

        public IDictionary<string, object> HtmlAttributes()
        {
            //Todo: we could use TypeDescriptor to get the dictionary of properties and their values
            IDictionary<string, object> htmlatts = new Dictionary<string, object>();
            if (!String.IsNullOrEmpty(MaskType))
            {
                htmlatts.Add("mask-type", MaskType);
            }
            return htmlatts;
        }
    }
}
