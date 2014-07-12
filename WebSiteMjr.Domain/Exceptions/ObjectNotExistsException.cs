using System;
using WebSiteMjr.Domain.Interfaces.Model;

namespace WebSiteMjr.Domain.Exceptions
{
    public class ObjectNotExistsException<TObject> : Exception where TObject : IMjrException
    {
        public TObject _Object;

        public ObjectNotExistsException()
            : base()
        {
            var t = typeof(TObject);
            _Object = (TObject)Activator.CreateInstance(t);
        }

        public override string Message
        {
            get
            {
                return _Object.ObjectName + " não existente";
            }
        }
    }
}
