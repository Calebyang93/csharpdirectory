using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.ToDoTasks
{
    public partial class MainWindowVM : Component
    {
        public MainWindowVM()
        {
            InitializeComponent();
        }

        public MainWindowVM(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
