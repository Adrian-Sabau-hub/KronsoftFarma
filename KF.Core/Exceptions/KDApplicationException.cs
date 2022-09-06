using System.Runtime.Serialization;

namespace KF.Core.Exceptions
{
    [Serializable]
    public class KDApplicationException : Exception
    {
        public KDApplicationException() : base()
        {

        }

        public KDApplicationException(string message) : base(message)
        {
        }

        public KDApplicationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected KDApplicationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
