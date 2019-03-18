// ---------------------------------------------------------------------------------------------------
//  <copyright file="DataPoint.cs" company="Grund Technical Solutions, Inc">
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
    /// A struct that represents a data point in two dimensional space.
    /// </summary>
    public struct DataPoint
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataPoint"/> struct.
        /// </summary>
        /// <param name="abscissa">The horizontal component of the data point.</param>
        /// <param name="ordinate">The vertical component of the data point.</param>
        public DataPoint(double abscissa, double ordinate)
            : this()
        {
            this.X = abscissa;
            this.Y = ordinate;
        }

        /// <summary>
        /// Gets or sets the abscissa component of the data point.
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Gets or sets the ordinate component of the data point.
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// Returns a string representation of the instance.
        /// </summary>
        /// <returns>A string representation of the instance.</returns>
        public override string ToString()
        {
            return @"[" + this.X + @"," + this.Y + @"]";
        }
    }
}
