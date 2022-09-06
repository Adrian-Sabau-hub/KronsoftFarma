using System.Runtime.Serialization;

namespace KF.Core.Exceptions
{
    [Serializable]
    public class ItemCannotBeInsertedException : KDApplicationException
    {
        public ItemCannotBeInsertedException(string message) : base(message)
        {
        }
        public ItemCannotBeInsertedException() : base()
        {
        }

        public ItemCannotBeInsertedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ItemCannotBeInsertedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
