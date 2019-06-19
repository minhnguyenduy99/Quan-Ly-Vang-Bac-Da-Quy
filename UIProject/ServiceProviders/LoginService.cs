using System;
using System.Collections.Generic;
using System.IO;
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
        private static readonly string DEFAULT_USERNAME = "admin123";
        private static readonly string DEFAULT_PASSWORD = "password123";
        private const string DEFAULT_ACCOUNT_PATH = "./loginaccount.txt";

        private static string UserName;
        private static string Password;

        /// <summary>
        /// Perform a waiting dialog when the login task is processed
        /// </summary>
        /// <param name="task">The login task to be processed</param>
        /// <param name="waitingDialogWindow">The instance of waiting window</param>
        /// <returns>Result of the login task</returns>
        public static async Task<bool?> PerformLoginAndWaitingDialog(Task<bool> task, IWindow waitingDialogWindow)
        {
            if (waitingDialogWindow == null)
                throw new ArgumentNullException();
            task.Start();
            waitingDialogWindow.Show();
            bool? result = await task;
            
            // Close the window after getting the result of login task
            waitingDialogWindow.Close();

            return result;
        }

        /// <summary>
        /// Perform a login task
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns>Result of the login task</returns>
        public static bool Login(string userName, string password)
        {
            LoadLoginAccount();
            return userName == UserName && password == Password;
        }

        private static void LoadLoginAccount()
        {
            if (!File.Exists(DEFAULT_ACCOUNT_PATH))
            {
                FileStream file = File.Create(DEFAULT_ACCOUNT_PATH);

                WriteAccountToFile(file);

                UserName = DEFAULT_USERNAME;
                Password = DEFAULT_PASSWORD;

                file.Close();
                return;
            }
            using (StreamReader reader = new StreamReader(DEFAULT_ACCOUNT_PATH, Encoding.UTF8))
            {
                // user and password
                string stringValue = string.Empty;
                stringValue = reader.ReadToEnd();
                string[] strValues = stringValue.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                // For some reasons the account file is unexpectedly involved invalid data
                if (strValues.Length != 2)
                    return;

                UserName = strValues[0];
                Password = strValues[1];

                reader.Close();
            }
        }

        private static void WriteAccountToFile(FileStream file)
        {
            using (StreamWriter writer = new StreamWriter(file))
            {
                writer.WriteLine(UserName);
                writer.WriteLine(Password);

                writer.Close();
            }
        }
    }
}
