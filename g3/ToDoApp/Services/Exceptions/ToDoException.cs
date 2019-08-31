using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Services.Exceptions
{
    [Serializable]
    public class ToDoException : Exception
    {
        public string ResourceReferenceProperty { get; set; }

        public ToDoException()
        {
        }

        public ToDoException(string message)
            : base(message)
        {
        }

        public ToDoException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected ToDoException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            ResourceReferenceProperty = info.GetString("ResourceReferenceProperty");
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException(nameof(info));
            info.AddValue("ResourceReferenceProperty", ResourceReferenceProperty);
            base.GetObjectData(info, context);
        }

    }
}
