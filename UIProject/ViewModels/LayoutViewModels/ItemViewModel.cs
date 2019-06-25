using BaseMVVM_Service.BaseMVVM;
using ModelProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UIProject.Events;
using UIProject.ViewModels.FunctionInterfaces;

namespace UIProject.ViewModels.LayoutViewModels
{

    /// <summary>
    /// View model of item type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ItemViewModel<T> : BaseViewModelObject<T>, ISelectable
    {
        private ICommand selectItemCmd;
        
        /// <summary>
        /// Indicating weither <see cref="ItemViewModel{T}"/> is selected
        /// </summary>
        public bool IsSelected
        {
            get => GetPropertyValue<bool>();
            set
            {
                SetProperty(value);
                if (value==true)
                    OnSelected(new ItemSelectedEventArgs<T>(this));
            }
        }

        /// <summary>
        /// Command executes when <see cref="ItemViewModel{T}"/> is selected
        /// </summary>
        public ICommand SelectItemCommand
        {
            get => selectItemCmd ?? new BaseCommand(OnSelectItemCommandExecute, () => true);
            set => selectItemCmd = value;
        }

        /// <summary>
        /// Initializes an <see cref="ItemViewModel{T}"/> for item and data model
        /// </summary>
        /// <param name="model">The model that associates with this view model</param>
        public ItemViewModel(T model) : base()
        {
            this.Model = model;
            this.IsSelected = false;
        }

        public void Select() { }

        /// <summary>
        /// Event occurs when <see cref="ItemViewModel{T}"/> is selected
        /// </summary>
        public event EventHandler<EventArgs> Selected;
        public override bool Equals(object obj)
        {
            if (obj is ItemViewModel<T>)
            {
                var castObj = (ItemViewModel<T>)obj;
                return Model.Equals(castObj.Model);
            }
            return false;
        }
        protected virtual void OnSelectItemCommandExecute()
        {
            IsSelected = true;
        }
        protected virtual void OnSelected(ItemSelectedEventArgs<T> e)
        {
            Selected?.Invoke(this, e);
        }

        protected override void LoadComponentsInternal() { }

        protected override void ReloadComponentsInternal() { }
    }
}
