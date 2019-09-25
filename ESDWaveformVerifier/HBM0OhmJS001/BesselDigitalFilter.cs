// ---------------------------------------------------------------------------------------------------
//  <copyright file="BesselDigitalFilter.cs" company="Grund Technical Solutions, Inc">
//      Copyright (c) Grund Technical Solutions, Inc. All rights reserved.
//  </copyright>
// ---------------------------------------------------------------------------------------------------

namespace ESDWaveformVerifier.HBM0OhmJS001
{
    using ESDWaveformVerifier.DataTypes;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// A class that provides filtering calculations using the Bessel algorithm.
    /// </summary>
    internal static class BesselDigitalFilter
    {
        /// <summary>
        /// Key: [long] Sampling Frequency in Hz
        /// Value:
        /// Item1: [double] Coefficient A
        /// Item2: [double] Coefficient B
        /// Item3: [double] Coefficient C
        /// Item4: [double] Coefficient D
        /// Item5: [integer] Shift
        /// </summary>
        private static Dictionary<long, Tuple<double, double, double, double, int>> samplingCoefficientSets = new Dictionary<long, Tuple<double, double, double, double, int>>()
        {
            { 1000000000, new Tuple<double, double, double, double, int>(0.149411432880668000, 0.003204035200000000, -0.159857410700000000, -0.038638087600000000, 1) },
            { 1250000000, new Tuple<double, double, double, double, int>(0.095744725535330900, 0.027325420000000000, -0.214565101600000000,  0.421281877600000000, 2) },
            { 2000000000, new Tuple<double, double, double, double, int>(0.034965550951425800, 0.113363861500000000, -0.610857942900000000,  1.217769673900000000, 3) },
            { 2500000000, new Tuple<double, double, double, double, int>(0.020896405559358200, 0.176568401600000000, -0.865172933400000000,  1.521433287300000000, 3) },
            { 4000000000, new Tuple<double, double, double, double, int>(0.006577303289247610, 0.340298878700000000, -1.415272524200000000,  2.022355219100000000, 6) },
            { 5000000000, new Tuple<double, double, double, double, int>(0.003688644050680860, 0.422675065100000000, -1.655051835400000000,  2.202867617900000000, 7) },
            { 10000000000, new Tuple<double, double, double, double, int>(0.000558936637177688, 0.650676563900000000, -2.241198996000000000,  2.586050939000000000, 13) },
            { 20000000000, new Tuple<double, double, double, double, int>(0.000077342942965062, 0.806732068400000000, -2.596490021300000000,  2.789139209300000000, 27) },
        };

        /// <summary>
        /// Returns a new waveform that has been filtered using the Bessel algorithm.
        /// </summary>
        /// <param name="waveform">The original waveform.</param>
        /// <returns>The Bessel filtered waveform.</returns>
        internal static Waveform FilterWaveform(Waveform waveform)
        {
            // If there are not more than 3 data points, then no bessel filtering can occur
            if (waveform == null || waveform.DataPoints.Count() <= 3)
            {
                return new Waveform(waveform.DataPoints);
            }

            double samplingFrequency = waveform.SamplingFrequency();

            int paddingCount = 3;

            List<DataPoint> inputDataPoints = new List<DataPoint>(waveform.DataPoints);

            // The filter should be started in the prepulse noise with y[0]=y[1]=y[2]=average of (x[0],x[1],x[2]).
            double paddingY = 0.0;
            for (int i = 0; i < paddingCount; i++)
            {
                paddingY += inputDataPoints[i].Y;
            }

            paddingY = paddingY / (double)paddingCount;
            for (int i = 0; i < paddingCount; i++)
            {
                inputDataPoints.Insert(0, new DataPoint(inputDataPoints[0].X, paddingY));
            }

            List<DataPoint> outputDataPoints = new List<DataPoint>(inputDataPoints.Count);

            Tuple<double, double, double, double, int> coefficientSet = BesselDigitalFilter.GetCorrectSamplingCoefficientSet(samplingFrequency);
            double a1 = coefficientSet.Item1;
            double a3 = coefficientSet.Item1 * 3.0;
            double b = coefficientSet.Item2;
            double c = coefficientSet.Item3;
            double d = coefficientSet.Item4;
            int delay = coefficientSet.Item5;

            // Calculate how much each datapoint's X value must be shifted by
            double horizontalShift = (1 / samplingFrequency) * delay * -1;

            for (int n = 0; n < inputDataPoints.Count; n++)
            {
                DataPoint newDataPoint;
                if (n < 3)
                {
                    newDataPoint = new DataPoint(inputDataPoints[n].X + horizontalShift, inputDataPoints[n].Y);
                }
                else
                {
                    double y =
                        (a1 * inputDataPoints[n - 3].Y) +
                        (a3 * inputDataPoints[n - 2].Y) +
                        (a3 * inputDataPoints[n - 1].Y) +
                        (a1 * inputDataPoints[n - 0].Y) +
                        (b * outputDataPoints[n - 3].Y) +
                        (c * outputDataPoints[n - 2].Y) +
                        (d * outputDataPoints[n - 1].Y);

                    newDataPoint = new DataPoint(inputDataPoints[n].X + horizontalShift, y);
                }

                outputDataPoints.Add(newDataPoint);
            }

            // Remove the 3 data points that were padding to get the digital filter started
            outputDataPoints.RemoveRange(0, paddingCount);

            return new Waveform(outputDataPoints);
        }

        /// <summary>
        /// Returns sampling coefficients.
        /// </summary>
        /// <param name="samplingFrequency">The sampling frequency.</param>
        /// <returns>The sampling coefficients.</returns>
        /// <remarks>
        /// Item1: [double] Coefficient A
        /// Item2: [double] Coefficient B
        /// Item3: [double] Coefficient C
        /// Item4: [double] Coefficient D
        /// Item5: [integer] Shift
        /// </remarks>
        private static Tuple<double, double, double, double, int> GetCorrectSamplingCoefficientSet(double samplingFrequency)
        {
            IEnumerable<long> increasingSamplingFrequencyKeys =
                from sf in BesselDigitalFilter.samplingCoefficientSets.Keys
                orderby sf ascending
                select sf;

            foreach (long sf in increasingSamplingFrequencyKeys)
            {
                if (samplingFrequency <= sf)
                {
                    return BesselDigitalFilter.samplingCoefficientSets[sf];
                }
            }

            // Since the sampling frequency is larger than any of the sets, return the largest set
            return BesselDigitalFilter.samplingCoefficientSets[increasingSamplingFrequencyKeys.Last()];
        }
    }
}
