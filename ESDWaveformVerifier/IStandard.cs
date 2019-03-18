// ---------------------------------------------------------------------------------------------------
//  <copyright file="IStandard.cs" company="Grund Technical Solutions, Inc">
//      Copyright (c) Grund Technical Solutions, Inc. All rights reserved.
//  </copyright>
// ---------------------------------------------------------------------------------------------------
namespace ESDWaveformVerifier
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using ESDWaveformVerifier.DataTypes;

    /// <summary>
    /// An interface for all Standards
    /// </summary>
    public interface IStandard
    {
        /// <summary>
        /// Gets the signed voltage
        /// </summary>
        double SignedVoltage { get; }

        /// <summary>
        /// Gets the waveform to provide calculations on
        /// </summary>
        Waveform Waveform { get; }
    }
}
