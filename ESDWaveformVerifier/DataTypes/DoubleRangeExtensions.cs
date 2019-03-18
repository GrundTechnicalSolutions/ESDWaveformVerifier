// ---------------------------------------------------------------------------------------------------
//  <copyright file="DoubleRangeExtensions.cs" company="Grund Technical Solutions, Inc">
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
    /// Provides extended functionality for double ranges
    /// </summary>
    internal static class DoubleRangeExtensions
    {
        /// <summary>
        /// Returns a value that is exactly between the two range values
        /// </summary>
        /// <param name="rangeVal1">The first range value</param>
        /// <param name="rangeVal2">The second range value</param>
        /// <returns>A value that is exactly between the two range values</returns>
        internal static double CenterOfRange(double rangeVal1, double rangeVal2)
        {
            return ((rangeVal2 - rangeVal1) / 2.0) + rangeVal1;
        }

        /// <summary>
        /// Returns a value that is in the equivalent position within the new range as it was in the original range.
        /// </summary>
        /// <param name="origValue">The original value to find the equivalent position value of in the new range</param>
        /// <param name="origRangeMax">The original range maximum value</param>
        /// <param name="origRangeMin">The original range minimum value</param>
        /// <param name="newRangeMax">The new range maximum value</param>
        /// <param name="newRangeMin">The new range minimum value</param>
        /// <returns>A value that is in the equivalent position within the new range as it was in the original range.</returns>
        internal static double EquivalentValueInNewRange(double origValue, double origRangeMax, double origRangeMin, double newRangeMax, double newRangeMin)
        {
            double origRange = origRangeMax - origRangeMin;
            if (origRange == 0)
            {
                throw new ArgumentOutOfRangeException("Original range cannot be 0");
            }

            double newRange = newRangeMax - newRangeMin;
            if (newRange == 0)
            {
                throw new ArgumentOutOfRangeException("New range cannot be 0");
            }

            double newValue = (((origValue - origRangeMin) * newRange) / origRange) + newRangeMin;
            return newValue;
        }

        /// <summary>
        /// Returns a value that is where the value lies between rangeMin and rangeMax as a percentage.
        /// If value is very close to rangeMin, it might return 0.01 (1%).
        /// If value is very close to rangeMax, it might return 0.99 (99%).
        /// </summary>
        /// <param name="value">The value to find where it lies percentage wise between rangeMin and rangeMax</param>
        /// <param name="rangeMax">The range maximum</param>
        /// <param name="rangeMin">The range minimum</param>
        /// <returns>A value that is where the value lies between rangeMin and rangeMax as a percentage.</returns>
        internal static double PercentWithinRange(double value, double rangeMax, double rangeMin)
        {
            return EquivalentValueInNewRange(value, rangeMax, rangeMin, 1, 0);
        }

        /// <summary>
        /// Determines if a value is in the given range.
        /// </summary>
        /// <param name="boundary1">The first boundary the value must be between in order to be true.</param>
        /// <param name="boundary2">The second boundary the value must be between in order to be true.</param>
        /// <param name="testValue">The value to test.</param>
        /// <returns>A boolean value indicating whether the test value is in the given range.</returns>
        internal static bool BetweenInclusive(double boundary1, double boundary2, double testValue)
        {
            if (boundary1 < boundary2)
            {
                return (testValue >= boundary1) && (testValue <= boundary2);
            }
            else
            {
                return (testValue <= boundary1) && (testValue >= boundary2);
            }
        }
    }
}
