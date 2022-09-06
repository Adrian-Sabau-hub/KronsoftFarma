using System.Runtime.Serialization;

namespace KF.Core.Exceptions
{
    public class ItemCannotBeDeletedException : KDApplicationException
    {
        public ItemCannotBeDeletedException(string message) : base(message)
        {
        }
        public ItemCannotBeDeletedException() : base()
        {
        }

        public ItemCannotBeDeletedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ItemCannotBeDeletedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
