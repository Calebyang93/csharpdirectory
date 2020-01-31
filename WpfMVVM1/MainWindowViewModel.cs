using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Utils;

namespace WpfMVVM1
{
  public class MainWindowViewModel : INotifyPropertyChanged
  {
    public string Info { get; set; }

    private ICommand cmdHello;
    public ICommand CmdHello
    {
      get { return cmdHello; }
      set { cmdHello = value; }
    }

    public MainWindowViewModel()
    {
      cmdHello = new RelayCommand(CmdHelloExecute, CmdHelloCanExecute);
    }

    private bool CmdHelloCanExecute(object obj)
    {
      return true;
    }

    private void CmdHelloExecute(object obj)
    {
      Info = string.Format("The time is {0:HH:mm:ss}", DateTime.Now);
      NotifyPropertyChanged("Info");
    }

    #region INotifyPropertyChanged
    [field: NonSerializedAttribute()]
    public virtual event PropertyChangedEventHandler PropertyChanged;
    protected void NotifyPropertyChanged(PropertyChangedEventArgs args)
    {
      PropertyChangedEventHandler handler = PropertyChanged;
      if (handler != null)
        handler(this, args);
    }

    protected void NotifyPropertyChanged(String propertyName)
    {
      this.VerifyPropertyName(propertyName);
      PropertyChangedEventHandler handler = PropertyChanged;
      if (handler != null)
        handler(this, new PropertyChangedEventArgs(propertyName));
    }

    protected void VerifyPropertyName(string propertyName)
    {
      if (string.IsNullOrEmpty(propertyName))  // allowed - means all properties are updated
        return;

      // Verify that the property name matches a real,  
      // public, instance property on this object.
      if (TypeDescriptor.GetProperties(this)[propertyName] == null)
      {
        string msg = "Invalid property name: " + propertyName;
        throw new Exception(msg);
      }
    }
    #endregion

  }
}
