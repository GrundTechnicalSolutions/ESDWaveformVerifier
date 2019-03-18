// ---------------------------------------------------------------------------------------------------
//  <copyright file="MainVM.cs" company="Grund Technical Solutions, Inc">
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
    using Prism.Mvvm;

    /// <summary>
    /// Class that represents the main View Model
    /// </summary>
    internal class MainVM : BindableBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainVM"/> class
        /// </summary>
        public MainVM()
        {
            this.CDMVM = new CDMVM();
            this.HBMVM = new HBMVM();
        }

        /// <summary>
        /// Gets the CDM View Model
        /// </summary>
        public CDMVM CDMVM
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the HBM View Model
        /// </summary>
        public HBMVM HBMVM
        {
            get;
            private set;
        }
    }
}
