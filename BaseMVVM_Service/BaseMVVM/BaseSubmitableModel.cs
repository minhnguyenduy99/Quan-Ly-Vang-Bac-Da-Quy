using BaseMVVM_Service.BaseMVVM.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseMVVM_Service.BaseMVVM
{
    /// <summary>
    /// Base model class for model type
    /// </summary>
    public abstract class BaseSubmitableModel : BaseModel, ISubmitable
    {
        protected bool isDataValid;

        /// <summary>
        /// Indicating weither the current data value is valid
        /// </summary>
        public bool IsDataValid
        {
            get => isDataValid;
            set
            {
                bool temp = isDataValid;
                SetProperty(ref isDataValid, value);
                if (temp != value)
                    OnDataValidChanged(new DataValidChangedEventArgs(value));          
            }
        }

        public BaseSubmitableModel()
        {
            IsDataValid = true;
        }

        public event EventHandler<SubmitEventArgs> Submited;

        public virtual bool Submit(SubmitType submitType)
        {
            if (!IsDataValid)
                return false;           
            try
            {
                InternalSubmit(submitType);
                OnSubmited(new SubmitEventArgs(true, submitType));
                return true;
            }
            catch
            {
                //throw new Exception("Submit into database failed");
                OnSubmited(new SubmitEventArgs(false, submitType));
                return false;
            }
        }

        protected void InternalSubmit(SubmitType submitType)
        {
            switch (submitType)
            {
                case SubmitType.Add: Add(); break;
                case SubmitType.Update: Update(); break;
                case SubmitType.Delete: Delete(); break;
            }
        }

        protected virtual void OnSubmited(SubmitEventArgs e)
        {
            Submited?.Invoke(this, e);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Occurs when the value of <see cref="IsDataValid"/> changed
        /// </summary>
        public event EventHandler<DataValidChangedEventArgs> DataValidChanged;

        protected virtual void OnDataValidChanged(DataValidChangedEventArgs e)
        {
            DataValidChanged?.Invoke(this, e);
        }

        public abstract override bool Equals(object obj);
        protected internal abstract void Add();
        protected internal abstract void Update();
        protected internal abstract void Delete();

        /// <summary>
        /// Provides information about <see cref="DataValidChanged"/> event
        /// </summary>
        public class DataValidChangedEventArgs
        {
            public bool DataValid { get; private set; }
            public DataValidChangedEventArgs(bool dataValid)
            {
                DataValid = dataValid;
            }
        }
    }
}
