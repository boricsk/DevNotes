using DevNotes.CommandRelay;
using DevNotes.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace DevNotes.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region WPF Data Communication
        private string? _dateTimeStatusBar;
        public string? DateTimeStatusBar
        {
            get { return _dateTimeStatusBar; }
            set { _dateTimeStatusBar = value; OnPropertyChanged(); }
        }

        private List<DevNoteDataModel>? _devNotes;
        public List<DevNoteDataModel>? DevNotes
        {
            get { return _devNotes; }
            set { _devNotes = value; OnPropertyChanged();}
        }


        #endregion

        #region Command Relay
        public ICommand NewNote { get; }
        private void newNote()
        {
            NewNoteWindow nn = new();
            nn.Show();
        }
        public ICommand ShowSetup { get; }
        private void showSetup()
        {

        }
        public ICommand EditNote { get; }
        private void editNote()
        {

        }

        public ICommand DeleteNote { get; }
        private void deleteNote()
        {

        }
        public ICommand BrowseNotes { get; }
        private void browseNotes()
        {

        }
        public ICommand ChangeSearch { get; }
        private void changeSearch()
        {

        }

        #endregion

        #region Constructor
        public MainWindowViewModel()
        {
            DateTimeStatusBar = DateTime.Now.ToString();
            NewNote = new MainWindowCommandRelay(_ => newNote());
            ShowSetup = new MainWindowCommandRelay(_ => showSetup());
            EditNote = new MainWindowCommandRelay(_ => editNote());
            DeleteNote = new MainWindowCommandRelay(_ => deleteNote());
            BrowseNotes = new MainWindowCommandRelay(_ => browseNotes());
            ChangeSearch = new MainWindowCommandRelay(_ => changeSearch());
            InitTimer();
        }
        #endregion

        private void InitTimer()
        {
            DispatcherTimer timer = new();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += TimerTick;
            timer.Start();
        }
        private void TimerTick(object? sender, EventArgs e)
        {
            DateTimeStatusBar = DateTime.Now.ToString();
        }

    }
}
