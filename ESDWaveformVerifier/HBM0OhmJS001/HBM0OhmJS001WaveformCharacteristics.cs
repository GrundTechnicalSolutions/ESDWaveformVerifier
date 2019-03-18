// ---------------------------------------------------------------------------------------------------
//  <copyright file="HBM0OhmJS001WaveformCharacteristics.cs" company="Grund Technical Solutions, Inc">
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
    /// A class that represents the HBM 0-Ohm JS-001 Waveform Specifications as found in JS-001 - Section 5.5 - Table 1
    /// </summary>
    internal static class HBM0OhmJS001WaveformCharacteristics
    {
        /// <summary>
        /// Private backing store for the sets of HBM JS-001 Specifications.
        /// These are derived from JS-001 - Section 5.5 - Table 1
        /// </summary>
        private static List<HBM0OhmJS001WaveformCharacteristicsSet> characteristicSets = new List<HBM0OhmJS001WaveformCharacteristicsSet>()
        {
            new HBM0OhmJS001WaveformCharacteristicsSet()
            {
                TestCondition = 125,
                PeakCurrent = new Tuple<double, double>(0.075, 0.092),
            },
            new HBM0OhmJS001WaveformCharacteristicsSet()
            {
                TestCondition = 250,
                PeakCurrent = new Tuple<double, double>(0.15, 0.18),
            },
            new HBM0OhmJS001WaveformCharacteristicsSet()
            {
                TestCondition = 500,
                PeakCurrent = new Tuple<double, double>(0.30, 0.37),
            },
            new HBM0OhmJS001WaveformCharacteristicsSet()
            {
                TestCondition = 1000,
                PeakCurrent = new Tuple<double, double>(0.60, 0.73),
            },
            new HBM0OhmJS001WaveformCharacteristicsSet()
            {
                TestCondition = 2000,
                PeakCurrent = new Tuple<double, double>(1.20, 1.47),
            },
            new HBM0OhmJS001WaveformCharacteristicsSet()
            {
                TestCondition = 4000,
                PeakCurrent = new Tuple<double, double>(2.40, 2.93),
            },
            new HBM0OhmJS001WaveformCharacteristicsSet()
            {
                TestCondition = 8000,
                PeakCurrent = new Tuple<double, double>(4.80, 5.87),
            },
        };

        /// <summary>
        /// Returns the nominal (Item1), min (Item2), and max (Item3) allowed HBM Peak Current to be within JS-001 specification
        /// </summary>
        /// <param name="hbmVoltage">The voltage of the HBM pulse waveform (polarity is ignored, absolute value will be used)</param>
        /// <returns>The nominal (Item1), min (Item2), and max (Item3) allowed HBM Peak Current to be within JS-001 specification</returns>
        internal static Tuple<double, double, double> HBMPeakCurrentNominalMinMax(double hbmVoltage)
        {
            double absHBMVoltage = System.Math.Abs(hbmVoltage);

            HBM0OhmJS001WaveformCharacteristicsSet set = HBM0OhmJS001WaveformCharacteristics.GenerateSetForHBMVoltage(absHBMVoltage);

            return new Tuple<double, double, double>(
                DoubleRangeExtensions.CenterOfRange(set.PeakCurrent.Item1, set.PeakCurrent.Item2),
                set.PeakCurrent.Item1,
                set.PeakCurrent.Item2);
        }

        /// <summary>
        /// Generates a new set for the HBM voltage from the JS-001 standard
        /// </summary>
        /// <param name="hbmVoltage">The voltage of the HBM pulse waveform (polarity is ignored, absolute value will be used)</param>
        /// <returns>A new set for the HBM voltage from the JS-001 standard</returns>
        private static HBM0OhmJS001WaveformCharacteristicsSet GenerateSetForHBMVoltage(double hbmVoltage)
        {
            double absHBMVoltage = System.Math.Abs(hbmVoltage);

            if (HBM0OhmJS001WaveformCharacteristics.characteristicSets.Any(set => set.TestCondition == absHBMVoltage))
            {
                // If the voltage is an exact match to a set, use it
                return HBM0OhmJS001WaveformCharacteristics.characteristicSets.First(set => set.TestCondition == absHBMVoltage).Clone();
            }
            else
            {
                // The voltage isn't an exact match to the table, so interpolate it
                HBM0OhmJS001WaveformCharacteristicsSet below = null;
                HBM0OhmJS001WaveformCharacteristicsSet above = null;

                // Try to find the closest sets that are below and above the Test Condition voltage
                foreach (HBM0OhmJS001WaveformCharacteristicsSet set in HBM0OhmJS001WaveformCharacteristics.characteristicSets)
                {
                    if (set.TestCondition < absHBMVoltage)
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
                    double percentWithinRange = DoubleRangeExtensions.PercentWithinRange(absHBMVoltage, above.TestCondition, below.TestCondition);

                    // Only the Peak Current varies between different Test Conditions.
                    // All other properties are constant for a given target size and bandwidth.
                    Tuple<double, double> interpolatedPeakCurrent = new Tuple<double, double>(
                        DoubleRangeExtensions.EquivalentValueInNewRange(percentWithinRange, 1, 0, above.PeakCurrent.Item1, below.PeakCurrent.Item1),
                        DoubleRangeExtensions.EquivalentValueInNewRange(percentWithinRange, 1, 0, above.PeakCurrent.Item2, below.PeakCurrent.Item2));

                    return new HBM0OhmJS001WaveformCharacteristicsSet()
                    {
                        TestCondition = absHBMVoltage,
                        PeakCurrent = interpolatedPeakCurrent,
                    };
                }
                else if (below != null)
                {
                    // The HBM voltage is higher than the highest table entry, so scale the largest one
                    double multiplier = absHBMVoltage / below.TestCondition;

                    // Only the Peak Current varies between different Test Conditions.
                    // All other properties are constant for a given target size and bandwidth.
                    Tuple<double, double> scaledPeakCurrent = new Tuple<double, double>(
                        below.PeakCurrent.Item1 * multiplier,
                        below.PeakCurrent.Item2 * multiplier);

                    return new HBM0OhmJS001WaveformCharacteristicsSet()
                    {
                        TestCondition = absHBMVoltage,
                        PeakCurrent = scaledPeakCurrent,
                    };
                }
                else if (above != null)
                {
                    // The HBM voltage is lower than the lowest table entry, so scale the smallest one
                    double multiplier = absHBMVoltage / above.TestCondition;

                    // Only the Peak Current varies between different Test Conditions.
                    // All other properties are constant for a given target size and bandwidth.
                    Tuple<double, double> scaledPeakCurrent = new Tuple<double, double>(
                        above.PeakCurrent.Item1 * multiplier,
                        above.PeakCurrent.Item2 * multiplier);

                    return new HBM0OhmJS001WaveformCharacteristicsSet()
                    {
                        TestCondition = absHBMVoltage,
                        PeakCurrent = scaledPeakCurrent,
                    };
                }
                else
                {
                    throw new InvalidOperationException("Finding table entries for the HBM voltage " + absHBMVoltage + " failed.");
                }
            }
        }

        /// <summary>
        /// Class that represents the allowed characteristics of a HBM waveform for the given Test Condition.
        /// </summary>
        private class HBM0OhmJS001WaveformCharacteristicsSet
        {
            /// <summary>
            /// Gets or sets the test condition the set represents, as Voltage
            /// </summary>
            public double TestCondition { get; set; }

            /// <summary>
            /// Gets or sets the allowed min (Item1) max (Item2) range of the Peak Current in Amps
            /// </summary>
            public Tuple<double, double> PeakCurrent { get; set; }

            /// <summary>
            /// Returns a copy of this set
            /// </summary>
            /// <returns>A copy of this set</returns>
            public HBM0OhmJS001WaveformCharacteristicsSet Clone()
            {
                return new HBM0OhmJS001WaveformCharacteristicsSet()
                {
                    TestCondition = this.TestCondition,
                    PeakCurrent = new Tuple<double, double>(this.PeakCurrent.Item1, this.PeakCurrent.Item2),
                };
            }
        }
    }
}