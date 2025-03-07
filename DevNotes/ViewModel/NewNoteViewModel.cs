using DevNotes.Classes;
using DevNotes.CommandRelay;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace DevNotes.ViewModel
{
    public class NewNoteViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        private DevNoteRichTextObjectManagement rtf = new();
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region ICommand
        public ICommand BtnLeftAlignClk { get; }
        private void _btnLeftAlignClk(object? param)
        {
            if (param is RichTextBox newNote) { rtf.TextAlign(newNote, 'L'); }
        }
        public ICommand BtnCenterAlignClk { get; }
        private void _btnCenterAlignClk(object? param)
        {
            if (param is RichTextBox newNote) { rtf.TextAlign(newNote, 'C'); }
        }

        public ICommand BtnRightAlignClk { get; }
        private void _btnRightAlignClk(object? param)
        {
            if (param is RichTextBox newNote) { rtf.TextAlign(newNote, 'R'); }
        }

        public ICommand BtnBoldFontClk { get; }
        private void _btnBoldFontClk(object? param)
        {
            if (param is RichTextBox newNote) { rtf.TextStyle(newNote, 'B'); }
        }

        public ICommand BtnItalicFontClk { get; }
        private void _btnItalicFontClk(object? param)
        {
            if (param is RichTextBox newNote) { rtf.TextStyle(newNote, 'I'); }
        }

        public ICommand BtnNormalFontClk { get; }
        private void _btnNormalFontClk(object? param)
        {
            if (param is RichTextBox newNote) { rtf.TextStyle(newNote, 'N'); }
        }


        #endregion

        public NewNoteViewModel()
        {
            BtnLeftAlignClk = new MainWindowCommandRelay(_btnLeftAlignClk);
            BtnCenterAlignClk = new MainWindowCommandRelay(_btnCenterAlignClk);
            BtnRightAlignClk = new MainWindowCommandRelay(_btnRightAlignClk);
            BtnBoldFontClk = new MainWindowCommandRelay(_btnBoldFontClk);
            BtnItalicFontClk = new MainWindowCommandRelay(_btnItalicFontClk);
            BtnNormalFontClk = new MainWindowCommandRelay(_btnNormalFontClk);

        }

    }
}
