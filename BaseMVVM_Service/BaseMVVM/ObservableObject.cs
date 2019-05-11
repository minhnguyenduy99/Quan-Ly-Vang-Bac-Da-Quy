﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BaseMVVM_Service.BaseMVVM
{
    public abstract class ObservableObject: INotifyPropertyChanged
    {
        private readonly Dictionary<string, object> propertyBackingDictionary = new Dictionary<string, object>();

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected virtual void SetProperty<T>(T value, [CallerMemberName] string propertyName = null)
        {
            if (!propertyBackingDictionary.ContainsKey(propertyName))
            {
                propertyBackingDictionary.Add(propertyName, value);
                OnPropertyChanged(propertyName);
                return;
            }
            object oldValue = propertyBackingDictionary[propertyName];
            if (!(oldValue is T))
                throw new ArgumentException("The type of value is not compatible with type of object");
            if (!EqualityComparer<T>.Default.Equals((T)oldValue, value))
            {
                propertyBackingDictionary[propertyName] = value;
                OnPropertyChanged(propertyName);
                return;
            }
        }

        protected virtual T GetPropertyValue<T>([CallerMemberName] string propertyName = null)
        {
            if (propertyName == null)
                throw new Exception("Property not found");
            if (propertyBackingDictionary.TryGetValue(propertyName, out object value))
            {
                return (T)value;
            }
            return default(T);
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public static Object GetPropValue(Object obj, String name)
        {
            foreach (String part in name.Split('.'))
            {
                if (obj == null) { return null; }

                Type type = obj.GetType();
                PropertyInfo info = type.GetProperty(part);
                if (info == null) { return null; }

                obj = info.GetValue(obj, null);
            }
            return obj;
        }
    }
}
