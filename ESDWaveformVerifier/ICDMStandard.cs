// ---------------------------------------------------------------------------------------------------
//  <copyright file="ICDMStandard.cs" company="Grund Technical Solutions, Inc">
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
    /// Interface that provides calculations for a CDM Standard
    /// </summary>
    public interface ICDMStandard : IStandard
    {
        /// <summary>
        /// Gets a value indicating whether the CDM target is the Large target or not (small if not)
        /// </summary>
        bool IsLargeTarget { get; }

        /// <summary>
        /// Gets a value indicating whether the oscilloscope used is high bandwidth or not
        /// </summary>
        bool OscilloscopeIsHighBandwidth { get; }

        /// <summary>
        /// Gets the calculated Peak Current Value
        /// </summary>
        double PeakCurrentValue { get; }

        /// <summary>
        /// Gets the calculated Peak Current DataPoint
        /// </summary>
        DataPoint PeakCurrentDataPoint { get; }

        /// <summary>
        /// Gets the calculated Peak Current allowed minimum
        /// </summary>
        double PeakCurrentAllowedMinimum { get; }

        /// <summary>
        /// Gets the calculated Peak Current allowed maximum
        /// </summary>
        double PeakCurrentAllowedMaximum { get; }

        /// <summary>
        /// Gets a value indicating whether the Peak Current Value is passing or not
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
        /// Gets the calculated Rise Time allowed minimum
        /// </summary>
        double RiseTimeAllowedMinimum { get; }

        /// <summary>
        /// Gets the calculated Rise Time allowed maximum
        /// </summary>
        double RiseTimeAllowedMaximum { get; }

        /// <summary>
        /// Gets a value indicating whether the Rise Time Value is passing or not
        /// </summary>
        bool RiseTimeIsPassing { get; }

        /// <summary>
        /// Gets the calculated Full Width at Half Maximum Value
        /// </summary>
        double FullWidthHalfMaxValue { get; }

        /// <summary>
        /// Gets the calculated Full Width at Half Maximum starting DataPoint
        /// </summary>
        DataPoint FullWidthHalfMaxStartDataPoint { get; }

        /// <summary>
        /// Gets the calculated Full Width at Half Maximum ending DataPoint
        /// </summary>
        DataPoint FullWidthHalfMaxEndDataPoint { get; }

        /// <summary>
        /// Gets the calculated Full Width at Half Maximum allowed minimum
        /// </summary>
        double FullWidthHalfMaxAllowedMinimum { get; }

        /// <summary>
        /// Gets the calculated Full Width at Half Maximum allowed maximum
        /// </summary>
        double FullWidthHalfMaxAllowedMaximum { get; }

        /// <summary>
        /// Gets a value indicating whether the Full Width at Half Maximum Value is passing or not
        /// </summary>
        bool FullWidthHalfMaxIsPassing { get; }

        /// <summary>
        /// Gets the signed Undershoot Value
        /// </summary>
        double UndershootValue { get; }

        /// <summary>
        /// Gets the signed Undershoot DataPoint
        /// </summary>
        DataPoint UndershootDataPoint { get; }

        /// <summary>
        /// Gets the Undershoot allowed maximum as a percentage
        /// </summary>
        double UndershootAllowedMaximumPercent { get; }

        /// <summary>
        /// Gets the Undershoot allowed maximum value
        /// </summary>
        double UndershootAllowedMaximumValue { get; }

        /// <summary>
        /// Gets a value indicating whether the Undershoot Value is passing or not
        /// </summary>
        bool UndershootIsPassing { get; }
    }
}
