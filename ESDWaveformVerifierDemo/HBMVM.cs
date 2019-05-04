// ---------------------------------------------------------------------------------------------------
//  <copyright file="HBMVM.cs" company="Grund Technical Solutions, Inc">
//      Copyright (c) Grund Technical Solutions, Inc. All rights reserved.
//  </copyright>
// ---------------------------------------------------------------------------------------------------
namespace ESDWaveformVerifierDemo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ESDWaveformVerifier;
    using ESDWaveformVerifier.HBM0OhmJS001;
    using ESDWaveformVerifier.HBM500OhmJS001;
    using Prism.Commands;
    using Prism.Mvvm;

    /// <summary>
    /// Class that shows HBM controls
    /// </summary>
    internal class HBMVM : WaveformVM
    {
        /// <summary>
        /// Private backing store
        /// </summary>
        private bool is0Ohm = true;

        /// <summary>
        /// Private backing store
        /// </summary>
        private bool is500Ohm = false;

        /// <summary>
        /// Gets or sets a value indicating whether the DUT is 0-Ohm or not
        /// </summary>
        public bool Is0Ohm
        {
            get
            {
                return this.is0Ohm;
            }

            set
            {
                this.SetProperty(ref this.is0Ohm, value);
                this.is500Ohm = !this.is0Ohm;
                this.RaisePropertyChanged(nameof(this.Is500Ohm));
                this.HandleInputChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the DUT is 500-Ohm or not
        /// </summary>
        public bool Is500Ohm
        {
            get
            {
                return this.is500Ohm;
            }

            set
            {
                this.SetProperty(ref this.is500Ohm, value);
                this.is0Ohm = !this.is500Ohm;
                this.RaisePropertyChanged(nameof(this.Is0Ohm));
                this.HandleInputChanged();
            }
        }

        /// <summary>
        /// Calculate the Standard results if possible.  Returns null if not possible.
        /// </summary>
        /// <returns>The Standard results if possible.  Returns null if not possible.</returns>
        protected override IStandard CalculateStandard()
        {
            if (this.OpenedWaveform != null && this.VoltageIsValid())
            {
                return this.Is0Ohm ?
                    (IStandard)new HBM0OhmJS001Standard(this.OpenedWaveform, this.Voltage) :
                    (IStandard)new HBM500OhmJS001Standard(this.OpenedWaveform, this.Voltage);
            }
            else
            {
                return null;
            }
        }
    }
}
