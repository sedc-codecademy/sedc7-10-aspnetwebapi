using System;

namespace Facebook.WebApi2_0.Exceptions
{
    public class UserNotFoundException : ArgumentException
    {
        public UserNotFoundException(string message) : base(message)
        {

        }

        public UserNotFoundException(string message, string paramName) 
            : base(message, paramName)
        {

        }
    }
}
