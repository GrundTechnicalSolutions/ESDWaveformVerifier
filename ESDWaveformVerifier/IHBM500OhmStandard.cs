// ---------------------------------------------------------------------------------------------------
//  <copyright file="IHBM500OhmStandard.cs" company="Grund Technical Solutions, Inc">
//      Copyright (c) Grund Technical Solutions, Inc. All rights reserved.
//  </copyright>
// ---------------------------------------------------------------------------------------------------
namespace ESDWaveformVerifier
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ESDWaveformVerifier.DataTypes;

    /// <summary>
    /// Interface that provides calculations for a HBM 500-Ohm Standard
    /// </summary>
    public interface IHBM500OhmStandard : IStandard
    {
        /// <summary>
        /// Gets the calculated Peak Current (Ips) Value
        /// </summary>
        double PeakCurrentValue { get; }

        /// <summary>
        /// Gets the calculated Peak Current (Ips) DataPoint
        /// </summary>
        DataPoint PeakCurrentDataPoint { get; }

        /// <summary>
        /// Gets the calculated Peak Current (Ips) allowed minimum
        /// </summary>
        double PeakCurrentAllowedMinimum { get; }

        /// <summary>
        /// Gets the calculated Peak Current (Ips) allowed maximum
        /// </summary>
        double PeakCurrentAllowedMaximum { get; }

        /// <summary>
        /// Gets a value indicating whether the Peak Current (Ips) Value is passing or not
        /// </summary>
        bool PeakCurrentIsPassing { get; }

        /// <summary>
        /// Gets the calculated Rise Time Value
        /// </summary>
        double RiseTimeValue { get; }

        /// <summary>
        /// Gets the calculated Rise Time starting DataPoint (interpolated)
        /// </summary>
        DataPoint RiseTimeStartDataPoint { get; }

        /// <summary>
        /// Gets the calculated Rise Time ending DataPoint (interpolated)
        /// </summary>
        DataPoint RiseTimeEndDataPoint { get; }

        /// <summary>
        /// Gets the Rise Time allowed minimum
        /// </summary>
        double RiseTimeAllowedMinimum { get; }

        /// <summary>
        /// Gets the Rise Time allowed maximum
        /// </summary>
        double RiseTimeAllowedMaximum { get; }

        /// <summary>
        /// Gets a value indicating whether the Rise Time Value is passing or not
        /// </summary>
        bool RiseTimeIsPassing { get; }
    }
}
