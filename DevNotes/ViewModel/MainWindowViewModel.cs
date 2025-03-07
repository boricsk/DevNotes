using DevNotes.CommandRelay;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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



        #endregion

        #region Command Relay
        public ICommand NewNote { get; }
        private void newNote()
        { 
            
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
            NewNote = new MainWindowCommandRelay(_ => newNote());
            ShowSetup = new MainWindowCommandRelay(_ => showSetup());
            EditNote = new MainWindowCommandRelay(_ => editNote());
            DeleteNote = new MainWindowCommandRelay(_ => deleteNote());
            BrowseNotes = new MainWindowCommandRelay(_ => browseNotes());
            ChangeSearch = new MainWindowCommandRelay(_ => changeSearch());
        }
        #endregion
    }
}
