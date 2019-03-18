// ---------------------------------------------------------------------------------------------------
//  <copyright file="CDMJS002Standard.cs" company="Grund Technical Solutions, Inc">
//      Copyright (c) Grund Technical Solutions, Inc. All rights reserved.
//  </copyright>
// ---------------------------------------------------------------------------------------------------
namespace ESDWaveformVerifier.CDMJS002
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ESDWaveformVerifier.DataTypes;

    /// <summary>
    /// Class that provides calculations for the CDM JS-002 Standard
    /// </summary>
    public class CDMJS002Standard : Standard, ICDMStandard
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CDMJS002Standard"/> class
        /// </summary>
        /// <param name="waveform">The CDM waveform to provide calculations on</param>
        /// <param name="signedVoltage">The signed voltage</param>
        /// <param name="isLargeTarget">A value indicating whether the CDM target is the Large target or not (small if not)</param>
        /// <param name="oscilloscopeIsHighBandwidth">A value indicating whether the oscilloscope used is high bandwidth or not</param>
        public CDMJS002Standard(Waveform waveform, double signedVoltage, bool isLargeTarget, bool oscilloscopeIsHighBandwidth)
            : base(waveform, signedVoltage)
        {
            this.IsLargeTarget = isLargeTarget;
            this.OscilloscopeIsHighBandwidth = oscilloscopeIsHighBandwidth;

            this.CalculatePeakCurrent();
            this.CalculateRiseTime();
            this.CalculateFullWidthAtHalfMax();
            this.CalculateUndershoot();
        }

        /// <summary>
        /// Gets a value indicating whether the CDM target is the Large target or not (small if not)
        /// </summary>
        public bool IsLargeTarget
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a value indicating whether the oscilloscope used is high bandwidth or not
        /// </summary>
        public bool OscilloscopeIsHighBandwidth
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the calculated Peak Current Value
        /// </summary>
        public double PeakCurrentValue
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the calculated Peak Current DataPoint
        /// </summary>
        public DataPoint PeakCurrentDataPoint
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the calculated Peak Current allowed minimum
        /// </summary>
        public double PeakCurrentAllowedMinimum
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the calculated Peak Current allowed maximum
        /// </summary>
        public double PeakCurrentAllowedMaximum
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a value indicating whether the Peak Current Value is passing or not
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
        /// Gets the calculated Rise Time allowed minimum
        /// </summary>
        public double RiseTimeAllowedMinimum
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the calculated Rise Time allowed maximum
        /// </summary>
        public double RiseTimeAllowedMaximum
        {
            get;
            private set;
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
        /// Gets the calculated Full Width at Half Maximum Value
        /// </summary>
        public double FullWidthHalfMaxValue
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the calculated Full Width at Half Maximum starting DataPoint
        /// </summary>
        public DataPoint FullWidthHalfMaxStartDataPoint
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the calculated Full Width at Half Maximum ending DataPoint
        /// </summary>
        public DataPoint FullWidthHalfMaxEndDataPoint
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the calculated Full Width at Half Maximum allowed minimum
        /// </summary>
        public double FullWidthHalfMaxAllowedMinimum
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the calculated Full Width at Half Maximum allowed maximum
        /// </summary>
        public double FullWidthHalfMaxAllowedMaximum
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a value indicating whether the Full Width at Half Maximum Value is passing or not
        /// </summary>
        public bool FullWidthHalfMaxIsPassing
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the signed Undershoot Value
        /// </summary>
        public double UndershootValue
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the signed Undershoot DataPoint
        /// </summary>
        public DataPoint UndershootDataPoint
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the Undershoot allowed maximum as a percentage
        /// </summary>
        public double UndershootAllowedMaximumPercent
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the Undershoot allowed maximum value
        /// </summary>
        public double UndershootAllowedMaximumValue
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a value indicating whether the Undershoot Value is passing or not
        /// </summary>
        public bool UndershootIsPassing
        {
            get;
            private set;
        }

        /// <summary>
        /// Calculates the Peak Current related values
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
            Tuple<double, double, double> nomMinMaxPeakCurrent = CDMJS002WaveformCharacteristics.PeakCurrentNominalMinMax(this.SignedVoltage, this.IsLargeTarget, this.OscilloscopeIsHighBandwidth);
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

            // Determine the max allowed Rise Time to be passing
            this.RiseTimeAllowedMaximum = CDMJS002WaveformCharacteristics.RiseTimeMax(this.SignedVoltage, this.IsLargeTarget, this.OscilloscopeIsHighBandwidth);

            // Determine if the Rise Time is passing
            this.RiseTimeIsPassing = this.RiseTimeValue <= this.RiseTimeAllowedMaximum;
        }

        /// <summary>
        /// Calculates the Full Width at Half Max related values
        /// </summary>
        /// <param name="fullWidthHalfMaxPercent">(Optional) The threshold of where to measure the FWHM as a percent of the peak current (Default: 50%)</param>
        private void CalculateFullWidthAtHalfMax(double fullWidthHalfMaxPercent = 0.5)
        {
            if (fullWidthHalfMaxPercent <= 0.0 || fullWidthHalfMaxPercent >= 1.0)
            {
                throw new ArgumentOutOfRangeException("The Full Width Half Max Percentage cannot be less than or equal to 0% or greater than or equal to 100%");
            }

            // Calculate what the half-max current value is (default is 50% of max amplitude)
            double fullWidthHalfMaxCurrentSigned = this.PeakCurrentValue * fullWidthHalfMaxPercent;
            double fullWidthHalfMaxCurrentAbsolute = fullWidthHalfMaxCurrentSigned.InvertValueIfNegativePolarity(this.WaveformIsPositivePolarity);

            // Find the first (interpolated) data point that is on the rising edge of the initial spike (of the absolute value waveform)
            DataPoint fullWidthHalfMaxRisingAbsoluteDataPoint = this.AbsoluteWaveform.DataPointAtYThreshold(fullWidthHalfMaxCurrentAbsolute, true);

            // Convert the rising data point to the signed value
            this.FullWidthHalfMaxStartDataPoint = fullWidthHalfMaxRisingAbsoluteDataPoint.InvertYValueIfNegativePolarity(this.WaveformIsPositivePolarity);

            // Trim the waveform, removing everything before the max peak
            Waveform absoluteWaveformAfterPeakCurrentTime = this.AbsoluteWaveform.TrimStart(this.PeakCurrentDataPoint.X);

            // Find the first (interpolated) data point that is on the falling edge of the initial spike (of the absolute value waveform)
            DataPoint fullWidthHalfMaxFallingDataPointAbsolute = absoluteWaveformAfterPeakCurrentTime.DataPointAtYThreshold(fullWidthHalfMaxCurrentAbsolute, true);

            // Convert the falling data point to the signed value
            this.FullWidthHalfMaxEndDataPoint = fullWidthHalfMaxFallingDataPointAbsolute.InvertYValueIfNegativePolarity(this.WaveformIsPositivePolarity);

            // Find the full width half max time
            this.FullWidthHalfMaxValue = this.FullWidthHalfMaxEndDataPoint.X - this.FullWidthHalfMaxStartDataPoint.X;

            // Determine the min and max values for the Full Width Half Max to be passing
            Tuple<double, double, double> nomMinMaxFullWidthHalfMax = CDMJS002WaveformCharacteristics.FullWidthHalfMaxNominalMinMax(this.SignedVoltage, this.IsLargeTarget, this.OscilloscopeIsHighBandwidth);
            this.FullWidthHalfMaxAllowedMinimum = nomMinMaxFullWidthHalfMax.Item2;
            this.FullWidthHalfMaxAllowedMaximum = nomMinMaxFullWidthHalfMax.Item3;

            // Determine if the Full Width Half Max is passing
            this.FullWidthHalfMaxIsPassing = DoubleRangeExtensions.BetweenInclusive(this.FullWidthHalfMaxAllowedMinimum, this.FullWidthHalfMaxAllowedMaximum, this.FullWidthHalfMaxValue);
        }

        /// <summary>
        /// Calculates the Undershoot related values
        /// </summary>
        /// <param name="undershootMaxTimeFWHMMultiplier">(Optional) The multiplier of the FWHM time to determine how long after the peak current time to search for undershoot (Default: 2.5x)</param>
        private void CalculateUndershoot(double undershootMaxTimeFWHMMultiplier = 2.5)
        {
            if (undershootMaxTimeFWHMMultiplier <= 0)
            {
                throw new ArgumentOutOfRangeException("The Undershoot Max Time multiplier cannot be less than or equal to 0");
            }

            // Determine how much time after the max data point of the waveform to keep for Undershoot
            double timeForUndershootDetectionAfterMaxTime = this.FullWidthHalfMaxValue * undershootMaxTimeFWHMMultiplier;

            // Add the undershoot allowed time to the max data point time
            double endTimeForUndershootDetection = this.PeakCurrentDataPoint.X + timeForUndershootDetectionAfterMaxTime;

            // Trim the waveform, removing everything before the max peak
            Waveform absoluteWaveformAfterPeakCurrentTime = this.AbsoluteWaveform.TrimStart(this.PeakCurrentDataPoint.X);

            // Trim the end of the waveform from the max time until 2.5x the FWHM time
            Waveform absoluteUndershootApplicableWaveform = absoluteWaveformAfterPeakCurrentTime.TrimEnd(endTimeForUndershootDetection);

            // Find the (absolute) undershoot data point of the absolute Current waveform after the max time
            DataPoint absoluteUndershootDataPoint = absoluteUndershootApplicableWaveform.Minimum();

            // Look up what the undershoot max percent can be (it is a negative value)
            this.UndershootAllowedMaximumPercent = CDMJS002WaveformCharacteristics.UndershootMaxPercent(this.SignedVoltage, this.IsLargeTarget, this.OscilloscopeIsHighBandwidth);

            // Calculate what the maximum undershoot value can be (it is a negative number since we are referring to undershoot)
            this.UndershootAllowedMaximumValue = this.PeakCurrentValue * this.UndershootAllowedMaximumPercent;

            // Convert the absolute undershoot value to the signed value
            double absoluteUndershootValue = absoluteUndershootDataPoint.Y;

            // If the absolute undershoot is greater than zero, set it to zero.  Otherwise set it to the signed value
            if (absoluteUndershootValue > 0)
            {
                this.UndershootValue = 0;
            }
            else
            {
                this.UndershootValue = absoluteUndershootValue.InvertValueIfNegativePolarity(this.WaveformIsPositivePolarity);
            }

            // Determine if the Undershoot is passing
            if (this.WaveformIsPositivePolarity)
            {
                this.UndershootIsPassing = this.UndershootValue >= this.UndershootAllowedMaximumValue;
            }
            else
            {
                this.UndershootIsPassing = this.UndershootValue <= this.UndershootAllowedMaximumValue;
            }

            // Create the undershoot signed Data Point
            this.UndershootDataPoint = absoluteUndershootDataPoint.InvertYValueIfNegativePolarity(this.WaveformIsPositivePolarity);
        }
    }
}
