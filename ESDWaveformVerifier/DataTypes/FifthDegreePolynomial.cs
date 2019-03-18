// ---------------------------------------------------------------------------------------------------
//  <copyright file="FifthDegreePolynomial.cs" company="Grund Technical Solutions, Inc">
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
    /// Defines a fifth degree polynomial (Quintic equation)
    /// </summary>
    public class FifthDegreePolynomial
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FifthDegreePolynomial"/> class.
        /// </summary>
        /// <param name="a0">The zero degree polynomial coefficient</param>
        /// <param name="a1">The first degree polynomial coefficient</param>
        /// <param name="a2">The second degree polynomial coefficient</param>
        /// <param name="a3">The third degree polynomial coefficient</param>
        /// <param name="a4">The fourth degree polynomial coefficient</param>
        /// <param name="a5">The fifth degree polynomial coefficient</param>
        public FifthDegreePolynomial(double a0, double a1, double a2, double a3, double a4, double a5)
        {
            if (double.IsInfinity(a0))
            {
                throw new ArgumentException("A0 cannot be infinity");
            }

            if (double.IsInfinity(a1))
            {
                throw new ArgumentException("A1 cannot be infinity");
            }

            if (double.IsInfinity(a2))
            {
                throw new ArgumentException("A2 cannot be infinity");
            }

            if (double.IsInfinity(a3))
            {
                throw new ArgumentException("A3 cannot be infinity");
            }

            if (double.IsInfinity(a4))
            {
                throw new ArgumentException("A4 cannot be infinity");
            }

            if (double.IsInfinity(a5))
            {
                throw new ArgumentException("A5 cannot be infinity");
            }

            if (double.IsNaN(a0))
            {
                throw new ArgumentException("A0 cannot be NaN");
            }

            if (double.IsNaN(a1))
            {
                throw new ArgumentException("A1 cannot be NaN");
            }

            if (double.IsNaN(a2))
            {
                throw new ArgumentException("A2 cannot be NaN");
            }

            if (double.IsNaN(a3))
            {
                throw new ArgumentException("A3 cannot be NaN");
            }

            if (double.IsNaN(a4))
            {
                throw new ArgumentException("A4 cannot be NaN");
            }

            if (double.IsNaN(a5))
            {
                throw new ArgumentException("A5 cannot be NaN");
            }

            this.A0 = a0;
            this.A1 = a1;
            this.A2 = a2;
            this.A3 = a3;
            this.A4 = a4;
            this.A5 = a5;
        }

        /// <summary>
        /// Gets the zero degree polynomial coefficient
        /// </summary>
        public double A0
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the first degree polynomial coefficient
        /// </summary>
        public double A1
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the second degree polynomial coefficient
        /// </summary>
        public double A2
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the third degree polynomial coefficient
        /// </summary>
        public double A3
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the fourth degree polynomial coefficient
        /// </summary>
        public double A4
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the fifth degree polynomial coefficient
        /// </summary>
        public double A5
        {
            get;
            private set;
        }

        /// <summary>
        /// Returns the result when the value is evaluated in a fifth degree polynomial with the predefined coefficients.
        /// </summary>
        /// <param name="value">The value to evaluate</param>
        /// <returns>the result when the value is evaluated in a fifth degree polynomial with the predefined coefficients</returns>
        public double Evaluate(double value)
        {
            return
                 this.A0 +
                (this.A1 * value) +
                (this.A2 * value * value) +
                (this.A3 * value * value * value) +
                (this.A4 * value * value * value * value) +
                (this.A5 * value * value * value * value * value);
        }
    }
}