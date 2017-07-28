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
    }
}
