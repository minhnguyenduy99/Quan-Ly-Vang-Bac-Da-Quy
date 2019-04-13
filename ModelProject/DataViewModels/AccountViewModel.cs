using Projects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Projects.ModelView.DataViewModels
{
    /// <summary>
    /// The view model of AccountModel class, provides functionality for data query and account check
    /// </summary>
    public class AccountViewModel : BaseDataViewModel<AccountModel>
    {
        public AccountViewModel(AccountModel model) : base(model)
        {
        }

        /// <summary>
        /// Login with input username and password
        /// </summary>
        /// <returns></returns>
        public static bool Login(string username, string password)
        {
            Thread.Sleep(3000);

            if (GetResultCount(username, password) == 1)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Query the account with 
        /// </summary>
        /// <param name="username">The username to query</param>
        /// <param name="password">The password to query</param>
        /// <returns></returns>
        private static int GetResultCount(string username, string password)
        {
            if (username == "Minh" && password == "1234")
                return 1;
            return 0;
        }

        private static string EncryptPassword(string password)
        {
            //  Password is encrypted here

            return null;
        }
    }
}
