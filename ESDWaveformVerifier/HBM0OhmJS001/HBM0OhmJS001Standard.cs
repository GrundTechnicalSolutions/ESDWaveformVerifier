// ---------------------------------------------------------------------------------------------------
//  <copyright file="HBM0OhmJS001Standard.cs" company="Grund Technical Solutions, Inc">
//      Copyright (c) Grund Technical Solutions, Inc. All rights reserved.
//  </copyright>
// ---------------------------------------------------------------------------------------------------
namespace ESDWaveformVerifier.HBM0OhmJS001
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ESDWaveformVerifier.DataTypes;

    /// <summary>
    /// Class that provides calculations for the HBM 0-Ohm JS-001 Standard
    /// </summary>
    public class HBM0OhmJS001Standard : Standard, IHBM0OhmStandard
    {
        /// <summary>
        /// Private backing store
        /// </summary>
        private Waveform absoluteIpsPlusDerivationOffsetWaveform = null;

        /// <summary>
        /// Private backing store
        /// </summary>
        private FifthDegreePolynomial absoluteIpsPolynomial = null;

        /// <summary>
        /// Private backing store
        /// </summary>
        private bool compensateForNoise;

        /// <summary>
        /// Private backing store
        /// </summary>
        private double compensateForNoiseCutoffTime;

        /// <summary>
        /// Private backing store
        /// </summary>
        private double peakCurrentDerivationOffsetTime;

        /// <summary>
        /// Private backing store
        /// </summary>
        private bool findDoublePeak;

        /// <summary>
        /// Private backing store
        /// </summary>
        private double doublePeakPercentIncrease;

        /// <summary>
        /// Private backing store
        /// </summary>
        private double doublePeakLowerPercentCutoff;

        /// <summary>
        /// Initializes a new instance of the <see cref="HBM0OhmJS001Standard"/> class
        /// </summary>
        /// <param name="waveform">The HBM 0-Ohm waveform to provide calculations on</param>
        /// <param name="signedVoltage">The signed voltage</param>
        public HBM0OhmJS001Standard(Waveform waveform, double signedVoltage)
            : this(true, 0, 0.1, 0.9, 0.000000055, false, 0.04, 0.85, waveform, signedVoltage)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="HBM0OhmJS001Standard"/> class
        /// </summary>
        /// <param name="compensateForNoise">A value indicating whether to compensate for noise or not</param>
        /// <param name="compensateForNoiseCutoffTime">The time to stop measuring for noise from the beginning of the waveform</param>
        /// <param name="riseTimeStartPercent">Rise Time starting percentage (Default: 90%)</param>
        /// <param name="riseTimeEndPercent">Rise Time ending percentage (Default: 10%)</param>
        /// <param name="peakCurrentDerivationOffsetTime">The amount of time after Tmax to interpolate Ips (see 5.2.3.3.1) in seconds.  Suggested is 40ns</param>
        /// <param name="findDoublePeak">A value indicating whether to look for a second peak that is slightly lower than the max peak, because some testers generate this type of peak</param>
        /// <param name="doublePeakPercentIncrease">The percent increase to look for when looking for a second peak.</param>
        /// <param name="doublePeakLowerPercentCutoff">The lower percent cutoff below which double-peaks will not be searched for</param>
        /// <param name="waveform">The HBM 0-Ohm waveform to provide calculations on</param>
        /// <param name="signedVoltage">The signed voltage</param>
        public HBM0OhmJS001Standard(
            bool compensateForNoise,
            double compensateForNoiseCutoffTime,
            double riseTimeStartPercent,
            double riseTimeEndPercent,
            double peakCurrentDerivationOffsetTime,
            bool findDoublePeak,
            double doublePeakPercentIncrease,
            double doublePeakLowerPercentCutoff,
            Waveform waveform,
            double signedVoltage)
            : base(waveform, signedVoltage)
        {
            this.compensateForNoise = compensateForNoise;
            this.compensateForNoiseCutoffTime = compensateForNoiseCutoffTime;
            this.peakCurrentDerivationOffsetTime = peakCurrentDerivationOffsetTime;
            this.findDoublePeak = findDoublePeak;
            this.doublePeakPercentIncrease = doublePeakPercentIncrease;
            this.doublePeakLowerPercentCutoff = doublePeakLowerPercentCutoff;
            this.CalculateIpsMaxDataPoint();
            this.CalculatePeakCurrent();
            this.CalculateRiseTime(riseTimeStartPercent, riseTimeEndPercent);
            this.CalculateDecayTime();
            this.CalculateRing();
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
        /// Gets the Ips Max DataPoint
        /// </summary>
        public DataPoint IpsMaxDataPoint
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
            get { return 0.000000002; }
        }

        /// <summary>
        /// Gets the Rise Time allowed maximum
        /// </summary>
        public double RiseTimeAllowedMaximum
        {
            get { return 0.000000010; }
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
        /// Gets the calculated Decay Time Value
        /// </summary>
        public double DecayTimeValue
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the calculated Decay Time starting DataPoint (interpolated)
        /// </summary>
        public DataPoint DecayTimeStartDataPoint
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the calculated Decay Time ending DataPoint (interpolated)
        /// </summary>
        public DataPoint DecayTimeEndDataPoint
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the calculated Decay Time allowed minimum
        /// </summary>
        public double DecayTimeAllowedMinimum
        {
            get { return 0.000000130; }
        }

        /// <summary>
        /// Gets the calculated Decay Time allowed maximum
        /// </summary>
        public double DecayTimeAllowedMaximum
        {
            get { return 0.000000170; }
        }

        /// <summary>
        /// Gets a value indicating whether the Decay Time Value is passing or not
        /// </summary>
        public bool DecayTimeIsPassing
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the calculated Total Ring Percent Value
        /// </summary>
        public double TotalRingPercentValue
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the Total Ring allowed maximum as a %
        /// </summary>
        public double TotalRingAllowedMaximum
        {
            get { return 0.15; }
        }

        /// <summary>
        /// Gets a value indicating whether the Total Ring Value is passing or not
        /// </summary>
        public bool TotalRingIsPassing
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the Ring1 peak DataPoint (maximum positive ring)
        /// </summary>
        public DataPoint Ring1PeakDataPoint
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the Ring2 peak DataPoint (maximum negative ring)
        /// </summary>
        public DataPoint Ring2PeakDataPoint
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the 5th Degree Polynomial that represents the Least Squares Fit line derived from Ips until Ips + peak current derivation offset time of the absolute Waveform
        /// </summary>
        public FifthDegreePolynomial AbsoluteIpsPolynomial
        {
            get
            {
                if (this.absoluteIpsPolynomial == null)
                {
                    this.absoluteIpsPolynomial = this.AbsoluteIpsPlusPeakCurrentDerivationOffsetWaveform.LeastSquaresFit();
                }

                return this.absoluteIpsPolynomial;
            }
        }

        /// <summary>
        /// Gets an absolute Waveform gated to only Ips time until after the peak current derivation offset time
        /// </summary>
        private Waveform AbsoluteIpsPlusPeakCurrentDerivationOffsetWaveform
        {
            get
            {
                if (this.absoluteIpsPlusDerivationOffsetWaveform == null)
                {
                    // Calculate the (absolute) Max Current DataPoint
                    DataPoint ipsMaxAbsoluteDataPoint = this.IpsMaxDataPoint.InvertYValueIfNegativePolarity(this.WaveformIsPositivePolarity);

                    // Find the time that is after the peak current derivation offset time
                    double afterDerivationOffsetTime = ipsMaxAbsoluteDataPoint.X + this.peakCurrentDerivationOffsetTime;

                    // Gate the waveform between IpsMax and after the peak current derivation offset time, to be used for least-squares-fit formula
                    this.absoluteIpsPlusDerivationOffsetWaveform = this.AbsoluteWaveform.Gates(ipsMaxAbsoluteDataPoint.X, afterDerivationOffsetTime);
                }

                return this.absoluteIpsPlusDerivationOffsetWaveform;
            }
        }

        /// <summary>
        /// Calculates Ips Max Data Point.  This may be different depending on what the double-peak option is set to.
        /// </summary>
        private void CalculateIpsMaxDataPoint()
        {
            DataPoint absIpsDataPoint = this.AbsoluteWaveform.Maximum();

            if (this.findDoublePeak)
            {
                double mostRecentTroughPercent = 1.0;
                DataPoint? secondIpsDataPoint = null;

                foreach (DataPoint dp in this.AbsoluteWaveform.DataPoints)
                {
                    if (dp.X > absIpsDataPoint.X && absIpsDataPoint.Y > 0)
                    {
                        double dpPercentOfAbsMax = dp.Y / absIpsDataPoint.Y;
                        if (dpPercentOfAbsMax < mostRecentTroughPercent)
                        {
                            mostRecentTroughPercent = System.Math.Max(dpPercentOfAbsMax, this.doublePeakLowerPercentCutoff);
                        }
                        else
                        {
                            if (dpPercentOfAbsMax - mostRecentTroughPercent > this.doublePeakPercentIncrease)
                            {
                                if (secondIpsDataPoint == null || secondIpsDataPoint.Value.Y < dp.Y)
                                {
                                    secondIpsDataPoint = dp;
                                }
                            }
                        }
                    }
                }

                absIpsDataPoint = secondIpsDataPoint ?? absIpsDataPoint;
            }

            this.IpsMaxDataPoint = absIpsDataPoint.InvertYValueIfNegativePolarity(this.WaveformIsPositivePolarity);
        }

        /// <summary>
        /// Calculates the Peak Current (Ips) related values
        /// </summary>
        private void CalculatePeakCurrent()
        {
            // Calculate the (absolute) Max Current DataPoint
            DataPoint ipsMaxAbsoluteDataPoint = this.IpsMaxDataPoint.InvertYValueIfNegativePolarity(this.WaveformIsPositivePolarity);

            // Determine Ips by evaluating the ips Polynomial at the IpsMax time
            double ipsAbsoluteValue = this.AbsoluteIpsPolynomial.Evaluate(ipsMaxAbsoluteDataPoint.X);

            // Determine the Peak Current (Ips) value
            this.PeakCurrentValue = ipsAbsoluteValue.InvertValueIfNegativePolarity(this.WaveformIsPositivePolarity);

            // Create a DataPoint at the time of Ips with the Ips value
            this.PeakCurrentDataPoint = new DataPoint(ipsMaxAbsoluteDataPoint.X, this.PeakCurrentValue);

            // Determine the min and max allowed values for the Peak Current to be passing
            Tuple<double, double, double> nomMinMaxPeakCurrent = HBM0OhmJS001WaveformCharacteristics.HBMPeakCurrentNominalMinMax(this.SignedVoltage);
            this.PeakCurrentAllowedMinimum = nomMinMaxPeakCurrent.Item2.InvertValueIfNegativePolarity(this.WaveformIsPositivePolarity);
            this.PeakCurrentAllowedMaximum = nomMinMaxPeakCurrent.Item3.InvertValueIfNegativePolarity(this.WaveformIsPositivePolarity);

            // Determine if the Peak Current is passing
            this.PeakCurrentIsPassing = DoubleRangeExtensions.BetweenInclusive(this.PeakCurrentAllowedMinimum, this.PeakCurrentAllowedMaximum, this.PeakCurrentValue);
        }

        /// <summary>
        /// Calculates the Rise Time related values
        /// </summary>
        /// <param name="riseTimeStartPercent">Rise Time starting percentage (Default: 90%)</param>
        /// <param name="riseTimeEndPercent">Rise Time ending percentage (Default: 10%)</param>
        private void CalculateRiseTime(double riseTimeStartPercent, double riseTimeEndPercent)
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
            DataPoint? riseTimeEndAbsoluteDataPoint = this.AbsoluteWaveform.DataPointAtYThreshold(riseTimeEndThreshold, true);

            if (riseTimeEndAbsoluteDataPoint.HasValue)
            {
                // Convert the first Rise Time Data Point to the signed value
                this.RiseTimeEndDataPoint = riseTimeEndAbsoluteDataPoint.Value.InvertYValueIfNegativePolarity(this.WaveformIsPositivePolarity);

                // Trim the waveform, removing everything after the Rise Time End Time
                Waveform absoluteWaveformUntilRiseTimeEnd = this.AbsoluteWaveform.TrimEnd(this.RiseTimeEndDataPoint.X);

                // Calculate what the Rise Time Start Threshold is (a percentage of peak current)
                double riseTimeStartThreshold = peakCurrentAbsoluteValue * riseTimeStartPercent;

                // Find the last Data Point (interpolated) Data Point that is below the Rise Time Start Threshold (of the absolute trimmed waveform)
                DataPoint? riseTimeStartAbsoluteDataPoint = absoluteWaveformUntilRiseTimeEnd.DataPointAtYThreshold(riseTimeStartThreshold, false);

                if (riseTimeStartAbsoluteDataPoint.HasValue)
                {
                    // Convert the last Rise Time Data Point to the signed value
                    this.RiseTimeStartDataPoint = riseTimeStartAbsoluteDataPoint.Value.InvertYValueIfNegativePolarity(this.WaveformIsPositivePolarity);

                    // Find the Rise Time
                    this.RiseTimeValue = this.RiseTimeEndDataPoint.X - this.RiseTimeStartDataPoint.X;

                    // Determine if the Rise Time is passing
                    this.RiseTimeIsPassing = DoubleRangeExtensions.BetweenInclusive(this.RiseTimeAllowedMinimum, this.RiseTimeAllowedMaximum, this.RiseTimeValue);
                }
                else
                {
                    // Risetime start data point could not be found, no calculations could be made.
                }
            }
            else
            {
                // Risetime end data point could not be found, no calculations could be made.
            }
        }

        /// <summary>
        /// Calculates the Decay Time related values
        /// </summary>
        private void CalculateDecayTime()
        {
            // The percentage of the Peak Current to be the end of the decay time (1/e)
            double decayTimeEndPercentOfIps = 1.0 / System.Math.E;

            // Trim the waveform, removing everything before the max peak
            Waveform absoluteWaveformAfterPeakCurrentTime = this.AbsoluteWaveform.TrimStart(this.PeakCurrentDataPoint.X);

            // Find the start/stop times for the noise-reducing exponential fit waveform
            DataPoint? expFitStartDataPoint = absoluteWaveformAfterPeakCurrentTime.DataPointAtYThreshold(0.5 * System.Math.Abs(this.PeakCurrentValue), true);
            if (expFitStartDataPoint.HasValue)
            {
                double expFitStartTimeThreshold = expFitStartDataPoint.Value.X;
                DataPoint? expFitEndDataPoint = absoluteWaveformAfterPeakCurrentTime.DataPointAtYThreshold(0.3 * System.Math.Abs(this.PeakCurrentValue), true);

                // The waveform was truncated before the lower end of the decaying region,
                // however there might be enough data to calculate by using the last data point instead.
                if (!expFitEndDataPoint.HasValue)
                {
                    expFitEndDataPoint = absoluteWaveformAfterPeakCurrentTime.DataPoints.Last();
                }

                double expFitEndTimeThreshold = expFitEndDataPoint.Value.X;

                // Gate the waveform to just the area for the noise-reducing exponential fit waveform
                Waveform absoluteWaveformDecayingRegion = this.AbsoluteWaveform.Gates(expFitStartTimeThreshold, expFitEndTimeThreshold);

                if (absoluteWaveformDecayingRegion.DataPoints.Any())
                {
                    // Find the exponential fit constants of the waveform from 0.5 * Ips to 0.3 * Ips to eliminate noise
                    Tuple<double, double> expFitAbsoluteWaveformDecayingRegionConstants = absoluteWaveformDecayingRegion.ExponentialFit();

                    // Create time values that go out far enough to ensure the decay time end threshold can be found
                    List<double> timeValues = (from dp in absoluteWaveformDecayingRegion.DataPoints select dp.X).ToList();
                    double samplingTime = absoluteWaveformDecayingRegion.SamplingTime();
                    while (timeValues.Last() < 0.000000800 && samplingTime > 0)
                    {
                        timeValues.Add(timeValues.Last() + samplingTime);
                    }

                    // Create the exponential fit equivalent waveform which has noise eliminated
                    Waveform expFitAbsoluteWaveformDecayingRegion = WaveformExtensions.CreateExponentialFitWaveform(
                        expFitAbsoluteWaveformDecayingRegionConstants.Item1,
                        expFitAbsoluteWaveformDecayingRegionConstants.Item2,
                        timeValues);

                    // Calculate what the decay time end threshold is (1/e % of Ips)
                    double decayTimeEndThreshold = System.Math.Abs(this.PeakCurrentValue) * decayTimeEndPercentOfIps;

                    // Find the first (interpolated) data point that is at 1/e % of Ips value (of the absolute value waveform)
                    DataPoint? decayTimeEndDataPointAbsolute = expFitAbsoluteWaveformDecayingRegion.DataPointAtYThreshold(decayTimeEndThreshold, true);

                    if (decayTimeEndDataPointAbsolute.HasValue)
                    {
                        // Convert the Decay Time end DataPoint to the signed value
                        this.DecayTimeEndDataPoint = decayTimeEndDataPointAbsolute.Value.InvertYValueIfNegativePolarity(this.WaveformIsPositivePolarity);

                        // Calculate the Decay Time
                        this.DecayTimeValue = this.DecayTimeEndDataPoint.X - this.PeakCurrentDataPoint.X;

                        // Determine if the Decay Time is passing
                        this.DecayTimeIsPassing = DoubleRangeExtensions.BetweenInclusive(this.DecayTimeAllowedMinimum, this.DecayTimeAllowedMaximum, this.DecayTimeValue);
                    }
                    else
                    {
                        // The derived exponential fit waveform didn't go out to a far enough time to capture the 1/e time.
                    }
                }
                else
                {
                    // Could not find any data points in the decay time window.  Likely due to a malformed waveform.
                }
            }
            else
            {
                // Since the waveform was truncated before even the start of the window to calculate the decaying region, the decay time cannot be calculated.
            }
        }

        /// <summary>
        /// Calculates the Ring as a % of Ips
        /// </summary>
        private void CalculateRing()
        {
            double positiveLeadingEdgeNoiseValue = 0;
            double negativeLeadingEdgeNoiseValue = 0;
            if (this.compensateForNoise)
            {
                // (R&D experiment) A flag indicating that the noise reduction should use the peak-to-peak found in the zero line
                bool useZeroLineStrategy = false;

                // (R&D experiment) A flag indicating that the noise reduction should use a bessel-filtered waveform to remove the noise
                bool useBesselFilterStrategy = true;

                if (useZeroLineStrategy)
                {
                    Waveform zeroLineBeforeTriggerWaveform = this.AbsoluteWaveform.TrimEnd(this.compensateForNoiseCutoffTime);
                    positiveLeadingEdgeNoiseValue = zeroLineBeforeTriggerWaveform.Maximum().Y;
                    negativeLeadingEdgeNoiseValue = zeroLineBeforeTriggerWaveform.Minimum().Y;
                }
                else if (useBesselFilterStrategy)
                {
                    Waveform rawWaveform = this.AbsoluteIpsPlusPeakCurrentDerivationOffsetWaveform;
                    Waveform besselWaveform = BesselDigitalFilter.FilterWaveform(rawWaveform);
                    for (int i = 0; i < rawWaveform.DataPoints.Count(); i++)
                    {
                        positiveLeadingEdgeNoiseValue = System.Math.Max(positiveLeadingEdgeNoiseValue, rawWaveform.DataPoints.ElementAt(i).Y - besselWaveform.DataPoints.ElementAt(i).Y);
                        negativeLeadingEdgeNoiseValue = System.Math.Min(negativeLeadingEdgeNoiseValue, rawWaveform.DataPoints.ElementAt(i).Y - besselWaveform.DataPoints.ElementAt(i).Y);
                    }
                }
            }

            // Find Ring1 Data point (max positive ring)
            this.Ring1PeakDataPoint = this.AbsoluteIpsPlusPeakCurrentDerivationOffsetWaveform.DataPointAtHBM0OhmJS001MaximumRing(this.AbsoluteIpsPolynomial, positiveLeadingEdgeNoiseValue, true);

            // Find Y-value of FifthDegreePolynomial at the time of Ring1
            double positiveRingLeastSquaresFitValue = this.AbsoluteIpsPolynomial.Evaluate(this.Ring1PeakDataPoint.X);

            // Find the difference between Ring1 and Ring1FDP
            double positiveRingCurrentDifference = this.Ring1PeakDataPoint.Y - positiveRingLeastSquaresFitValue;

            // Find the absolute value of the difference between Ring1 and Ring1FDP
            double positiveRingCurrentDifferenceAbs = System.Math.Abs(positiveRingCurrentDifference);

            // Find Ring2 Data point (max negative ring)
            this.Ring2PeakDataPoint = this.AbsoluteIpsPlusPeakCurrentDerivationOffsetWaveform.DataPointAtHBM0OhmJS001MaximumRing(this.AbsoluteIpsPolynomial, negativeLeadingEdgeNoiseValue, false);

            // Output the Ring2 DataPoint (max negative ring)

            // Find Y-value of FifthDegreePolynomial at the time of Ring2
            double negativeRingLeastSquaresFitValue = this.AbsoluteIpsPolynomial.Evaluate(this.Ring2PeakDataPoint.X);

            // Find the difference between Ring2 and Ring2FDP
            double negativeRingCurrentDifference = this.Ring2PeakDataPoint.Y - negativeRingLeastSquaresFitValue;

            // Find the absolute value of the difference between Ring2 and Ring2FDP
            double negativeRingCurrentDifferenceAbs = System.Math.Abs(negativeRingCurrentDifference);

            // Add the two differences to get Ir
            double totalRingCurrent = positiveRingCurrentDifferenceAbs + negativeRingCurrentDifferenceAbs;

            // Determine the ringing % compared to Ips
            this.TotalRingPercentValue = totalRingCurrent / System.Math.Abs(this.PeakCurrentValue);

            // Determine whether the ringing % is passing
            this.TotalRingIsPassing = this.TotalRingPercentValue <= this.TotalRingAllowedMaximum;
        }
    }
}
