﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIProject.ServiceProviders
{
    public interface IWindow
    {
        object DataContext { get; set; }
        void Show();
        bool? ShowDialog();
        void Close();
    }
}
