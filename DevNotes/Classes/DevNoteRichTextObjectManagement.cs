using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace DevNotes.Classes
{
    public class DevNoteRichTextObjectManagement
    {
        DevNoteRegistryManagement registry = new DevNoteRegistryManagement();
        /// <summary>
        /// Create a codebox from selected text
        /// </summary>
        /// <param name="newSnippetCodeBox"></param>
        /// <returns></returns>
        public RichTextBox CreateCodeBoxParagraph(RichTextBox richTextContent)
        {
            var rgbColor = registry.ReadCodeBoxColor();
            var selection = richTextContent.Selection;
            if (selection != null)
            {
                var codeBox = new Paragraph(new Run(selection.Text))
                {
                    Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb((byte)(rgbColor.red ?? 255), (byte)(rgbColor.green ?? 255), (byte)(rgbColor.blue ?? 255))),
                    BorderBrush = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0)),
                    BorderThickness = new Thickness(2),
                    FontFamily = new System.Windows.Media.FontFamily("Consolas"),
                    FontSize = 14,
                    Margin = new Thickness(5),
                    Padding = new Thickness(5),
                };

                selection.Text = string.Empty;
                var insertPos = selection.Start;
                insertPos.Paragraph.SiblingBlocks.InsertAfter(insertPos.Paragraph, codeBox);

            }
            return richTextContent;
        }

        /// <summary>
        /// Create list from selected text
        /// </summary>
        /// <param name="newSnippetCodeBox"></param>
        /// <param name="fontFamily"></param>
        /// <param name="fontSize"></param>
        /// <returns></returns>
        public RichTextBox CreateListParagraph(RichTextBox richTextContent, FontFamily fontFamily, double fontSize)
        {
            TextSelection selection = richTextContent.Selection;
            TextRange range = new TextRange(selection.Start, selection.End);

            // Lista létrehozása
            List list = new List();
            list.MarkerStyle = TextMarkerStyle.Disc;

            // Sorok szétválasztása és listaelemmé alakítása
            string[] lines = range.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in lines)
            {
                var paragraph = new Paragraph(new Run(line.Trim()));
                paragraph.FontFamily = fontFamily;
                paragraph.FontSize = fontSize;
                ListItem item = new ListItem(paragraph);
                list.ListItems.Add(item);
            }
            range.Text = string.Empty;
            Block originalParagraph = selection.Start.Paragraph;
            selection.Start.Paragraph.SiblingBlocks.InsertBefore(originalParagraph, list);
            selection.Start.Paragraph.SiblingBlocks.Remove(originalParagraph);
            return richTextContent;
        }

        public RichTextBox TextAlign(RichTextBox richTextContent, char orientation)
        {
            if (richTextContent.Selection != null)
            {
                TextSelection selection = richTextContent.Selection;
                switch (orientation)
                {
                    case 'L': selection.ApplyPropertyValue(Paragraph.TextAlignmentProperty, TextAlignment.Left); return richTextContent;
                    case 'C': selection.ApplyPropertyValue(Paragraph.TextAlignmentProperty, TextAlignment.Center); return richTextContent;
                    case 'R': selection.ApplyPropertyValue(Paragraph.TextAlignmentProperty, TextAlignment.Right); return richTextContent;
                    default: return richTextContent;
                }               
            }
            else
            {
                return richTextContent;
            }
        }

        public RichTextBox TextStyle(RichTextBox richTextContent, char style)
        {
            if (richTextContent.Selection != null)
            {
                TextSelection selection = richTextContent.Selection;
                switch (style)
                {
                    case 'B':
                        {
                            selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Bold);
                            return (richTextContent);
                        }
                    case 'I':
                        {
                            selection.ApplyPropertyValue(Inline.FontStyleProperty, FontStyles.Italic);
                            return (richTextContent);
                        }
                    case 'N':
                        {
                            selection.ApplyPropertyValue(Inline.FontStyleProperty, FontStyles.Normal);
                            selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Normal);
                            return (richTextContent);
                        }
                    default:
                        return (richTextContent);
                }
            }
            else 
            {
                return (richTextContent);
            }
        }

        #region Link management
        /// <summary>
        /// Create hiperlink from selected text
        /// </summary>
        /// <param name="newSnippetCodeBox"></param>
        /// <param name="fontFamily"></param>
        /// <param name="fontSize"></param>
        /// <returns></returns>
        public RichTextBox CreateHiperlinkParagraph(RichTextBox richTextContent, FontFamily fontFamily, double fontSize)
        {
            if (richTextContent.Selection != null && !richTextContent.Selection.IsEmpty)
            {
                var cursor_pos = richTextContent.CaretPosition;
                TextSelection selection = richTextContent.Selection;
                TextRange range = new TextRange(selection.Start, selection.End);
                var hyperlink = new Hyperlink(new Run(range.Text))
                {
                    NavigateUri = new System.Uri(range.Text),
                    FontFamily = fontFamily,
                    FontSize = fontSize,
                };
                hyperlink.RequestNavigate += Hyperlink_RequestNavigate;

                var paragraph = selection.Start.Paragraph;
                range.Text = string.Empty;
                if (paragraph != null)
                {
                    paragraph.Inlines.Add(hyperlink);
                }
                else
                {
                    paragraph = new Paragraph(hyperlink);
                    richTextContent.Document.Blocks.Add(paragraph);
                }
            }
            return richTextContent;
        }

        /// <summary>
        /// Open link in default browser
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            // Hivatkozás megnyitása az alapértelmezett böngészőben
            Process.Start(new ProcessStartInfo
            {
                FileName = e.Uri.AbsoluteUri,
                UseShellExecute = true
            });
            e.Handled = true;
        }
        #endregion
    }
}

