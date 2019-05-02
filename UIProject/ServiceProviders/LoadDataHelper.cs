using BaseMVVM_Service.BaseMVVM.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace UIProject.ServiceProviders
{
    class LoadDataHelper
    {
        public IEnumerable<T> LoadData<T>(IDataLoader<T> loader)
        {
            return loader.LoadData();
        }

        /// <summary>
        /// Load data asynchronously 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="loader">The object that provides implementation of data loading</param>
        /// <param name="asyncHelper">The service provides asynchronous performance</param>
        /// <param name="asyncObject">The object contains the method for perform asynchronously</param>
        /// <returns></returns>
        public static async Task<IEnumerable<T>> LoadDataAsync<T>(IDataLoader<T> loader, IAsynchronousable asyncObject)
        {
            IEnumerable<T> result = null;
            await AsynchorousPerformHelper.PerformAsync(asyncObject, () => result = loader.LoadData());
            return result;
        }
    }
}
