// ---------------------------------------------------------------------------------------------------
//  <copyright file="HBM500OhmJS001Standard.cs" company="Grund Technical Solutions, Inc">
//      Copyright (c) Grund Technical Solutions, Inc. All rights reserved.
//  </copyright>
// ---------------------------------------------------------------------------------------------------
namespace ESDWaveformVerifier.HBM500OhmJS001
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ESDWaveformVerifier.DataTypes;

    /// <summary>
    /// Class that provides calculations for the HBM 500-Ohm JS-001 Standard
    /// </summary>
    public class HBM500OhmJS001Standard : Standard, IHBM500OhmStandard
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HBM500OhmJS001Standard"/> class
        /// </summary>
        /// <param name="waveform">The HBM 500-Ohm waveform to provide calculations on</param>
        /// <param name="signedVoltage">The signed voltage</param>
        public HBM500OhmJS001Standard(Waveform waveform, double signedVoltage)
            : base(waveform, signedVoltage)
        {
            this.CalculatePeakCurrent();
            this.CalculateRiseTime();
        }

        /// <summary>
        /// Gets the calculated Peak Current (Ips) Value
        /// </summary>
        public double PeakCurrentValue
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the calculated Peak Current (Ips) DataPoint
        /// </summary>
        public DataPoint PeakCurrentDataPoint
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the calculated Peak Current (Ips) allowed minimum
        /// </summary>
        public double PeakCurrentAllowedMinimum
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the calculated Peak Current (Ips) allowed maximum
        /// </summary>
        public double PeakCurrentAllowedMaximum
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a value indicating whether the Peak Current (Ips) Value is passing or not
        /// </summary>
        public bool PeakCurrentIsPassing
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the calculated Rise Time Value
        /// </summary>
        public double RiseTimeValue
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the calculated Rise Time starting DataPoint (interpolated)
        /// </summary>
        public DataPoint RiseTimeStartDataPoint
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the calculated Rise Time ending DataPoint (interpolated)
        /// </summary>
        public DataPoint RiseTimeEndDataPoint
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the Rise Time allowed minimum
        /// </summary>
        public double RiseTimeAllowedMinimum
        {
            get { return 0.000000005; }
        }

        /// <summary>
        /// Gets the Rise Time allowed maximum
        /// </summary>
        public double RiseTimeAllowedMaximum
        {
            get { return 0.000000025; }
        }

        /// <summary>
        /// Gets a value indicating whether the Rise Time Value is passing or not
        /// </summary>
        public bool RiseTimeIsPassing
        {
            get;
            private set;
        }

        /// <summary>
        /// Calculates the Peak Current (Ips) related values
        /// </summary>
        private void CalculatePeakCurrent()
        {
            // Calculate the (absolute) Peak Current DataPoint
            DataPoint absolutePeakCurrentDataPoint = this.AbsoluteWaveform.Maximum();

            // Convert to the signed Peak Current DataPoint
            this.PeakCurrentDataPoint = absolutePeakCurrentDataPoint.InvertYValueIfNegativePolarity(this.WaveformIsPositivePolarity);

            // Extract the Peak Current value
            this.PeakCurrentValue = this.PeakCurrentDataPoint.Y;

            // Determine the min and max allowed values for the Peak Current to be passing
            Tuple<double, double, double> nomMinMaxPeakCurrent = HBM500OhmJS001WaveformCharacteristics.HBMPeakCurrentNominalMinMax(this.SignedVoltage);
            this.PeakCurrentAllowedMinimum = nomMinMaxPeakCurrent.Item2.InvertValueIfNegativePolarity(this.WaveformIsPositivePolarity);
            this.PeakCurrentAllowedMaximum = nomMinMaxPeakCurrent.Item3.InvertValueIfNegativePolarity(this.WaveformIsPositivePolarity);

            // Determine if the Peak Current is passing
            this.PeakCurrentIsPassing = DoubleRangeExtensions.BetweenInclusive(this.PeakCurrentAllowedMinimum, this.PeakCurrentAllowedMaximum, this.PeakCurrentValue);
        }

        /// <summary>
        /// Calculates the Rise Time related values
        /// </summary>
        /// <param name="riseTimeStartPercent">(Optional) The Rise Time starting percentage (Default: 90%)</param>
        /// <param name="riseTimeEndPercent">(Optional) The Rise Time ending percentage (Default: 10%)</param>
        private void CalculateRiseTime(double riseTimeStartPercent = 0.1, double riseTimeEndPercent = 0.9)
        {
            if (riseTimeStartPercent < 0.0 || riseTimeStartPercent >= 1.0)
            {
                throw new ArgumentOutOfRangeException("The Rise Time Start % cannot be less than 0% or greater than or equal to 100%");
            }

            if (riseTimeEndPercent <= 0.0 || riseTimeEndPercent > 1.0)
            {
                throw new ArgumentOutOfRangeException("The Rise Time End % cannot be less than or equal to 0% or greater than 100%");
            }

            if (riseTimeStartPercent >= riseTimeEndPercent)
            {
                throw new ArgumentOutOfRangeException("The Rise Time Start % cannot be equal to or greater than the Rise Time End %");
            }

            // Generate an absolute-value version of the peak current value
            double peakCurrentAbsoluteValue = this.PeakCurrentValue.InvertValueIfNegativePolarity(this.WaveformIsPositivePolarity);

            // Calculate what the rise time end threshold is (a percentage of peak current)
            double riseTimeEndThreshold = peakCurrentAbsoluteValue * riseTimeEndPercent;

            // Find the first (interpolated) data point that is at the rise time end threshold (of the absolute value waveform)
            DataPoint riseTimeEndAbsoluteDataPoint = this.AbsoluteWaveform.DataPointAtYThreshold(riseTimeEndThreshold, true);

            // Convert the first Rise Time Data Point to the signed value
            this.RiseTimeEndDataPoint = riseTimeEndAbsoluteDataPoint.InvertYValueIfNegativePolarity(this.WaveformIsPositivePolarity);

            // Trim the waveform, removing everything after the Rise Time End Time
            Waveform absoluteWaveformUntilRiseTimeEnd = this.AbsoluteWaveform.TrimEnd(this.RiseTimeEndDataPoint.X);

            // Calculate what the Rise Time Start Threshold is (a percentage of peak current)
            double riseTimeStartThreshold = peakCurrentAbsoluteValue * riseTimeStartPercent;

            // Find the last Data Point (interpolated) Data Point that is below the Rise Time Start Threshold (of the absolute trimmed waveform)
            DataPoint riseTimeStartAbsoluteDataPoint = absoluteWaveformUntilRiseTimeEnd.DataPointAtYThreshold(riseTimeStartThreshold, false);

            // Convert the last Rise Time Data Point to the signed value
            this.RiseTimeStartDataPoint = riseTimeStartAbsoluteDataPoint.InvertYValueIfNegativePolarity(this.WaveformIsPositivePolarity);

            // Find the Rise Time
            this.RiseTimeValue = this.RiseTimeEndDataPoint.X - this.RiseTimeStartDataPoint.X;

            // Determine if the Rise Time is passing
            this.RiseTimeIsPassing = DoubleRangeExtensions.BetweenInclusive(this.RiseTimeAllowedMinimum, this.RiseTimeAllowedMaximum, this.RiseTimeValue);
        }
    }
}
