// ---------------------------------------------------------------------------------------------------
//  <copyright file="DataPointExtensions.cs" company="Grund Technical Solutions, Inc">
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
    /// An extension class that provides additional DataPoint functionality
    /// </summary>
    internal static class DataPointExtensions
    {
        /// <summary>
        /// Returns a DataPoint that is inverted (Y-value only) if the polarity is negative, and the original waveform if positive
        /// </summary>
        /// <param name="dataPoint">The DataPoint to invert (Y-value only) if the polarity is negative</param>
        /// <param name="isPositivePolarity">A value indicating whether the polarity is positive or not</param>
        /// <returns>A DataPoint that is inverted (Y-value only) if the polarity is negative, and the original DataPoint if positive</returns>
        internal static DataPoint InvertYValueIfNegativePolarity(this DataPoint dataPoint, bool isPositivePolarity)
        {
            return isPositivePolarity ?
                dataPoint :
                new DataPoint(dataPoint.X, dataPoint.Y * -1.0);
        }
    }
}
