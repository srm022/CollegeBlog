using System;

namespace Blog.Services.Exceptions
{
    public class PasswordsNotIdenticalException : Exception
    {
        public PasswordsNotIdenticalException(string message) : base(message)
        {

        }
    }
}