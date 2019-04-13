using BaseMVVM_Service.BaseMVVM;
using ModelProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ModelProject.DataViewModels
{
    /// <summary>
    /// View model của tài khoản
    /// </summary>
    public class AccountVM : BaseDataViewModel<AccountModel>
    {
        public AccountVM(AccountModel model) : base(model)
        {
        }

        public static bool Login(string userName, string password)
        {
            Thread.Sleep(3000);
            if (userName.Equals("Minh") && password.Equals("1234"))
                return true;
            return false;
        }
    }
}
