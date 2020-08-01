using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace NoName
{
    class MainVM : BindableBase, INotifyPropertyChanged
    {

        DispatcherTimer timer;
        

        public List<string> listTitle;
        public List<string> listDate;
        public List<string> listLink;
        public List<string> listDescription;

        bool CheckHideWindow = false;

        public ObservableCollection<NewBlock> ListNew { get; } = new ObservableCollection<NewBlock>();

        public ObservableCollection<NewTextBlock> ListBlockNew { get; } = new ObservableCollection<NewTextBlock>();

        public NewBlock SelectedListBox { get; set; } = new NewBlock();

        

        public MainVM()
        {
            CommandTextCreate = new RelayCommand(obj => AddNewPers());
            CommandLink = new RelayCommand(obj => LinkOpen());
            MainCommandLeft = new RelayCommand(obj => mainCommandLeft());
            CommandCloseClick = new RelayCommand(obj => { Application.Current.MainWindow.Close(); });
            CommandHideClick = new RelayCommand(obj => { Application.Current.MainWindow.Hide(); CheckHideWindow = true; });

            StartTimer();
            NewActual();
        }
        #region Commands
        public ICommand CommandCloseClick
        {
            get;
        }
        public ICommand CommandHideClick
        {
            get;
        }
        public ICommand CommandTextCreate
        {
            get;
        }
        private void AddNewPers()
        {
            ListBlockNew.Clear();
            int j = 0;
            foreach (string Title in listTitle)
            {
                if (Title == SelectedListBox.NameHabrNew)
                {
                    ListBlockNew.Add(new NewTextBlock
                    {
                        TextBlockNew = listDescription[j]
                    });
                    break;
                }
                j++;
            }

        }
        public ICommand CommandLink
        {
            get;
        }
        private void LinkOpen()
        {
            int j = 0;
            foreach (string Title in listTitle)
            {
                if (Title == SelectedListBox.NameHabrNew)
                {
                    System.Diagnostics.Process.Start(listLink[j]);
                    break;
                }
                j++;
            }
        }
        public ICommand MainCommandLeft
        {
            get;
        }
        private void mainCommandLeft()
        {
            if (CheckHideWindow == false)
            {
                Application.Current.MainWindow.Hide();
                CheckHideWindow = true;
            }
            else if (CheckHideWindow)
            {
                Application.Current.MainWindow.Show();
                CheckHideWindow = false;
                NewActual();
            }
        }
        #endregion
        private void NewActual()
        {
            MainParsingModel mainParsingModel = new MainParsingModel(20);
            listTitle = mainParsingModel.listTitle;
            listDate = mainParsingModel.listDate;
            listLink = mainParsingModel.listLink;
            listDescription = mainParsingModel.listDescription;
            for (int i = 0; i < listTitle.Count; i++)
            {
                ListNew.Add(new NewBlock
                {
                    NameHabrNew = listTitle[i],
                    DataHabrNew = listDate[i],
                });
            }
        }
        private void AddNewAcrual()
        {
            List<string> NewListTitle;
            List<string> NewListDate;
            List<string> NewListLink;
            List<string> NewListDescription;

            ActualParsingModel actualParsingModel = new ActualParsingModel(this);
            NewListTitle = actualParsingModel.newListTitle;
            NewListDate = actualParsingModel.newListDate;
            NewListLink = actualParsingModel.newListDate;
            NewListDescription = actualParsingModel.newListDescription;

            listTitle.InsertRange(0, NewListTitle);
            listDate.InsertRange(0, NewListDate);
            listLink.InsertRange(0, NewListLink);
            listDescription.InsertRange(0, NewListDescription);

            for (int i = 0; i < NewListTitle.Count; i++)
            {
                if (!CheckHideWindow)
                {
                    ListNew.Insert(i, new NewBlock
                    {
                        NameHabrNew = NewListTitle[i],
                        DataHabrNew = NewListDate[i]
                    });
                }
                else
                {
                    OpenNoticationWindow _openChildWindow = new OpenNoticationWindow(NewListTitle[i], NewListDate[i], NewListLink[i]);
                }
            }

        }
        
        #region Timer
        private void StartTimer()
        {
            timer = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, 20)
            };
            timer.Tick += new EventHandler(TickAsync);
            timer.Start();
        }
        private void TickAsync(object sender, EventArgs e)
        {
             AddNewAcrual();
        }
        #endregion

    }
    
    class OpenNoticationWindow
    {
        private string Title;
        private string Date;
        private string Link;
        public OpenNoticationWindow(string _title, string _date, string _link)
        {
            Title = _title;
            Date = _date;
            Link = _link;

            var displayRootRegistry = (Application.Current as App).displayRootRegistry;

            var notificationVM = new NotificationVM(Title, Date, Link);
            displayRootRegistry.ShowPresentation(notificationVM);
        }
        
    }
}
