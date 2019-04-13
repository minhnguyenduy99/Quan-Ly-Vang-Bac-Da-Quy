using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BaseMVVM_Service.BaseMVVM
{
    /// <summary>
    /// Base command implementation allows binding from UI to View Model layer
    /// </summary>
    /// <typeparam name="Model"></typeparam>
    public class BaseCommand<Model> : ICommand
    {
        private Action<Model> action;
        private Predicate<Model> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove
            {
                if (canExecute != null)
                    CommandManager.RequerySuggested -= value;
            }
        }

        public BaseCommand(Action<Model> action, Predicate<Model> canExecute = null)
        {
            if (action == null)
                throw new ArgumentNullException("Action cannot be null for a command");
            this.action = action;
            this.canExecute = canExecute ?? new Predicate<Model>(model => true);
        }

        public bool CanExecute(object parameter)
        {
            if (canExecute == null)
                return true;
            return canExecute(TranslateParameter(parameter));
        }

        public void Execute(object parameter)
        {
            action(TranslateParameter(parameter));
        }


        private Model TranslateParameter(object parameter)
        {
            Model defaultVal = default(Model);

            //  Data is type of enum
            if (defaultVal != null && typeof(Model).IsEnum)
            {
                if (parameter == null)
                    return default(Model);
                return (Model)Enum.Parse(typeof(Model), (string)parameter);
            }
            return (Model)parameter;
        }
    }

    public class BaseCommand: BaseCommand<object>
    {
        public BaseCommand(Action execute, Func<bool> canExecute = null)
            : base(nothing => execute(), canExecute == null? null: new Predicate<object>(obj => canExecute()))
        {

        }

    }
}
