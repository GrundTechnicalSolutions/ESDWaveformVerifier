// ---------------------------------------------------------------------------------------------------
//  <copyright file="CDMJS002WaveformCharacteristics.cs" company="Grund Technical Solutions, Inc">
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
    /// A class that represents the CDM JS-002 Waveform Characteristics as found in JS-002 - Section 6.7 - Tables 1 and 2
    /// </summary>
    internal static class CDMJS002WaveformCharacteristics
    {
        /// <summary>
        /// Private backing store for the sets of CDM JS-002 characteristics.
        /// These are derived from JS-002 - Section 6.7 - Tables 1 and 2.
        /// </summary>
        private static List<CDMJS002WaveformCharacteristicsSet> characteristicSets = new List<CDMJS002WaveformCharacteristicsSet>()
        {
            new CDMJS002WaveformCharacteristicsSet()
            {
                IsHighBandwidth = true,
                IsLargeTarget = true,
                TestCondition = 125,
                PeakCurrent = new Tuple<double, double>(2.3, 3.8),
                RiseTimeMax = 0.000000000350,
                FullWidthAtHalfMaximum = new Tuple<double, double>(0.000000000450, 0.000000000900),
                UndershootMaxPercent = -0.5,
            },
            new CDMJS002WaveformCharacteristicsSet()
            {
                IsHighBandwidth = true,
                IsLargeTarget = true,
                TestCondition = 250,
                PeakCurrent = new Tuple<double, double>(4.8, 7.3),
                RiseTimeMax = 0.000000000350,
                FullWidthAtHalfMaximum = new Tuple<double, double>(0.000000000450, 0.000000000900),
                UndershootMaxPercent = -0.5,
            },
            new CDMJS002WaveformCharacteristicsSet()
            {
                IsHighBandwidth = true,
                IsLargeTarget = true,
                TestCondition = 500,
                PeakCurrent = new Tuple<double, double>(10.3, 13.9),
                RiseTimeMax = 0.000000000350,
                FullWidthAtHalfMaximum = new Tuple<double, double>(0.000000000450, 0.000000000900),
                UndershootMaxPercent = -0.5,
            },
            new CDMJS002WaveformCharacteristicsSet()
            {
                IsHighBandwidth = true,
                IsLargeTarget = true,
                TestCondition = 750,
                PeakCurrent = new Tuple<double, double>(15.5, 20.9),
                RiseTimeMax = 0.000000000350,
                FullWidthAtHalfMaximum = new Tuple<double, double>(0.000000000450, 0.000000000900),
                UndershootMaxPercent = -0.5,
            },
            new CDMJS002WaveformCharacteristicsSet()
            {
                IsHighBandwidth = true,
                IsLargeTarget = true,
                TestCondition = 1000,
                PeakCurrent = new Tuple<double, double>(20.6, 27.9),
                RiseTimeMax = 0.000000000350,
                FullWidthAtHalfMaximum = new Tuple<double, double>(0.000000000450, 0.000000000900),
                UndershootMaxPercent = -0.5,
            },

            new CDMJS002WaveformCharacteristicsSet()
            {
                IsHighBandwidth = false,
                IsLargeTarget = true,
                TestCondition = 125,
                PeakCurrent = new Tuple<double, double>(1.9, 3.2),
                RiseTimeMax = 0.000000000450,
                FullWidthAtHalfMaximum = new Tuple<double, double>(0.000000000500, 0.000000001000),
                UndershootMaxPercent = -0.5,
            },
            new CDMJS002WaveformCharacteristicsSet()
            {
                IsHighBandwidth = false,
                IsLargeTarget = true,
                TestCondition = 250,
                PeakCurrent = new Tuple<double, double>(4.2, 6.3),
                RiseTimeMax = 0.000000000450,
                FullWidthAtHalfMaximum = new Tuple<double, double>(0.000000000500, 0.000000001000),
                UndershootMaxPercent = -0.5,
            },
            new CDMJS002WaveformCharacteristicsSet()
            {
                IsHighBandwidth = false,
                IsLargeTarget = true,
                TestCondition = 500,
                PeakCurrent = new Tuple<double, double>(9.1, 12.3),
                RiseTimeMax = 0.000000000450,
                FullWidthAtHalfMaximum = new Tuple<double, double>(0.000000000500, 0.000000001000),
                UndershootMaxPercent = -0.5,
            },
            new CDMJS002WaveformCharacteristicsSet()
            {
                IsHighBandwidth = false,
                IsLargeTarget = true,
                TestCondition = 750,
                PeakCurrent = new Tuple<double, double>(13.7, 18.5),
                RiseTimeMax = 0.000000000450,
                FullWidthAtHalfMaximum = new Tuple<double, double>(0.000000000500, 0.000000001000),
                UndershootMaxPercent = -0.5,
            },
            new CDMJS002WaveformCharacteristicsSet()
            {
                IsHighBandwidth = false,
                IsLargeTarget = true,
                TestCondition = 1000,
                PeakCurrent = new Tuple<double, double>(18.3, 24.7),
                RiseTimeMax = 0.000000000450,
                FullWidthAtHalfMaximum = new Tuple<double, double>(0.000000000500, 0.000000001000),
                UndershootMaxPercent = -0.5,
            },

            new CDMJS002WaveformCharacteristicsSet()
            {
                IsHighBandwidth = true,
                IsLargeTarget = false,
                TestCondition = 125,
                PeakCurrent = new Tuple<double, double>(1.4, 2.3),
                RiseTimeMax = 0.000000000250,
                FullWidthAtHalfMaximum = new Tuple<double, double>(0.000000000250, 0.000000000600),
                UndershootMaxPercent = -0.7,
            },
            new CDMJS002WaveformCharacteristicsSet()
            {
                IsHighBandwidth = true,
                IsLargeTarget = false,
                TestCondition = 250,
                PeakCurrent = new Tuple<double, double>(2.9, 4.3),
                RiseTimeMax = 0.000000000250,
                FullWidthAtHalfMaximum = new Tuple<double, double>(0.000000000250, 0.000000000600),
                UndershootMaxPercent = -0.7,
            },
            new CDMJS002WaveformCharacteristicsSet()
            {
                IsHighBandwidth = true,
                IsLargeTarget = false,
                TestCondition = 500,
                PeakCurrent = new Tuple<double, double>(6.1, 8.3),
                RiseTimeMax = 0.000000000250,
                FullWidthAtHalfMaximum = new Tuple<double, double>(0.000000000250, 0.000000000600),
                UndershootMaxPercent = -0.7,
            },
            new CDMJS002WaveformCharacteristicsSet()
            {
                IsHighBandwidth = true,
                IsLargeTarget = false,
                TestCondition = 750,
                PeakCurrent = new Tuple<double, double>(9.2, 12.4),
                RiseTimeMax = 0.000000000250,
                FullWidthAtHalfMaximum = new Tuple<double, double>(0.000000000250, 0.000000000600),
                UndershootMaxPercent = -0.7,
            },
            new CDMJS002WaveformCharacteristicsSet()
            {
                IsHighBandwidth = true,
                IsLargeTarget = false,
                TestCondition = 1000,
                PeakCurrent = new Tuple<double, double>(12.2, 16.5),
                RiseTimeMax = 0.000000000250,
                FullWidthAtHalfMaximum = new Tuple<double, double>(0.000000000250, 0.000000000600),
                UndershootMaxPercent = -0.7,
            },

            new CDMJS002WaveformCharacteristicsSet()
            {
                IsHighBandwidth = false,
                IsLargeTarget = false,
                TestCondition = 125,
                PeakCurrent = new Tuple<double, double>(1.0, 1.6),
                RiseTimeMax = 0.000000000350,
                FullWidthAtHalfMaximum = new Tuple<double, double>(0.000000000325, 0.000000000725),
                UndershootMaxPercent = -0.7,
            },
            new CDMJS002WaveformCharacteristicsSet()
            {
                IsHighBandwidth = false,
                IsLargeTarget = false,
                TestCondition = 250,
                PeakCurrent = new Tuple<double, double>(2.1, 3.1),
                RiseTimeMax = 0.000000000350,
                FullWidthAtHalfMaximum = new Tuple<double, double>(0.000000000325, 0.000000000725),
                UndershootMaxPercent = -0.7,
            },
            new CDMJS002WaveformCharacteristicsSet()
            {
                IsHighBandwidth = false,
                IsLargeTarget = false,
                TestCondition = 500,
                PeakCurrent = new Tuple<double, double>(4.4, 5.9),
                RiseTimeMax = 0.000000000350,
                FullWidthAtHalfMaximum = new Tuple<double, double>(0.000000000325, 0.000000000725),
                UndershootMaxPercent = -0.7,
            },
            new CDMJS002WaveformCharacteristicsSet()
            {
                IsHighBandwidth = false,
                IsLargeTarget = false,
                TestCondition = 750,
                PeakCurrent = new Tuple<double, double>(6.6, 8.9),
                RiseTimeMax = 0.000000000350,
                FullWidthAtHalfMaximum = new Tuple<double, double>(0.000000000325, 0.000000000725),
                UndershootMaxPercent = -0.7,
            },
            new CDMJS002WaveformCharacteristicsSet()
            {
                IsHighBandwidth = false,
                IsLargeTarget = false,
                TestCondition = 1000,
                PeakCurrent = new Tuple<double, double>(8.8, 11.9),
                RiseTimeMax = 0.000000000350,
                FullWidthAtHalfMaximum = new Tuple<double, double>(0.000000000325, 0.000000000725),
                UndershootMaxPercent = -0.7,
            },
        };

        /// <summary>
        /// Returns the max allowed CDM Rise Time to be within JS-002 specification
        /// </summary>
        /// <param name="signedCDMVoltage">The sign value of the CDM pulse waveform</param>
        /// <param name="isLargeTarget">A value indicating whether the CDM target is large or not (if not, then it is small)</param>
        /// <param name="oscilloscopeIsHighBandwidth">A value indicating whether the oscilloscope is high bandwidth (6GHz+) or not</param>
        /// <returns>The max allowed CDM Rise Time to be within JS-002 specification</returns>
        public static double RiseTimeMax(double signedCDMVoltage, bool isLargeTarget, bool oscilloscopeIsHighBandwidth)
        {
            CDMJS002WaveformCharacteristicsSet set = CDMJS002WaveformCharacteristics.GenerateSetForCDMVoltageAndCharacteristics(
                signedCDMVoltage,
                isLargeTarget,
                oscilloscopeIsHighBandwidth);

            return set.RiseTimeMax;
        }

        /// <summary>
        /// Returns the nominal (Item1), min (Item2), and max (Item3) allowed CDM Peak Current to be within the JS-002 specification
        /// </summary>
        /// <param name="signedVoltage">The sign value of the CDM pulse waveform</param>
        /// <param name="isLargeTarget">A value indicating whether the CDM target is large or not (if not, then it is small)</param>
        /// <param name="oscilloscopeIsHighBandwidth">A value indicating whether the oscilloscope is high bandwidth (6GHz+) or not</param>
        /// <returns>The nominal (Item1), min (Item2), and max (Item3) allowed CDM Peak Current to be within the JS-002 specification</returns>
        public static Tuple<double, double, double> PeakCurrentNominalMinMax(double signedVoltage, bool isLargeTarget, bool oscilloscopeIsHighBandwidth)
        {
            CDMJS002WaveformCharacteristicsSet set = CDMJS002WaveformCharacteristics.GenerateSetForCDMVoltageAndCharacteristics(
                signedVoltage,
                isLargeTarget,
                oscilloscopeIsHighBandwidth);

            return new Tuple<double, double, double>(
                DoubleRangeExtensions.CenterOfRange(set.PeakCurrent.Item1, set.PeakCurrent.Item2),
                set.PeakCurrent.Item1,
                set.PeakCurrent.Item2);
        }

        /// <summary>
        /// Returns the nominal (Item1), min (Item2), and max (Item3) allowed CDM Full Width at Half Maximum to be within the JS-002 specification
        /// </summary>
        /// <param name="signedCDMVoltage">The sign value of the CDM pulse waveform</param>
        /// <param name="isLargeTarget">A value indicating whether the CDM target is large or not (if not, then it is small)</param>
        /// <param name="oscilloscopeIsHighBandwidth">A value indicating whether the oscilloscope is high bandwidth (6GHz+) or not</param>
        /// <returns>The nominal (Item1), min (Item2), and max (Item3) allowed CDM Full Width at Half Maximum to be within the JS-002 specification</returns>
        public static Tuple<double, double, double> FullWidthHalfMaxNominalMinMax(double signedCDMVoltage, bool isLargeTarget, bool oscilloscopeIsHighBandwidth)
        {
            CDMJS002WaveformCharacteristicsSet set = CDMJS002WaveformCharacteristics.GenerateSetForCDMVoltageAndCharacteristics(
                signedCDMVoltage,
                isLargeTarget,
                oscilloscopeIsHighBandwidth);

            return new Tuple<double, double, double>(
                DoubleRangeExtensions.CenterOfRange(set.FullWidthAtHalfMaximum.Item1, set.FullWidthAtHalfMaximum.Item2),
                set.FullWidthAtHalfMaximum.Item1,
                set.FullWidthAtHalfMaximum.Item2);
        }

        /// <summary>
        /// Returns the max allowed CDM Undershoot as a percentage to be within the JS-002 specification
        /// </summary>
        /// <param name="signedCDMVoltage">The sign value of the CDM pulse waveform</param>
        /// <param name="isLargeTarget">A value indicating whether the CDM target is large or not (if not, then it is small)</param>
        /// <param name="oscilloscopeIsHighBandwidth">A value indicating whether the oscilloscope is high bandwidth (6GHz+) or not</param>
        /// <returns>The max allowed CDM Undershoot as a percentage to be within ESDA/JEDEC joint specification</returns>
        public static double UndershootMaxPercent(double signedCDMVoltage, bool isLargeTarget, bool oscilloscopeIsHighBandwidth)
        {
            CDMJS002WaveformCharacteristicsSet set = CDMJS002WaveformCharacteristics.GenerateSetForCDMVoltageAndCharacteristics(
                signedCDMVoltage,
                isLargeTarget,
                oscilloscopeIsHighBandwidth);

            return set.UndershootMaxPercent;
        }

        /// <summary>
        /// Generates a new set for the CDM voltage, interpolated from the JS-002 specification.
        /// </summary>
        /// <param name="cdmVoltage">The voltage of the CDM pulse waveform (polarity is ignored, absolute value will be used)</param>
        /// <param name="isLargeTarget">A value indicating whether the CDM target is large or not (if not, then it is small)</param>
        /// <param name="oscilloscopeIsHighBandwidth">A value indicating whether the oscilloscope is high bandwidth (6GHz+) or not</param>
        /// <returns>A new set for the CDM voltage, interpolated from the JS-002 specification</returns>
        private static CDMJS002WaveformCharacteristicsSet GenerateSetForCDMVoltageAndCharacteristics(double cdmVoltage, bool isLargeTarget, bool oscilloscopeIsHighBandwidth)
        {
            double absCDMVoltage = System.Math.Abs(cdmVoltage);

            List<CDMJS002WaveformCharacteristicsSet> possibleSets =
                (from set in CDMJS002WaveformCharacteristics.characteristicSets
                 where set.IsLargeTarget == isLargeTarget && set.IsHighBandwidth == oscilloscopeIsHighBandwidth
                 select set).ToList();

            if (possibleSets.Any(set => set.TestCondition == absCDMVoltage))
            {
                // If the voltage is an exact match to a set, use it
                return possibleSets.First(set => set.TestCondition == absCDMVoltage).Clone();
            }
            else
            {
                // The voltage isn't an exact match to the table, so interpolate it
                CDMJS002WaveformCharacteristicsSet below = null;
                CDMJS002WaveformCharacteristicsSet above = null;

                // Try to find the closest sets that are below and above the Test Condition voltage
                foreach (CDMJS002WaveformCharacteristicsSet set in possibleSets)
                {
                    if (set.TestCondition < absCDMVoltage)
                    {
                        if (below == null || below.TestCondition < set.TestCondition)
                        {
                            below = set;
                        }
                    }
                    else
                    {
                        if (above == null || above.TestCondition > set.TestCondition)
                        {
                            above = set;
                        }
                    }
                }

                if (below != null && above != null)
                {
                    // Interpolate between the below and above table entries
                    double percentWithinRange = DoubleRangeExtensions.PercentWithinRange(absCDMVoltage, above.TestCondition, below.TestCondition);

                    // Only the Peak Current varies between different Test Conditions.
                    // All other properties are constant for a given target size and bandwidth.
                    Tuple<double, double> interpolatedPeakCurrent = new Tuple<double, double>(
                        DoubleRangeExtensions.EquivalentValueInNewRange(percentWithinRange, 1, 0, above.PeakCurrent.Item1, below.PeakCurrent.Item1),
                        DoubleRangeExtensions.EquivalentValueInNewRange(percentWithinRange, 1, 0, above.PeakCurrent.Item2, below.PeakCurrent.Item2));

                    return new CDMJS002WaveformCharacteristicsSet()
                    {
                        IsHighBandwidth = below.IsHighBandwidth,
                        IsLargeTarget = below.IsLargeTarget,
                        TestCondition = absCDMVoltage,
                        PeakCurrent = interpolatedPeakCurrent,
                        RiseTimeMax = below.RiseTimeMax,
                        FullWidthAtHalfMaximum = new Tuple<double, double>(below.FullWidthAtHalfMaximum.Item1, below.FullWidthAtHalfMaximum.Item2),
                        UndershootMaxPercent = below.UndershootMaxPercent,
                    };
                }
                else if (below != null)
                {
                    // The CDM voltage is higher than the highest table entry, so scale the largest one
                    double multiplier = absCDMVoltage / below.TestCondition;

                    // Only the Peak Current varies between different Test Conditions.
                    // All other properties are constant for a given target size and bandwidth.
                    Tuple<double, double> scaledPeakCurrent = new Tuple<double, double>(
                        below.PeakCurrent.Item1 * multiplier,
                        below.PeakCurrent.Item2 * multiplier);

                    return new CDMJS002WaveformCharacteristicsSet()
                    {
                        IsHighBandwidth = below.IsHighBandwidth,
                        IsLargeTarget = below.IsLargeTarget,
                        TestCondition = absCDMVoltage,
                        PeakCurrent = scaledPeakCurrent,
                        RiseTimeMax = below.RiseTimeMax,
                        FullWidthAtHalfMaximum = new Tuple<double, double>(below.FullWidthAtHalfMaximum.Item1, below.FullWidthAtHalfMaximum.Item2),
                        UndershootMaxPercent = below.UndershootMaxPercent,
                    };
                }
                else if (above != null)
                {
                    // The CDM voltage is lower than the lowest table entry, so scale the smallest one
                    double multiplier = absCDMVoltage / above.TestCondition;

                    // Only the Peak Current varies between different Test Conditions.
                    // All other properties are constant for a given target size and bandwidth.
                    Tuple<double, double> scaledPeakCurrent = new Tuple<double, double>(
                        above.PeakCurrent.Item1 * multiplier,
                        above.PeakCurrent.Item2 * multiplier);

                    return new CDMJS002WaveformCharacteristicsSet()
                    {
                        IsHighBandwidth = above.IsHighBandwidth,
                        IsLargeTarget = above.IsLargeTarget,
                        TestCondition = absCDMVoltage,
                        PeakCurrent = scaledPeakCurrent,
                        RiseTimeMax = above.RiseTimeMax,
                        FullWidthAtHalfMaximum = new Tuple<double, double>(above.FullWidthAtHalfMaximum.Item1, above.FullWidthAtHalfMaximum.Item2),
                        UndershootMaxPercent = above.UndershootMaxPercent,
                    };
                }
                else
                {
                    throw new InvalidOperationException("Finding table entries for the CDM voltage " + absCDMVoltage + " failed.");
                }
            }
        }

        /// <summary>
        /// Class that represents the JS-002 allowed characteristics of a CDM waveform for the given Test Condition, Target Size, and oscilloscope bandwidth.
        /// </summary>
        private class CDMJS002WaveformCharacteristicsSet
        {
            /// <summary>
            /// Gets or sets a value indicating whether the oscilloscope is high bandwidth (6GHz+) or not
            /// </summary>
            public bool IsHighBandwidth { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether the CDM target is large or not (if not, then it is small)
            /// </summary>
            public bool IsLargeTarget { get; set; }

            /// <summary>
            /// Gets or sets the test condition the set represents, as Voltage
            /// </summary>
            public double TestCondition { get; set; }

            /// <summary>
            /// Gets or sets the allowed min (Item1) max (Item2) range of the Peak Current in Amps
            /// </summary>
            public Tuple<double, double> PeakCurrent { get; set; }

            /// <summary>
            /// Gets or sets the maximum allowed risetime in Seconds
            /// </summary>
            public double RiseTimeMax { get; set; }

            /// <summary>
            /// Gets or sets the allowed min (Item1) max (Item2) range of the Full Width at Half Maximum in Seconds
            /// </summary>
            public Tuple<double, double> FullWidthAtHalfMaximum { get; set; }

            /// <summary>
            /// Gets or sets the maximum allowed undershoot as a percent (0.0 - 1.0)
            /// </summary>
            public double UndershootMaxPercent { get; set; }

            /// <summary>
            /// Returns a copy of this set
            /// </summary>
            /// <returns>A copy of this set</returns>
            public CDMJS002WaveformCharacteristicsSet Clone()
            {
                return new CDMJS002WaveformCharacteristicsSet()
                {
                    IsHighBandwidth = this.IsHighBandwidth,
                    IsLargeTarget = this.IsLargeTarget,
                    TestCondition = this.TestCondition,
                    PeakCurrent = new Tuple<double, double>(this.PeakCurrent.Item1, this.PeakCurrent.Item2),
                    RiseTimeMax = this.RiseTimeMax,
                    FullWidthAtHalfMaximum = new Tuple<double, double>(this.FullWidthAtHalfMaximum.Item1, this.FullWidthAtHalfMaximum.Item2),
                    UndershootMaxPercent = this.UndershootMaxPercent,
                };
            }
        }
    }
}
