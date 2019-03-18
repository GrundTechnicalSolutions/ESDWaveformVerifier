// ---------------------------------------------------------------------------------------------------
//  <copyright file="Waveform.cs" company="Grund Technical Solutions, Inc">
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
    /// A class that holds waveform data.
    /// </summary>
    public class Waveform
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Waveform"/> class.
        /// </summary>
        /// <param name="dataPoints">The data points that make up this waveform</param>
        public Waveform(IEnumerable<DataPoint> dataPoints)
        {
            if (dataPoints == null)
            {
                throw new ArgumentNullException("DataPoints cannot be null");
            }

            // Make a local copy of the collection
            this.DataPoints = dataPoints.ToList();
        }

        /// <summary>
        /// Gets the Data Points that make up this Waveform
        /// </summary>
        public IEnumerable<DataPoint> DataPoints
        {
            get;
            private set;
        }
    }
}
