// ---------------------------------------------------------------------------------------------------
//  <copyright file="IHBM0OhmStandard.cs" company="Grund Technical Solutions, Inc">
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
    /// Interface that provides calculations for a HBM 0-Ohm Standard
    /// </summary>
    public interface IHBM0OhmStandard : IStandard
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

        /// <summary>
        /// Gets the calculated Decay Time Value
        /// </summary>
        double DecayTimeValue { get; }

        /// <summary>
        /// Gets the calculated Decay Time starting DataPoint (interpolated)
        /// </summary>
        DataPoint DecayTimeStartDataPoint { get; }

        /// <summary>
        /// Gets the calculated Decay Time ending DataPoint (interpolated)
        /// </summary>
        DataPoint DecayTimeEndDataPoint { get; }

        /// <summary>
        /// Gets the calculated Decay Time allowed minimum
        /// </summary>
        double DecayTimeAllowedMinimum { get; }

        /// <summary>
        /// Gets the calculated Decay Time allowed maximum
        /// </summary>
        double DecayTimeAllowedMaximum { get; }

        /// <summary>
        /// Gets a value indicating whether the Decay Time Value is passing or not
        /// </summary>
        bool DecayTimeIsPassing { get; }

        /// <summary>
        /// Gets the calculated Total Ring Percent Value
        /// </summary>
        double TotalRingPercentValue { get; }

        /// <summary>
        /// Gets the Total Ring allowed maximum as a %
        /// </summary>
        double TotalRingAllowedMaximum { get; }

        /// <summary>
        /// Gets a value indicating whether the Total Ring Value is passing or not
        /// </summary>
        bool TotalRingIsPassing { get; }

        /// <summary>
        /// Gets the Ring1 peak DataPoint (maximum positive ring)
        /// </summary>
        DataPoint Ring1PeakDataPoint { get; }

        /// <summary>
        /// Gets the Ring2 peak DataPoint (maximum negative ring)
        /// </summary>
        DataPoint Ring2PeakDataPoint { get; }

        /// <summary>
        /// Gets the 5th Degree Polynomial that represents the Least Squares Fit line derived from Ips until Ips+40ns time of the absolute Waveform
        /// </summary>
        FifthDegreePolynomial AbsoluteIpsPolynomial { get; }
    }
}
