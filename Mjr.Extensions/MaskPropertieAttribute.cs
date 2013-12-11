using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebSiteMjr.Domain;

namespace Mjr.Extensions
{
    public class MetadataProvider : DataAnnotationsModelMetadataProvider
    {
        protected override ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes,
                                                        Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
        {
            var metadata = base.CreateMetadata(attributes, containerType, modelAccessor, modelType, propertyName);
            var additionalValues = attributes.OfType<MaskPropertieAttribute>().FirstOrDefault();
            if (additionalValues != null)
            {
                metadata.AdditionalValues.Add("HtmlAttributes", additionalValues);
            }
            return metadata;
        }
    }
}
