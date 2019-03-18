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
    public class CDMJS002StandardSmallTests
    {
        [TestMethod]
        public void CDMJS002SmallPositiveHighBandwidthTest()
        {
            Waveform waveform = PublicStandardTestExtensions.ParseWaveformFromString(Properties.Resources.CDM_Small_Pos_0250V);
            CDMJS002Standard cdmJS002Standard = new CDMJS002Standard(waveform, 250, false, true);

            // Assert that Peak Current value is within 1mA of expected
            Assert.AreEqual(3.775, cdmJS002Standard.PeakCurrentValue, 0.001);

            // Assert that Peak Current min/max are within 1mA of expected
            Assert.AreEqual(2.9, cdmJS002Standard.PeakCurrentAllowedMinimum, 0.001);
            Assert.AreEqual(4.3, cdmJS002Standard.PeakCurrentAllowedMaximum, 0.001);

            // Assert that Peak Current is passing
            Assert.IsTrue(cdmJS002Standard.PeakCurrentIsPassing);

            // Assert that Rise Time is within 10ps of expected
            Assert.AreEqual(0.0000000002127, cdmJS002Standard.RiseTimeValue, 0.000000000010);

            // Assert that Rise Time max are within 10ps of expected
            Assert.AreEqual(0.000000000250, cdmJS002Standard.RiseTimeAllowedMaximum, 0.000000000010);

            // Assert that Rise Time is passing
            Assert.IsTrue(cdmJS002Standard.RiseTimeIsPassing);

            // Assert that Full Width at Half Max is within 10ps of expected
            Assert.AreEqual(0.0000000003922, cdmJS002Standard.FullWidthHalfMaxValue, 0.000000000010);

            // Assert that Full Width at Half Max min/max are within 10ps of expected
            Assert.AreEqual(0.000000000250, cdmJS002Standard.FullWidthHalfMaxAllowedMinimum, 0.000000000010);
            Assert.AreEqual(0.000000000600, cdmJS002Standard.FullWidthHalfMaxAllowedMaximum, 0.000000000010);

            // Assert that Full Width at Half Max is passing
            Assert.IsTrue(cdmJS002Standard.FullWidthHalfMaxIsPassing);

            // Assert that Undershoot is within 1mA of expected
            Assert.AreEqual(-1.865, cdmJS002Standard.UndershootValue, 0.001);

            // Assert that Undershoot percent is as expected
            Assert.AreEqual(-0.7, cdmJS002Standard.UndershootAllowedMaximumPercent);

            // Assert that Undershoot max is within 1mA of expected
            Assert.AreEqual(-2.643, cdmJS002Standard.UndershootAllowedMaximumValue, 0.001);

            // Assert that Undershoot is passing
            Assert.IsTrue(cdmJS002Standard.UndershootIsPassing);
        }

        [TestMethod]
        public void CDMJS002SmallNegativeHighBandwidthTest()
        {
            Waveform waveform = PublicStandardTestExtensions.ParseWaveformFromString(Properties.Resources.CDM_Small_Neg_1000V);
            CDMJS002Standard cdmJS002Standard = new CDMJS002Standard(waveform, -1000, false, true);

            // Assert that Peak Current value is within 1mA of expected
            Assert.AreEqual(-14.733, cdmJS002Standard.PeakCurrentValue, 0.001);

            // Assert that Peak Current min/max are within 1mA of expected
            Assert.AreEqual(-12.2, cdmJS002Standard.PeakCurrentAllowedMinimum, 0.001);
            Assert.AreEqual(-16.5, cdmJS002Standard.PeakCurrentAllowedMaximum, 0.001);

            // Assert that Peak Current is passing
            Assert.IsTrue(cdmJS002Standard.PeakCurrentIsPassing);

            // Assert that Rise Time is within 10ps of expected
            Assert.AreEqual(0.0000000002118, cdmJS002Standard.RiseTimeValue, 0.000000000010);

            // Assert that Rise Time max are within 10ps of expected
            Assert.AreEqual(0.000000000250, cdmJS002Standard.RiseTimeAllowedMaximum, 0.000000000010);

            // Assert that Rise Time is passing
            Assert.IsTrue(cdmJS002Standard.RiseTimeIsPassing);

            // Assert that Full Width at Half Max is within 10ps of expected
            Assert.AreEqual(0.0000000004048, cdmJS002Standard.FullWidthHalfMaxValue, 0.000000000010);

            // Assert that Full Width at Half Max min/max are within 10ps of expected
            Assert.AreEqual(0.000000000250, cdmJS002Standard.FullWidthHalfMaxAllowedMinimum, 0.000000000010);
            Assert.AreEqual(0.000000000600, cdmJS002Standard.FullWidthHalfMaxAllowedMaximum, 0.000000000010);

            // Assert that Full Width at Half Max is passing
            Assert.IsTrue(cdmJS002Standard.FullWidthHalfMaxIsPassing);

            // Assert that Undershoot is within 1mA of expected
            Assert.AreEqual(7.38, cdmJS002Standard.UndershootValue, 0.001);

            // Assert that Undershoot percent is as expected
            Assert.AreEqual(-0.7, cdmJS002Standard.UndershootAllowedMaximumPercent);

            // Assert that Undershoot max is within 1mA of expected
            Assert.AreEqual(10.313, cdmJS002Standard.UndershootAllowedMaximumValue, 0.001);

            // Assert that Undershoot is passing
            Assert.IsTrue(cdmJS002Standard.UndershootIsPassing);
        }

        [TestMethod]
        public void CDMJS002LargePositiveUndershootTest()
        {
            Waveform waveform = PublicStandardTestExtensions.ParseWaveformFromString(Properties.Resources.CDM_Small_Pos_1000V_PosUndershoot);
            CDMJS002Standard cdmJS002Standard = new CDMJS002Standard(waveform, 1000, false, false);

            // Assert that Peak Current value is within 1mA of expected
            Assert.AreEqual(10.99, cdmJS002Standard.PeakCurrentValue, 0.01);

            // Assert that Peak Current min/max are within 1mA of expected
            Assert.AreEqual(8.8, cdmJS002Standard.PeakCurrentAllowedMinimum, 0.001);
            Assert.AreEqual(11.9, cdmJS002Standard.PeakCurrentAllowedMaximum, 0.001);

            // Assert that Peak Current is passing
            Assert.IsTrue(cdmJS002Standard.PeakCurrentIsPassing);

            // Assert that Rise Time is within 10ps of expected
            Assert.AreEqual(0.0000000002819, cdmJS002Standard.RiseTimeValue, 0.000000000010);

            // Assert that Rise Time max are within 10ps of expected
            Assert.AreEqual(0.000000000350, cdmJS002Standard.RiseTimeAllowedMaximum, 0.000000000010);

            // Assert that Rise Time is passing
            Assert.IsTrue(cdmJS002Standard.RiseTimeIsPassing);

            // Assert that Full Width at Half Max is within 10ps of expected
            Assert.AreEqual(0.0000000004196, cdmJS002Standard.FullWidthHalfMaxValue, 0.000000000010);

            // Assert that Full Width at Half Max min/max are within 10ps of expected
            Assert.AreEqual(0.000000000325, cdmJS002Standard.FullWidthHalfMaxAllowedMinimum, 0.000000000010);
            Assert.AreEqual(0.000000000725, cdmJS002Standard.FullWidthHalfMaxAllowedMaximum, 0.000000000010);

            // Assert that Full Width at Half Max is passing
            Assert.IsTrue(cdmJS002Standard.FullWidthHalfMaxIsPassing);

            // Assert that Undershoot is within 1mA of expected
            Assert.AreEqual(0, cdmJS002Standard.UndershootValue, 0.001);

            // Assert that Undershoot percent is as expected
            Assert.AreEqual(-0.7, cdmJS002Standard.UndershootAllowedMaximumPercent);

            // Assert that Undershoot max is within 1mA of expected
            Assert.AreEqual(-7.692, cdmJS002Standard.UndershootAllowedMaximumValue, 0.001);

            // Assert that Undershoot is passing
            Assert.IsTrue(cdmJS002Standard.UndershootIsPassing);
        }
    }
}
