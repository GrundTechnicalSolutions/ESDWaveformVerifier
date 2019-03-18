// ---------------------------------------------------------------------------------------------------
//  <copyright file="WaveformVM.cs" company="Grund Technical Solutions, Inc">
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
    using ESDWaveformVerifier.DataTypes;
    using Prism.Commands;
    using Prism.Mvvm;

    /// <summary>
    /// Abstract class for the Waveform View Models
    /// </summary>
    internal abstract class WaveformVM : BindableBase
    {
        /// <summary>
        /// Private backing store
        /// </summary>
        private DelegateCommand openCSVFileCommand;

        /// <summary>
        /// Private backing store
        /// </summary>
        private IStandard standard;

        /// <summary>
        /// Private backing store
        /// </summary>
        private double voltage = 1000;

        /// <summary>
        /// Private backing store
        /// </summary>
        private string openedWaveformOutcome;

        /// <summary>
        /// Private backing store
        /// </summary>
        private string openedWaveformFullPathName;

        /// <summary>
        /// Gets a description of the outcome of opening a CSV file
        /// </summary>
        public string OpenedWaveformOutcome
        {
            get { return this.openedWaveformOutcome; }
            private set { this.SetProperty(ref this.openedWaveformOutcome, value); }
        }

        /// <summary>
        /// Gets the full path name of the opened waveform CSV file
        /// </summary>
        public string OpenedWaveformFullPathName
        {
            get { return this.openedWaveformFullPathName; }
            private set { this.SetProperty(ref this.openedWaveformFullPathName, value); }
        }

        /// <summary>
        /// Gets the Standard Calculation results object
        /// </summary>
        public IStandard Standard
        {
            get { return this.standard; }
            private set { this.SetProperty(ref this.standard, value); }
        }

        /// <summary>
        /// Gets or sets the Voltage of the Waveform
        /// </summary>
        public double Voltage
        {
            get
            {
                return this.voltage;
            }

            set
            {
                this.SetProperty(ref this.voltage, value);
                this.HandleInputChanged();
            }
        }

        /// <summary>
        /// Gets a command to open waveform data from a CSV file
        /// </summary>
        public DelegateCommand OpenCSVFileCommand
        {
            get
            {
                if (this.openCSVFileCommand == null)
                {
                    this.openCSVFileCommand = new DelegateCommand(() =>
                    {
                        string fullPathName = ImportExtensions.PromptUserForCSVFileWithOpenFileDialog();
                        if (!string.IsNullOrWhiteSpace(fullPathName))
                        {
                            this.OpenedWaveformFullPathName = fullPathName;
                            this.OpenedWaveform = ImportExtensions.ConvertCSVFileToWaveform(fullPathName);
                            this.OpenedWaveformOutcome = this.OpenedWaveform != null ?
                                "Opening the waveform was successful" :
                                "There was a problem opening the waveform";

                            this.HandleInputChanged();
                        }
                    });
                }

                return this.openCSVFileCommand;
            }
        }

        /// <summary>
        /// Gets the Waveform that the user specified to open
        /// </summary>
        protected Waveform OpenedWaveform
        {
            get;
            private set;
        }

        /// <summary>
        /// This should be called anytime an input has been changed, so the Standard can be re-calculated.
        /// </summary>
        protected void HandleInputChanged()
        {
            this.Standard = this.CalculateStandard();
        }

        /// <summary>
        /// Returns a value indicating whether the Voltage is a valid value or not
        /// </summary>
        /// <returns>A value indicating whether the Voltage is a valid value or not</returns>
        protected bool VoltageIsValid()
        {
            return
                !double.IsInfinity(this.Voltage) &&
                !double.IsNaN(this.Voltage) &&
                this.Voltage != 0 &&
                this.Voltage >= -100000 &&
                this.Voltage <= 100000;
        }

        /// <summary>
        /// Calculate the Standard results if possible.  Returns null if not possible.
        /// </summary>
        /// <returns>The Standard results if possible.  Returns null if not possible.</returns>
        protected abstract IStandard CalculateStandard();
    }
}
