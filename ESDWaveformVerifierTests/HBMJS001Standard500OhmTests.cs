using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESDWaveformVerifier.DataTypes;
using ESDWaveformVerifier.HBM500OhmJS001;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ESDWaveformVerifierTests
{
    [TestClass]
    public class HBMJS001Standard500OhmTests
    {
        [TestMethod]
        public void HBMJS001500OhmPositiveTest()
        {
            Waveform waveform = PublicStandardTestExtensions.ParseWaveformFromString(Properties.Resources.HBM_500Ohm_Pos_1000V);
            HBM500OhmJS001Standard hbm500OhmJS001Standard = new HBM500OhmJS001Standard(waveform, 1000);

            // Assert that Peak Current value is within 1mA of expected
            Assert.AreEqual(0.4121, hbm500OhmJS001Standard.PeakCurrentValue, 0.001);

            // Assert that Peak Current min/max are within 1mA of expected
            Assert.AreEqual(0.370, hbm500OhmJS001Standard.PeakCurrentAllowedMinimum, 0.001);
            Assert.AreEqual(0.550, hbm500OhmJS001Standard.PeakCurrentAllowedMaximum, 0.001);

            // Assert that Peak Current is passing
            Assert.IsTrue(hbm500OhmJS001Standard.PeakCurrentIsPassing);

            // Assert that Rise Time is within 10ps of expected
            Assert.AreEqual(0.00000002048, hbm500OhmJS001Standard.RiseTimeValue, 0.000000000010);

            // Assert that Rise Time min/max are within 10ps of expected
            Assert.AreEqual(0.000000005, hbm500OhmJS001Standard.RiseTimeAllowedMinimum, 0.000000000010);
            Assert.AreEqual(0.000000025, hbm500OhmJS001Standard.RiseTimeAllowedMaximum, 0.000000000010);

            // Assert that Rise Time is passing
            Assert.IsTrue(hbm500OhmJS001Standard.RiseTimeIsPassing);
        }

        [TestMethod]
        public void HBMJS001500OhmNegativeTest()
        {
            Waveform waveform = PublicStandardTestExtensions.ParseWaveformFromString(Properties.Resources.HBM_500Ohm_Neg_4000V);
            HBM500OhmJS001Standard hbm500OhmJS001Standard = new HBM500OhmJS001Standard(waveform, -4000);

            // Assert that Peak Current value is within 1mA of expected
            Assert.AreEqual(-1.709, hbm500OhmJS001Standard.PeakCurrentValue, 0.001);

            // Assert that Peak Current min/max are within 1mA of expected
            Assert.AreEqual(-1.5, hbm500OhmJS001Standard.PeakCurrentAllowedMinimum, 0.001);
            Assert.AreEqual(-2.2, hbm500OhmJS001Standard.PeakCurrentAllowedMaximum, 0.001);

            // Assert that Peak Current is passing
            Assert.IsTrue(hbm500OhmJS001Standard.PeakCurrentIsPassing);

            // Assert that Rise Time is within 10ps of expected
            Assert.AreEqual(0.00000002292, hbm500OhmJS001Standard.RiseTimeValue, 0.000000000010);

            // Assert that Rise Time min/max are within 10ps of expected
            Assert.AreEqual(0.000000005, hbm500OhmJS001Standard.RiseTimeAllowedMinimum, 0.000000000010);
            Assert.AreEqual(0.000000025, hbm500OhmJS001Standard.RiseTimeAllowedMaximum, 0.000000000010);

            // Assert that Rise Time is passing
            Assert.IsTrue(hbm500OhmJS001Standard.RiseTimeIsPassing);
        }
    }
}
