using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESDWaveformVerifier.DataTypes;
using ESDWaveformVerifier.HBM0OhmJS001;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ESDWaveformVerifierTests
{
    [TestClass]
    public class HBMJS001Standard0OhmTests
    {
        [TestMethod]
        public void DecayMissingTest()
        {
            Waveform waveform = PublicStandardTestExtensions.ParseWaveformFromString(Properties.Resources.HBM_0Ohm_Pos_2500V_DecayMissing);
            HBM0OhmJS001Standard hbm0OhmJS001Standard = new HBM0OhmJS001Standard(waveform, 2500);

            // Assert that Peak Current value is within 1mA of expected
            double expectedPeakCurrent = 1.579;
            Assert.AreEqual(expectedPeakCurrent, hbm0OhmJS001Standard.PeakCurrentValue, 0.001);

            // Assert that Decay Time is within 100ps of expected
            Assert.AreEqual(0.0000001649, hbm0OhmJS001Standard.DecayTimeValue, 0.000000000100);
        }

        [TestMethod]
        public void PositiveNoNoiseCompensationTest()
        {
            Waveform waveform = PublicStandardTestExtensions.ParseWaveformFromString(Properties.Resources.HBM_0Ohm_Pos_0500V);
            HBM0OhmJS001Standard hbm0OhmJS001Standard = new HBM0OhmJS001Standard(waveform, 500);

            // Assert that Peak Current value is within 1mA of expected
            Assert.AreEqual(0.3488, hbm0OhmJS001Standard.PeakCurrentValue, 0.001);

            // Assert that Peak Current min/max are within 1mA of expected
            Assert.AreEqual(0.3, hbm0OhmJS001Standard.PeakCurrentAllowedMinimum, 0.001);
            Assert.AreEqual(0.37, hbm0OhmJS001Standard.PeakCurrentAllowedMaximum, 0.001);

            // Assert that Peak Current is passing
            Assert.IsTrue(hbm0OhmJS001Standard.PeakCurrentIsPassing);

            // Assert that Rise Time is within 10ps of expected
            Assert.AreEqual(0.000000007815, hbm0OhmJS001Standard.RiseTimeValue, 0.000000000010);

            // Assert that Rise Time min/max are within 10ps of expected
            Assert.AreEqual(0.000000002, hbm0OhmJS001Standard.RiseTimeAllowedMinimum, 0.000000000010);
            Assert.AreEqual(0.000000010, hbm0OhmJS001Standard.RiseTimeAllowedMaximum, 0.000000000010);

            // Assert that Rise Time is passing
            Assert.IsTrue(hbm0OhmJS001Standard.RiseTimeIsPassing);

            // Assert that Decay Time is within 10ps of expected
            Assert.AreEqual(0.00000016335, hbm0OhmJS001Standard.DecayTimeValue, 0.000000000010);

            // Assert that Decay Time min/max are within 10ps of expected
            Assert.AreEqual(0.000000130, hbm0OhmJS001Standard.DecayTimeAllowedMinimum, 0.000000000010);
            Assert.AreEqual(0.000000170, hbm0OhmJS001Standard.DecayTimeAllowedMaximum, 0.000000000010);

            // Assert that Decay Time is passing
            Assert.IsTrue(hbm0OhmJS001Standard.DecayTimeIsPassing);

            // Assert that Decay Time is within 0.01% of expected
            Assert.AreEqual(0.0820, hbm0OhmJS001Standard.TotalRingPercentValue, 0.0001);

            // Assert that Ring % Max is within 0.01% of expected
            Assert.AreEqual(0.15, hbm0OhmJS001Standard.TotalRingAllowedMaximum, 0.0001);
        }

        [TestMethod]
        public void NegativeNoNoiseCompensationTest()
        {
            Waveform waveform = PublicStandardTestExtensions.ParseWaveformFromString(Properties.Resources.HBM_0Ohm_Neg_8000V);
            HBM0OhmJS001Standard hbm0OhmJS001Standard = new HBM0OhmJS001Standard(waveform, -8000);

            // Assert that Peak Current value is within 1mA of expected
            Assert.AreEqual(-5.424, hbm0OhmJS001Standard.PeakCurrentValue, 0.001);

            // Assert that Peak Current min/max are within 1mA of expected
            Assert.AreEqual(-4.8, hbm0OhmJS001Standard.PeakCurrentAllowedMinimum, 0.001);
            Assert.AreEqual(-5.87, hbm0OhmJS001Standard.PeakCurrentAllowedMaximum, 0.001);

            // Assert that Peak Current is passing
            Assert.IsTrue(hbm0OhmJS001Standard.PeakCurrentIsPassing);

            // Assert that Rise Time is within 10ps of expected
            Assert.AreEqual(0.000000007845, hbm0OhmJS001Standard.RiseTimeValue, 0.000000000010);

            // Assert that Rise Time min/max are within 10ps of expected
            Assert.AreEqual(0.000000002, hbm0OhmJS001Standard.RiseTimeAllowedMinimum, 0.000000000010);
            Assert.AreEqual(0.000000010, hbm0OhmJS001Standard.RiseTimeAllowedMaximum, 0.000000000010);

            // Assert that Rise Time is passing
            Assert.IsTrue(hbm0OhmJS001Standard.RiseTimeIsPassing);

            // Assert that Decay Time is within 10ps of expected
            Assert.AreEqual(0.000000167932, hbm0OhmJS001Standard.DecayTimeValue, 0.000000000010);

            // Assert that Decay Time min/max are within 10ps of expected
            Assert.AreEqual(0.000000130, hbm0OhmJS001Standard.DecayTimeAllowedMinimum, 0.000000000010);
            Assert.AreEqual(0.000000170, hbm0OhmJS001Standard.DecayTimeAllowedMaximum, 0.000000000010);

            // Assert that Decay Time is passing
            Assert.IsTrue(hbm0OhmJS001Standard.DecayTimeIsPassing);

            // Assert that Ring % is within 0.01% of expected
            Assert.AreEqual(0.0161992, hbm0OhmJS001Standard.TotalRingPercentValue, 0.0001);

            // Assert that Ring % Max is within 0.01% of expected
            Assert.AreEqual(0.15, hbm0OhmJS001Standard.TotalRingAllowedMaximum, 0.0001);
        }
    }
}
