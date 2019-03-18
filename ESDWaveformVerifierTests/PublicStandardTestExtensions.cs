using ESDWaveformVerifier.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESDWaveformVerifierTests
{
    internal static class PublicStandardTestExtensions
    {
        public static Waveform ParseWaveformFromString(string waveformString)
        {
            IEnumerable<string> dataPointLines = SplitLines(waveformString);
            List<DataPoint> dataPoints = new List<DataPoint>();
            foreach (string line in dataPointLines)
            {
                string[] tokens = line.Split(',');
                dataPoints.Add(new DataPoint(double.Parse(tokens[0]), double.Parse(tokens[1])));
            }

            return new Waveform(dataPoints);
        }

        /// <summary>
        /// Returns the input text split into an array, split on newlines.  Empty lines are not included.
        /// </summary>
        /// <param name="text">Text to be split on each new line</param>
        /// <returns>text split into an array, split on newlines.  Empty lines are not included.</returns>
        private static IEnumerable<string> SplitLines(this string text)
        {
            return text == null ?
                new string[] { } :
                text.Split(
                    new string[] { "\r\n", "\n" },
                    StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
