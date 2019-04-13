using BaseMVVM_Service.BaseMVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ModelProject.Models
{
    /// <summary>
    /// Model of account to login
    /// </summary>
    public class AccountModel: BaseDataModel
    {
        private string username;
        private string password;
        
        /// <summary>
        /// Username value of account
        /// </summary>
        
        public string Username
        {
            get => username;
            set
            {
                SetProperty(ref username, value);
            }
        }

        /// <summary>
        /// Password value of account
        /// </summary>
        public string Password
        {
            get => password;
            set
            {
                SetProperty(ref password, value);
            }
        }
                   
    }
}
