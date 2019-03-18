// ---------------------------------------------------------------------------------------------------
//  <copyright file="Standard.cs" company="Grund Technical Solutions, Inc">
//      Copyright (c) Grund Technical Solutions, Inc. All rights reserved.
//  </copyright>
// ---------------------------------------------------------------------------------------------------
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("ESDWaveformVerifierTests")]
namespace ESDWaveformVerifier
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ESDWaveformVerifier.DataTypes;

    /// <summary>
    /// A base class for all Standards
    /// </summary>
    public abstract class Standard
    {
        /// <summary>
        /// Private backing store
        /// </summary>
        private Waveform absoluteWaveform = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="Standard"/> class
        /// </summary>
        /// <param name="waveform">The waveform to provide calculations on</param>
        /// <param name="signedVoltage">The signed voltage</param>
        public Standard(Waveform waveform, double signedVoltage)
        {
            this.Waveform = waveform;
            this.SignedVoltage = signedVoltage;
        }

        /// <summary>
        /// Gets the signed voltage
        /// </summary>
        public double SignedVoltage
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the waveform to provide calculations on
        /// </summary>
        public Waveform Waveform
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the waveform that has been inverted if the original waveform is a negative polarity, and the original waveform if positive polarity.
        /// </summary>
        protected Waveform AbsoluteWaveform
        {
            get
            {
                if (!this.WaveformIsPositivePolarity)
                {
                    if (this.absoluteWaveform == null)
                    {
                        this.absoluteWaveform = this.Waveform.ScaleVertically(-1.0);
                    }

                    return this.absoluteWaveform;
                }
                else
                {
                    return this.Waveform;
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether the waveform is a positive polarity or not
        /// </summary>
        protected bool WaveformIsPositivePolarity
        {
            get { return this.SignedVoltage > 0; }
        }
    }
}
