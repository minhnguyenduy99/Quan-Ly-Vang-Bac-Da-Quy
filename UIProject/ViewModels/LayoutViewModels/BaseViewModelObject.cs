using BaseMVVM_Service.BaseMVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIProject.ViewModels.FunctionInterfaces;

namespace UIProject.ViewModels.LayoutViewModels
{
    /// <summary>
    /// A base class of view model object
    /// </summary>
    public abstract class BaseViewModelObject<T> : BaseViewModel<T>, IViewModelObject 
    {
        
        public BaseViewModelObject()
        {
            Load();
        }

        public void Load()
        {
            LoadComponentsInternal();          
            Loaded?.Invoke(this, EventArgs.Empty);
        }

        public void Reload()
        {
            ReloadComponentsInternal();
            Reloaded?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler Loaded;
        public event EventHandler Reloaded;


        protected abstract void LoadComponentsInternal();
        protected abstract void ReloadComponentsInternal();
    }

    /// <summary>
    /// Non-generic version of <see cref="BaseViewModelObject"/>
    /// </summary>
    public abstract class BaseViewModelObject : BaseViewModel, IViewModelObject
    {
        public BaseViewModelObject() : base()
        {
            Load();
        }

        public void Load()
        {
            LoadComponentsInternal();

            Loaded?.Invoke(this, EventArgs.Empty);
        }

        public void Reload()
        {
            ReloadComponentsInternal();
            Reloaded?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler Loaded;
        public event EventHandler Reloaded;


        protected abstract void LoadComponentsInternal();
        protected abstract void ReloadComponentsInternal();
    }
}
