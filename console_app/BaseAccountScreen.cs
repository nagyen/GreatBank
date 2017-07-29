using System;
using core;
using core.DbModels;

namespace console_app
{
    public abstract class BaseAccountScreen: BaseScreen
    {
        protected User User { get; set; }
        protected Guid AuthKey { get; set; }
		protected AuthService AuthService { get; set; }
		protected UserTransactionService UserTransactionService { get; set; }

        public BaseAccountScreen(long userId, Guid authKey)
        {
			UserTransactionService = new UserTransactionService();
			this.User = UserTransactionService.GetUser(userId);

			this.AuthKey = authKey;
			AuthService = new AuthService();
        }
    }
}
