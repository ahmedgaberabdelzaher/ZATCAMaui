

namespace ZATCAMAUI.Core.Exceptions
{

    [Serializable]
    public class InternetException : Exception
    {
        public InternetException()
        {
        }
        public InternetException(string ExceptionMessage)
            : base(ExceptionMessage)
        {
        }
    }
}
