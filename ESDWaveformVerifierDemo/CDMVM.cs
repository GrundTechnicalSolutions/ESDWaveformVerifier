// ---------------------------------------------------------------------------------------------------
//  <copyright file="CDMVM.cs" company="Grund Technical Solutions, Inc">
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
    using ESDWaveformVerifier.CDMJS002;
    using ESDWaveformVerifier.DataTypes;
    using Prism.Mvvm;

    /// <summary>
    /// Class that shows CDM controls
    /// </summary>
    internal class CDMVM : WaveformVM
    {
        /// <summary>
        /// Private backing store
        /// </summary>
        private bool isLargeTarget = true;

        /// <summary>
        /// Private backing store
        /// </summary>
        private bool isSmallTarget = false;

        /// <summary>
        /// Private backing store
        /// </summary>
        private bool isHighBandwidth = true;

        /// <summary>
        /// Private backing store
        /// </summary>
        private bool isLowBandwidth = false;

        /// <summary>
        /// Gets or sets a value indicating whether the target is Large or not
        /// </summary>
        public bool IsLargeTarget
        {
            get
            {
                return this.isLargeTarget;
            }

            set
            {
                this.SetProperty(ref this.isLargeTarget, value);
                this.isSmallTarget = !this.isLargeTarget;
                this.RaisePropertyChanged(nameof(this.IsSmallTarget));
                this.HandleInputChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the target is Small or not
        /// </summary>
        public bool IsSmallTarget
        {
            get
            {
                return this.isSmallTarget;
            }

            set
            {
                this.SetProperty(ref this.isSmallTarget, value);
                this.isLargeTarget = !this.isSmallTarget;
                this.RaisePropertyChanged(nameof(this.IsLargeTarget));
                this.HandleInputChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Oscilloscope is High Bandwidth or not
        /// </summary>
        public bool IsHighBandwidth
        {
            get
            {
                return this.isHighBandwidth;
            }

            set
            {
                this.SetProperty(ref this.isHighBandwidth, value);
                this.isLowBandwidth = !this.isHighBandwidth;
                this.RaisePropertyChanged(nameof(this.IsLowBandwidth));
                this.HandleInputChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Oscilloscope is Low Bandwidth or not
        /// </summary>
        public bool IsLowBandwidth
        {
            get
            {
                return this.isLowBandwidth;
            }

            set
            {
                this.SetProperty(ref this.isLowBandwidth, value);
                this.isHighBandwidth = !this.isLowBandwidth;
                this.RaisePropertyChanged(nameof(this.IsHighBandwidth));
                this.HandleInputChanged();
            }
        }

        /// <summary>
        /// Calculate the Standard results if possible.  Returns null if not possible.
        /// </summary>
        /// <returns>The Standard results if possible.  Returns null if not possible.</returns>
        protected override IStandard CalculateStandard()
        {
            return this.OpenedWaveform != null && this.VoltageIsValid() ?
                new CDMJS002Standard(this.OpenedWaveform, this.Voltage, this.IsLargeTarget, this.IsHighBandwidth) :
                null;
        }
    }
}
