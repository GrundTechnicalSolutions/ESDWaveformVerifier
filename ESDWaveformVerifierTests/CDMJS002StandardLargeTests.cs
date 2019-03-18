using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESDWaveformVerifier.CDMJS002;
using ESDWaveformVerifier.DataTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ESDWaveformVerifierTests
{
    [TestClass]
    public class CDMJS002StandardLargeTests
    {
        [TestMethod]
        public void CDMJS002LargePositiveHighBandwidthTest()
        {
            Waveform waveform = PublicStandardTestExtensions.ParseWaveformFromString(Properties.Resources.CDM_Large_Pos_0250V);
            CDMJS002Standard cdmJS002Standard = new CDMJS002Standard(waveform, 250, true, true);

            // Assert that Peak Current value is within 1mA of expected
            Assert.AreEqual(5.838, cdmJS002Standard.PeakCurrentValue, 0.001);

            // Assert that Peak Current min/max are within 1mA of expected
            Assert.AreEqual(4.8, cdmJS002Standard.PeakCurrentAllowedMinimum, 0.001);
            Assert.AreEqual(7.3, cdmJS002Standard.PeakCurrentAllowedMaximum, 0.001);

            // Assert that Peak Current is passing
            Assert.IsTrue(cdmJS002Standard.PeakCurrentIsPassing);

            // Assert that Rise Time is within 10ps of expected
            Assert.AreEqual(0.0000000003102, cdmJS002Standard.RiseTimeValue, 0.000000000010);

            // Assert that Rise Time max are within 10ps of expected
            Assert.AreEqual(0.000000000350, cdmJS002Standard.RiseTimeAllowedMaximum, 0.000000000010);

            // Assert that Rise Time is passing
            Assert.IsTrue(cdmJS002Standard.RiseTimeIsPassing);

            // Assert that Full Width at Half Max is within 10ps of expected
            Assert.AreEqual(0.0000000006688, cdmJS002Standard.FullWidthHalfMaxValue, 0.000000000010);

            // Assert that Full Width at Half Max min/max are within 10ps of expected
            Assert.AreEqual(0.000000000450, cdmJS002Standard.FullWidthHalfMaxAllowedMinimum, 0.000000000010);
            Assert.AreEqual(0.000000000900, cdmJS002Standard.FullWidthHalfMaxAllowedMaximum, 0.000000000010);

            // Assert that Full Width at Half Max is passing
            Assert.IsTrue(cdmJS002Standard.FullWidthHalfMaxIsPassing);

            // Assert that Undershoot is within 1mA of expected
            Assert.AreEqual(-0.982, cdmJS002Standard.UndershootValue, 0.001);

            // Assert that Undershoot percent is as expected
            Assert.AreEqual(-0.5, cdmJS002Standard.UndershootAllowedMaximumPercent);

            // Assert that Undershoot max is within 1mA of expected
            Assert.AreEqual(-2.919, cdmJS002Standard.UndershootAllowedMaximumValue, 0.001);

            // Assert that Undershoot is passing
            Assert.IsTrue(cdmJS002Standard.UndershootIsPassing);
        }

        [TestMethod]
        public void CDMJS002LargeNegativeHighBandwidthTest()
        {
            Waveform waveform = PublicStandardTestExtensions.ParseWaveformFromString(Properties.Resources.CDM_Large_Neg_1000V);
            CDMJS002Standard cdmJS002Standard = new CDMJS002Standard(waveform, -1000, true, true);

            // Assert that Peak Current value is within 1mA of expected
            Assert.AreEqual(-25.015, cdmJS002Standard.PeakCurrentValue, 0.001);

            // Assert that Peak Current min/max are within 1mA of expected
            Assert.AreEqual(-20.6, cdmJS002Standard.PeakCurrentAllowedMinimum, 0.001);
            Assert.AreEqual(-27.9, cdmJS002Standard.PeakCurrentAllowedMaximum, 0.001);

            // Assert that Peak Current is passing
            Assert.IsTrue(cdmJS002Standard.PeakCurrentIsPassing);

            // Assert that Rise Time is within 10ps of expected
            Assert.AreEqual(0.0000000003116, cdmJS002Standard.RiseTimeValue, 0.000000000010);

            // Assert that Rise Time max are within 10ps of expected
            Assert.AreEqual(0.000000000350, cdmJS002Standard.RiseTimeAllowedMaximum, 0.000000000010);

            // Assert that Rise Time is passing
            Assert.IsTrue(cdmJS002Standard.RiseTimeIsPassing);

            // Assert that Full Width at Half Max is within 10ps of expected
            Assert.AreEqual(0.0000000006756, cdmJS002Standard.FullWidthHalfMaxValue, 0.000000000010);

            // Assert that Full Width at Half Max min/max are within 10ps of expected
            Assert.AreEqual(0.000000000450, cdmJS002Standard.FullWidthHalfMaxAllowedMinimum, 0.000000000010);
            Assert.AreEqual(0.000000000900, cdmJS002Standard.FullWidthHalfMaxAllowedMaximum, 0.000000000010);

            // Assert that Full Width at Half Max is passing
            Assert.IsTrue(cdmJS002Standard.FullWidthHalfMaxIsPassing);

            // Assert that Undershoot is within 1mA of expected
            Assert.AreEqual(6.397, cdmJS002Standard.UndershootValue, 0.001);

            // Assert that Undershoot percent is as expected
            Assert.AreEqual(-0.5, cdmJS002Standard.UndershootAllowedMaximumPercent);

            // Assert that Undershoot max is within 1mA of expected
            Assert.AreEqual(12.507, cdmJS002Standard.UndershootAllowedMaximumValue, 0.001);

            // Assert that Undershoot is passing
            Assert.IsTrue(cdmJS002Standard.UndershootIsPassing);
        }
    }
}
