// ---------------------------------------------------------------------------------------------------
//  <copyright file="WaveformExtensions.cs" company="Grund Technical Solutions, Inc">
//      Copyright (c) Grund Technical Solutions, Inc. All rights reserved.
//  </copyright>
// ---------------------------------------------------------------------------------------------------
namespace ESDWaveformVerifier.DataTypes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// An extension class that provides additional Waveform functionality
    /// </summary>
    internal static class WaveformExtensions
    {
        /// <summary>
        /// Returns the FifthDegreePolynomial formula for the waveform using the Least Squares Fit method
        /// </summary>
        /// <param name="input">The Waveform to find the Least Squares Fit FifthDegreePolynomial of</param>
        /// <returns>The FifthDegreePolynomial formula for the waveform using the Least Squares Fit method</returns>
        internal static FifthDegreePolynomial LeastSquaresFit(this Waveform input)
        {
            if (input != null && input.DataPoints.Count() > 0)
            {
                double s1 = input.DataPoints.Count();
                double sx = 0;
                double sy = 0;
                double sxx = 0;
                double sxy = 0;
                foreach (DataPoint pt in input.DataPoints)
                {
                    sx += pt.X;
                    sy += pt.Y;
                    sxx += pt.X * pt.X;
                    sxy += pt.X * pt.Y;
                }

                // Solve for m and b.
                double m = ((sxy * s1) - (sx * sy)) / ((sxx * s1) - (sx * sx));
                double b = ((sxy * sx) - (sy * sxx)) / ((sx * sx) - (s1 * sxx));

                return new FifthDegreePolynomial(b, m, 0, 0, 0, 0);
            }
            else
            {
                return new FifthDegreePolynomial(0, 0, 0, 0, 0, 0);
            }
        }

        /// <summary>
        /// Returns a Tuple(double, double) that are the constants 'a' (Item1) and 'b' (Item2) of the exponential fit for the input Waveform
        /// </summary>
        /// <param name="input">The Waveform to find the Exponential Fit constants of</param>
        /// <returns>Tuple(double, double) that are the constants 'a' (Item1) and 'b' (Item2) of the exponential fit for the input Waveform</returns>
        internal static Tuple<double, double> ExponentialFit(this Waveform input)
        {
            double n = Convert.ToDouble(input.DataPoints.Count());

            double sumX = input.DataPoints.Sum(dp => dp.X);
            double sumXSq = input.DataPoints.Sum(dp => dp.X * dp.X);
            double sumLnY = input.DataPoints.Sum(dp => System.Math.Log(dp.Y));
            double sumXLnY = input.DataPoints.Sum(dp => dp.X * System.Math.Log(dp.Y));

            double nsumXSqminusSumXSq = (n * sumXSq) - (sumX * sumX);
            double a = ((sumLnY * sumXSq) - (sumX * sumXLnY)) / nsumXSqminusSumXSq;
            double b = ((n * sumXLnY) - (sumX * sumLnY)) / nsumXSqminusSumXSq;

            return new Tuple<double, double>(a, b);
        }

        /// <summary>
        /// Creates a Waveform using the time values for X, and the Exponential Fit constants 'a' and 'b' to calculate Y
        /// </summary>
        /// <param name="a">The Exponential Fit constant 'a'</param>
        /// <param name="b">The Exponential Fit constant 'b'</param>
        /// <param name="timeValues">The values to use for each DataPoint's X value (time)</param>
        /// <returns>A Waveform using the time values for X, and the Exponential Fit constants 'a' and 'b' to calculate Y</returns>
        internal static Waveform CreateExponentialFitWaveform(double a, double b, IEnumerable<double> timeValues)
        {
            double expA = System.Math.Exp(a);

            List<DataPoint> expFitDataPoints = new List<DataPoint>();
            foreach (double x in timeValues)
            {
                double y = expA * System.Math.Exp(b * x);
                expFitDataPoints.Add(new DataPoint(x, y));
            }

            return new Waveform(expFitDataPoints);
        }

        /// <summary>
        /// Returns the average Y value of the Waveform, or 0.0 if no DataPoints exist
        /// </summary>
        /// <param name="input">The Waveform to find the average Y value of</param>
        /// <returns>the average Y value of the Waveform, or 0.0 if no DataPoints exist</returns>
        internal static double Average(this Waveform input)
        {
            return input.DataPoints.Count() > 0 ?
                (from dp in input.DataPoints
                 select dp.Y).Average() :
                 0.0;
        }

        /// <summary>
        /// Returns a new Waveform which only contains the points who's X-axis resides between the boundaries.
        /// </summary>
        /// <param name="input">The original waveform.</param>
        /// <param name="boundary1">The first waveform boundary.</param>
        /// <param name="boundary2">The second waveform boundary.</param>
        /// <returns>The gated waveform.</returns>
        internal static Waveform Gates(this Waveform input, double boundary1, double boundary2)
        {
            double lowerbound = boundary1 < boundary2 ? boundary1 : boundary2;
            double upperbound = boundary1 >= boundary2 ? boundary1 : boundary2;

            List<DataPoint> points = new List<DataPoint>();
            foreach (DataPoint point in input.DataPoints)
            {
                if (point.X >= lowerbound && point.X <= upperbound)
                {
                    points.Add(new DataPoint(point.X, point.Y));
                }
            }

            return new Waveform(points);
        }

        /// <summary>
        /// Returns the first DataPoint with the maximum Y value of the Waveform, or [0, 0] if no DataPoints exist
        /// </summary>
        /// <param name="waveform">The Waveform to find the first DataPoint with maximum Y value of</param>
        /// <returns>the first DataPoint with maximum Y value of the Waveform, or [0, 0] if no DataPoints exist</returns>
        internal static DataPoint Maximum(this Waveform waveform)
        {
            if (waveform.DataPoints.Any())
            {
                DataPoint maxDataPoint = waveform.DataPoints.First();
                foreach (var dataPoint in waveform.DataPoints)
                {
                    if (dataPoint.Y > maxDataPoint.Y)
                    {
                        maxDataPoint = dataPoint;
                    }
                }

                return maxDataPoint;
            }
            else
            {
                return new DataPoint(0, 0);
            }
        }

        /// <summary>
        /// Returns the first DataPoint with the minimum Y value of the Waveform, or [0, 0] if no DataPoints exist
        /// </summary>
        /// <param name="waveform">The Waveform to find the first DataPoint with minimum Y value of</param>
        /// <returns>the first DataPoint with minimum Y value of the Waveform, or [0, 0] if no DataPoints exist</returns>
        internal static DataPoint Minimum(this Waveform waveform)
        {
            if (waveform.DataPoints.Any())
            {
                DataPoint minDataPoint = waveform.DataPoints.First();
                foreach (var dataPoint in waveform.DataPoints)
                {
                    if (dataPoint.Y < minDataPoint.Y)
                    {
                        minDataPoint = dataPoint;
                    }
                }

                return minDataPoint;
            }
            else
            {
                return new DataPoint(0, 0);
            }
        }

        /// <summary>
        /// Returns the sampling frequency of the waveform, or 0.0 if less than two DataPoints exist
        /// </summary>
        /// <param name="waveform">The Waveform to find the sampling frequency of</param>
        /// <returns>the sampling frequency of the Waveform, or 0.0 if less than two DataPoints exist</returns>
        internal static double SamplingFrequency(this Waveform waveform)
        {
            if (waveform.DataPoints.Count() >= 2)
            {
                double sampleTime = waveform.SamplingTime();
                if (sampleTime > 0.0)
                {
                    return System.Math.Round(1.0 / sampleTime);
                }
                else
                {
                    return 0.0;
                }
            }
            else
            {
                return 0.0;
            }
        }

        /// <summary>
        /// Returns the sampling time between each data point, or 0.0 if less than two DataPoints exist
        /// </summary>
        /// <param name="waveform">The Waveform to find the sampling time of</param>
        /// <returns>the sampling time between each data point, or 0.0 if less than two DataPoints exist</returns>
        internal static double SamplingTime(this Waveform waveform)
        {
            if (waveform.DataPoints.Count() >= 2)
            {
                int upperBound = 1;
                double firstDPTime = waveform.DataPoints.ElementAt(0).X;
                double lastDPTime = firstDPTime;
                do
                {
                    if (waveform.DataPoints.Count() > upperBound)
                    {
                        lastDPTime = waveform.DataPoints.ElementAt(upperBound).X;
                    }

                    upperBound++;
                } while (firstDPTime == lastDPTime && upperBound < waveform.DataPoints.Count());

                return (lastDPTime / firstDPTime) / (double)upperBound;
            }
            else
            {
                return 0.0;
            }
        }

        /// <summary>
        /// Returns a new Waveform which has the Y-Axis scaled by the amount specified
        /// </summary>
        /// <param name="waveform">The original waveform.</param>
        /// <param name="scaleFactor">The scaling factor.</param>
        /// <returns>The scaled waveform.</returns>
        internal static Waveform ScaleVertically(this Waveform waveform, double scaleFactor)
        {
            return new Waveform(
                from dp in waveform.DataPoints
                select new DataPoint(dp.X, dp.Y * scaleFactor));
        }

        /// <summary>
        /// Returns the DataPoint at the Y-value threshold (could be interpolated), or [0, 0] if never crossed
        /// </summary>
        /// <param name="waveform">The waveform to find the first data point(s) with a Y-value that crosses the threshold</param>
        /// <param name="yThreshold">The Y-value threshold to find the first data point crossing</param>
        /// <param name="findFirstThreshold">A value indicating whether the first threshold crossing should be returned (false will return the last)</param>
        /// <returns>the DataPoint at the Y-value threshold (could be interpolated), or [0, 0] if never crossed</returns>
        internal static DataPoint? DataPointAtYThreshold(this Waveform waveform, double yThreshold, bool findFirstThreshold)
        {
            if (!waveform.DataPoints.Any())
            {
                return null;
            }

            IEnumerable<int> thresholdCrossingIndexes = waveform.ThresholdCrossingIndexes(yThreshold, findFirstThreshold);
            if (thresholdCrossingIndexes.Count() == 2)
            {
                // The threshold is crossed between a pair of data points.  Interpolate X and Y
                DataPoint dp1 = waveform.DataPoints.ElementAt(thresholdCrossingIndexes.ElementAt(0));
                DataPoint dp2 = waveform.DataPoints.ElementAt(thresholdCrossingIndexes.ElementAt(1));

                double thresholdX = DoubleRangeExtensions.EquivalentValueInNewRange(yThreshold, dp1.Y, dp2.Y, dp1.X, dp2.X);
                return new DataPoint(thresholdX, yThreshold);
            }
            else if (thresholdCrossingIndexes.Count() == 1)
            {
                // A Data Point Y-value is exactly at the threshold
                DataPoint dp = waveform.DataPoints.ElementAt(thresholdCrossingIndexes.First());
                return new DataPoint(dp.X, dp.Y);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Returns a new Waveform which only contains the points who's X-axis resides after or equal to the boundary
        /// </summary>
        /// <param name="waveform">The original waveform.</param>
        /// <param name="boundary">The time boundary to trim everything before</param>
        /// <returns>The trimmed waveform.</returns>
        internal static Waveform TrimStart(this Waveform waveform, double boundary)
        {
            return new Waveform(
                from dp in waveform.DataPoints
                where dp.X >= boundary
                select dp);
        }

        /// <summary>
        /// Returns a new Waveform which only contains the data points who's X-axis resides before or equal to the boundary
        /// </summary>
        /// <param name="waveform">The original waveform.</param>
        /// <param name="boundary">The time boundary to trim everything after</param>
        /// <returns>The trimmed waveform.</returns>
        internal static Waveform TrimEnd(this Waveform waveform, double boundary)
        {
            return new Waveform(
                from dp in waveform.DataPoints
                where dp.X <= boundary
                select dp);
        }

        /// <summary>
        /// Returns the DataPoint with the maximum positive/negative Ringing current (Ring1/Ring2) of the decaying HBM waveform within the JS-001 specification
        /// </summary>
        /// <param name="input">The Waveform to find the maximum positive Ringing current (Ring1) of</param>
        /// <param name="leastSquaresFitLine">The least squares fit line equivalent to the waveform</param>
        /// <param name="noiseAmount">The amount of noise that should be removed</param>
        /// <param name="isPositiveRing">A value indicating whether to return the maximum positive DataPoint, or the negative</param>
        /// <returns>The maximum positive Ringing current (Ring1) of the decaying HBM waveform within the JS-001 specification</returns>
        internal static DataPoint DataPointAtHBM0OhmJS001MaximumRing(this Waveform input, FifthDegreePolynomial leastSquaresFitLine, double noiseAmount, bool isPositiveRing)
        {
            if (input.DataPoints.Any())
            {
                DataPoint maxPositiveDataPoint = new DataPoint(input.DataPoints.First().X, input.DataPoints.First().Y);
                DataPoint maxNegativeDataPoint = new DataPoint(input.DataPoints.First().X, input.DataPoints.First().Y);
                double maxPositiveRingValue = 0;
                double maxNegativeRingValue = 0;

                foreach (DataPoint dp in input.DataPoints)
                {
                    double leastSquaresFitValue = leastSquaresFitLine.Evaluate(dp.X);
                    double difference = dp.Y - leastSquaresFitValue;
                    if (difference >= 0)
                    {
                        if (difference > maxPositiveRingValue)
                        {
                            maxPositiveDataPoint = new DataPoint(dp.X, dp.Y);
                            maxPositiveRingValue = difference;
                        }
                    }
                    else
                    {
                        if (difference < maxNegativeRingValue)
                        {
                            maxNegativeDataPoint = new DataPoint(dp.X, dp.Y);
                            maxNegativeRingValue = difference;
                        }
                    }
                }

                if (isPositiveRing)
                {
                    return new DataPoint(maxPositiveDataPoint.X, maxPositiveDataPoint.Y - noiseAmount);
                }
                else
                {
                    return new DataPoint(maxNegativeDataPoint.X, maxNegativeDataPoint.Y - noiseAmount);
                }
            }
            else
            {
                return default(DataPoint);
            }
        }

        /// <summary>
        /// Finds the threshold crossing indexes of the first data point(s) with a Y-value that crosses the threshold.
        /// If a data point's Y-value is exactly the threshold value, only its index is returned.  If the threshold is
        /// crossed between two data points, both adjacent data point's indexes are returned.  If no data points cross
        /// the threshold, an empty collection is returned.
        /// </summary>
        /// <param name="waveform">The waveform to find the first data point(s) with a Y-value that crosses the threshold</param>
        /// <param name="yThreshold">The Y-value threshold to find the first data point crossing</param>
        /// <param name="findFirstThreshold">A value indicating whether the first threshold crossing should be returned (false will return the last)</param>
        /// <returns>
        /// The threshold crossing indexes of the first data point(s) with a Y-value that crosses the threshold.
        /// If a data point's Y-value is exactly the threshold value, only it's index is returned.  If the threshold is
        /// crossed between two data points, both adjacent data point's indexes are returned.  If no data points cross
        /// the threshold, an empty collection is returned.
        /// </returns>
        private static IEnumerable<int> ThresholdCrossingIndexes(this Waveform waveform, double yThreshold, bool findFirstThreshold)
        {
            if (waveform == null || !waveform.DataPoints.Any())
            {
                throw new ArgumentNullException("Waveform cannot be null, and must have at least 1 data point");
            }

            if (double.IsInfinity(yThreshold) || double.IsNaN(yThreshold))
            {
                throw new ArgumentOutOfRangeException("Threshold cannot be infinity or NaN");
            }

            List<int> indexes = new List<int>();
            if (findFirstThreshold)
            {
                for (int i = 0; i < waveform.DataPoints.Count(); i++)
                {
                    indexes.Add(i);
                }
            }
            else
            {
                for (int i = waveform.DataPoints.Count() - 1; i >= 0; i--)
                {
                    indexes.Add(i);
                }
            }

            bool? isPrevDataPointAboveThreshold = null;
            for (int i = 0; i < indexes.Count; i++)
            {
                double currDataPointY = waveform.DataPoints.ElementAt(indexes[i]).Y;
                if (currDataPointY == yThreshold)
                {
                    // The current data point Y-value matches the threshold exactly, so no interpolation is required
                    return new List<int>() { indexes[i] };
                }
                else
                {
                    bool isCurrDataPointAboveThreshold = currDataPointY > yThreshold;
                    if (isPrevDataPointAboveThreshold.HasValue)
                    {
                        if (isPrevDataPointAboveThreshold.Value != isCurrDataPointAboveThreshold)
                        {
                            // The threshold was crossed from the previous data point
                            return new List<int>() { indexes[i - 1], indexes[i] };
                        }
                    }

                    isPrevDataPointAboveThreshold = isCurrDataPointAboveThreshold;
                }
            }

            return new List<int>();
        }
    }
}
