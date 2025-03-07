using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevNotes.Classes
{
    public class DevNoteRegistryManagement
    {
        private string _registrySubkey = @"SOFTWARE\DevNullSec\DevNotes";

        /// <summary>
        /// Write string registry key
        /// </summary>
        /// <param name="registryKey"></param>
        /// <param name="registryValue"></param>
        public void WriteStringRegistryKey(string registryKey, string registryValue)
        {
            using (RegistryKey? key = Registry.CurrentUser.CreateSubKey(_registrySubkey))
            {
                key?.SetValue(registryKey, registryValue);
            }
        }

        /// <summary>
        /// Read string registry key
        /// </summary>
        /// <param name="registryKey"></param>
        /// <returns></returns>
        public string? ReadStringRegistryKey(string registryKey)
        {
            string? ret = null;
            using (RegistryKey? key = Registry.CurrentUser.OpenSubKey(_registrySubkey))
            {
                if (key != null)
                {
                    var value = key.GetValue(registryKey);
                    if (value != null)
                    {
                        ret = value.ToString() ?? null;
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// Save code box background color
        /// </summary>
        /// <param name="rgbValue"></param>
        public void WriteCodeBoxColor((int? red, int? green, int? blue) rgbValue)
        {
            string registryPath = _registrySubkey;
            RegistryKey key = Registry.CurrentUser.CreateSubKey(registryPath);
            if (key != null)
            {
                if (rgbValue.green != null && rgbValue.red != null && rgbValue.blue != null)
                {
                    key.SetValue("CodeBoxRed", rgbValue.red);
                    key.SetValue("CodeBoxGreen", rgbValue.green);
                    key.SetValue("CodeBoxBlue", rgbValue.blue);
                    key.Close();
                }
                else
                {
                    key.SetValue("CodeBoxRed", 255);
                    key.SetValue("CodeBoxGreen", 255);
                    key.SetValue("CodeBoxBlue", 255);
                    key.Close();
                }
            }
        }

        /// <summary>
        /// Read code box background color
        /// </summary>
        /// <returns></returns>
        public (int? red, int? green, int? blue) ReadCodeBoxColor()
        {
            (int? red, int? green, int? blue) rgbValue;

            RegistryKey? key = Registry.CurrentUser.OpenSubKey(_registrySubkey);
            if (key != null)
            {
                rgbValue.red = (int?)key.GetValue("CodeBoxRed", 255);
                rgbValue.green = (int?)key.GetValue("CodeBoxGreen", 255);
                rgbValue.blue = (int?)key.GetValue("CodeBoxBlue", 255);
                key.Close();
                return (rgbValue);
            }
            return (255, 255, 255); // Alapértelmezett értékek            
        }

        /// <summary>
        /// Write embedded code box font family
        /// </summary>
        /// <param name="fontFamily"></param>
        /// <param name="fontSize"></param>
        public void WriteEmbeddedFontFamilyType(string fontFamily, double fontSize)
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(_registrySubkey);
            if (key != null)
            {
                key.SetValue("FontFamily", fontFamily);
                key.SetValue("FontSize", fontSize.ToString());
                key.Close();
            }
        }

        /// <summary>
        /// Read embedded code box font family
        /// </summary>
        /// <returns></returns>
        public (string? FontFamily, double? FontSize) ReadEmbeddedFontFamilyType()
        {
            RegistryKey? key = Registry.CurrentUser.OpenSubKey(_registrySubkey);
            if (key != null)
            {
                string? fontFamily = key.GetValue("FontFamily", "Arial").ToString();
                double? fontSize = double.TryParse(key.GetValue("FontSize", "12").ToString(), out double result) ? result : 12;
                key.Close();
                return (fontFamily, fontSize);
            }
            return ("Arial", 12); // Alapértelmezett értékek
        }

        /// <summary>
        /// Write code box font family
        /// </summary>
        /// <param name="fontFamily"></param>
        /// <param name="fontSize"></param>
        public void WriteFontFamilyType(string fontFamily, double fontSize)
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(_registrySubkey);
            if (key != null)
            {
                key.SetValue("FontFamily", fontFamily);
                key.SetValue("FontSize", fontSize.ToString());
                key.Close();
            }
        }

        /// <summary>
        /// Read code box font family
        /// </summary>
        /// <returns></returns>
        public (string? FontFamily, double? FontSize) ReadFontFamilyType()
        {
            RegistryKey? key = Registry.CurrentUser.OpenSubKey(_registrySubkey);
            if (key != null)
            {
                string? fontFamily = key.GetValue("FontFamily", "Arial").ToString();
                double? fontSize = double.TryParse(key.GetValue("FontSize", "12").ToString(), out double result) ? result : 12;
                key.Close();
                return (fontFamily, fontSize);
            }
            return ("Arial", 12); // Alapértelmezett értékek
        }
    }
}

