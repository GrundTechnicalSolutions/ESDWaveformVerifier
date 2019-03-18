// ---------------------------------------------------------------------------------------------------
//  <copyright file="ImportExtensions.cs" company="Grund Technical Solutions, Inc">
//      Copyright (c) Grund Technical Solutions, Inc. All rights reserved.
//  </copyright>
// ---------------------------------------------------------------------------------------------------
namespace ESDWaveformVerifierDemo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// A static helper class for importing waveform CSV files
    /// </summary>
    internal static class ImportExtensions
    {
        /// <summary>
        /// Shows an OpenFileDialog prompting the user to choose a CSV file, and returns the filepath to open
        /// </summary>
        /// <returns>The filepath to open</returns>
        internal static string PromptUserForCSVFileWithOpenFileDialog()
        {
            // Prompt the user for the Verification Waveform File
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog
            {
                DefaultExt = "csv",
                Filter = "CSV (Comma delimited) (*.csv)|*.csv",
            };

            string demoDataPath = System.IO.Path.Combine(Environment.CurrentDirectory, "DemoData") + @"\";
            if (System.IO.Directory.Exists(demoDataPath))
            {
                dlg.InitialDirectory = demoDataPath;
            }

            bool? dlgresult = dlg.ShowDialog();

            // If the user did not cancel the Open File Dialog
            if (dlgresult.HasValue && dlgresult.Value == true)
            {
                return dlg.FileName;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Converts a CSV file into a Waveform object
        /// </summary>
        /// <param name="fullPathName">The full path name of the CSV file to convert</param>
        /// <returns>A Waveform object representation of the CSV file</returns>
        internal static ESDWaveformVerifier.DataTypes.Waveform ConvertCSVFileToWaveform(string fullPathName)
        {
            try
            {
                string contents = System.IO.File.ReadAllText(fullPathName);
                IEnumerable<string> lines = ImportExtensions.SplitLines(contents);
                List<ESDWaveformVerifier.DataTypes.DataPoint> dataPoints = new List<ESDWaveformVerifier.DataTypes.DataPoint>();
                foreach (string line in lines)
                {
                    if (HasCommasOrTabs(line))
                    {
                        // Split the string into tokens by comma/tab, and remove empty tokens
                        IEnumerable<string> tokens = ImportExtensions.SplitCommaOrTabbedString(line);

                        if (tokens.Count() == 2)
                        {
                            if (double.TryParse(tokens.ElementAt(0), out double x))
                            {
                                if (double.TryParse(tokens.ElementAt(1), out double y))
                                {
                                    dataPoints.Add(new ESDWaveformVerifier.DataTypes.DataPoint(x, y));
                                }
                            }
                        }
                    }
                }

                return dataPoints.Any() ?
                    new ESDWaveformVerifier.DataTypes.Waveform(dataPoints) :
                    null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Returns the input text split into an array, split on newlines.  Empty lines are not included.
        /// </summary>
        /// <param name="text">Text to be split on each new line</param>
        /// <returns>text split into an array, split on newlines.  Empty lines are not included.</returns>
        private static IEnumerable<string> SplitLines(string text)
        {
            return text == null ?
                new string[] { } :
                text.Split(
                    new string[] { "\r\n", "\n" },
                    StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Returns a List of strings that contains the substrings in line that are delimited with any commas or tabs.  Empty (or whitespace) lines are included.
        /// </summary>
        /// <param name="line">The input line of text to split on commas or tabs.</param>
        /// <returns>A List of strings that contains the substrings in line that are delimited with any commas or tabs.</returns>
        private static IEnumerable<string> SplitCommaOrTabbedString(string line)
        {
            if (!string.IsNullOrWhiteSpace(line))
            {
                return line.Split(new char[] { ',', '\t' }).Select(p => p.Trim()).ToList();
            }
            else
            {
                return new List<string>();
            }
        }

        /// <summary>
        /// Returns a value indicating whether the line contains any commas or tabs.
        /// </summary>
        /// <param name="line">The line to check for commas or tabs.</param>
        /// <returns>A value indicating whether the line contains any commas or tabs.</returns>
        private static bool HasCommasOrTabs(string line)
        {
            if (!string.IsNullOrWhiteSpace(line))
            {
                return line.Contains(',') || line.Contains('\t');
            }
            else
            {
                return false;
            }
        }
    }
}
