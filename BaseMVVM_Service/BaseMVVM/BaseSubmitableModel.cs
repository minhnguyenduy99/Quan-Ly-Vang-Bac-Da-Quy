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
        public bool IsDataValid { get; private set; }
        public virtual bool Submit(SubmitType submitType)
        {
            if (!IsDataValid)
                return false;           
            try
            {
                InternalSubmit(submitType);
                return true;
            }
            catch
            {
                throw new Exception("Submit into database failed");
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


        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public abstract override bool Equals(object obj);

        protected internal abstract void Add();
        protected internal abstract void Update();
        protected internal abstract void Delete();
    }
}
