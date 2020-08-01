using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace NoName
{
    public class NotificationVM : BindableBase
    {
        
        private string title;
        private string date;
        private string link;
        Notification notification;
        DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
        public ICommand CommandClickLink
        {
            get;
        }
        public NotificationVM(Notification _notification)
        {
            notification = _notification;
            timer.Interval = TimeSpan.FromSeconds(25);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }
        
        public NotificationVM(string _title, string _date, string _link)
        {
            CommandClickLink = new RelayCommand(obj => commandClickLink());
            title = _title;
            date = _date;
            link = _link;
        }
        private void commandClickLink()
        {
            System.Diagnostics.Process.Start(link);
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            notification.Close();
        }
        public string NotificationTitle
        {
            get
            {
                return NotificationModel.Title(title);
            }
        }
        public string NotificationDate
        {
            get
            {
                return NotificationModel.Date(date);
            }
        }
       
    }
    
}
