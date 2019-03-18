// ---------------------------------------------------------------------------------------------------
//  <copyright file="ValueExtensions.cs" company="Grund Technical Solutions, Inc">
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
    /// An extension class that provides additional value-type functionality
    /// </summary>
    internal static class ValueExtensions
    {
        /// <summary>
        /// Returns a value that is inverted if the polarity is negative, and the original value if positive
        /// </summary>
        /// <param name="value">The value to invert if the polarity is negative</param>
        /// <param name="isPositivePolarity">A value indicating whether the polarity is positive or not</param>
        /// <returns>A value that is inverted if the polarity is negative, and the original value if positive</returns>
        internal static double InvertValueIfNegativePolarity(this double value, bool isPositivePolarity)
        {
            return isPositivePolarity ?
                value :
                value * -1.0;
        }
    }
}
