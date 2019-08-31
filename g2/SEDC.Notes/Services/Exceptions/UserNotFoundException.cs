using System;

namespace Services.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(int userId, string message) : base(message)
        {
            UserId = userId;
        }

        public int UserId { get; }
    }
}
