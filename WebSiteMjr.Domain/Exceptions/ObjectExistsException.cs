using System;
using WebSiteMjr.Domain.Model;

namespace WebSiteMjr.Domain.Exceptions
{
    public class ObjectExistsException<TObject> : Exception where TObject : IMjrException
    {
        public TObject _Object;

        public ObjectExistsException()
            : base()
        {
            var t = typeof(TObject);
            _Object = (TObject)Activator.CreateInstance(t);
        }

        public override string Message
        {
            get
            {
                return _Object.ObjectName + " já existente";
            }
        }
    }
}
