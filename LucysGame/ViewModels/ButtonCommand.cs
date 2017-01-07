using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;

namespace LucysGame
{
    public class ButtonCommand : ICommand
    {
        private Action WhatToExecute;
        //private Func<bool> WhenToExecute;

        public ButtonCommand(Action _what) //, Func<bool> _when)
        {
            WhatToExecute = _what;
            //WhenToExecute = _when;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
            //return WhenToExecute();
        }

        public void Execute(object parameter)
        {
            WhatToExecute();
        }
    }
}
