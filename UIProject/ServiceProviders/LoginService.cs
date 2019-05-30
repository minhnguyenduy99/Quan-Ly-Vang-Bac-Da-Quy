using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using UIProject.UIConnector;
using UIProject.ViewModels.LayoutViewModels;

namespace UIProject.ServiceProviders
{
    public static class LoginService
    {
        public static async Task<bool?> PerformLoginAndWaitingDialog(Task<bool> task, IWindow waitingDialogWindow)
        {
            if (waitingDialogWindow == null)
                throw new ArgumentNullException();
            task.Start();
            waitingDialogWindow.Show();
            bool? result = await task;
            waitingDialogWindow.Close();
            return result;
        }
    }
}
