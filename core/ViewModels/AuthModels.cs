using System;

namespace core
{
    public class AuthModels
    {
        // register feedback
        public class RegisterFeedback
        {
            public string Username { get; set; }
            public string Errors { get; set; }
            public bool Success { get; set; }
        }

        // login feedback
        public class LoginFeedback
        {
            public bool Success { get; set; }
			public string Errors { get; set; }
            public Guid AuthKey { get; set; }
            public long UserId { get; set; }
		}

        // register/login user
        public class LoginRegister
        {
            public string Username { get; set; }
            public string Password { get; set; }
            public string ConfirmPassword { get; set; }
        }
    }
}
