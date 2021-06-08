using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace TestRigLibrary.Templates
{
    /// <summary>
    /// Represents a model
    /// </summary>
    public class TestConfigurationTemplate 
    {
        public string modelName;
        public string diodeCode;
        public string additionalCode;
        public string customerCode;
        public string diodeType;
        public string barCodeOption;

        public double positiveTolerenceVoltage;
        public double negativeTolerenceVoltage;
        public double nominalForwardDropVolts;
        public double positiveTolerenceCurrent;
        public double negativeTolerenceCurrent;
        public double nominalReverseCurrent;
        public double forwardTestCurrent;
        public double reverseTestVoltage;
        public double forwardMaxVoltage;
        public double positiveTolerenceResistance;
        public double negativeTolerenceResistance;
        public double contactResistance;

        public double positiveTolerenceVoltageHigh;
        public double negativeTolerenceVoltageHigh;
        public double nominalForwardDropVoltsHigh;
        public double positiveTolerenceCurrentHigh;
        public double negativeTolerenceCurrentHigh;
        public double nominalReverseCurrentHigh;
        public double forwardTestCurrentHigh;
        public double reverseTestVoltageHigh;
        public double forwardMaxVoltageHigh;
        public double positiveTolerenceResistanceHigh;
        public double negativeTolerenceResistanceHigh;
        public double contactResistanceHigh;

        public double positiveTolerenceVoltageLow;
        public double negativeTolerenceVoltageLow;
        public double nominalForwardDropVoltsLow;
        public double positiveTolerenceCurrentLow;
        public double negativeTolerenceCurrentLow;
        public double nominalReverseCurrentLow;
        public double forwardTestCurrentLow;
        public double reverseTestVoltageLow;
        public double forwardMaxVoltageLow;
        public double positiveTolerenceResistanceLow;
        public double negativeTolerenceResistanceLow;
        public double contactResistanceLow;

        public static string GetDiodeTypeFromIndex(int index)
        {
            return DiodeTypes.ElementAt(index);
        }

        public static int GetIndexFromDiodeType(string diodeType)
        {
            return DiodeTypes.IndexOf(diodeType);
        }

        public static string GetBarcodeOptionFromIndex(int index)
        {
            return BarCodeOptions.ElementAt(index);
        }

        public static int GetIndexFromBarcodeOption(string barcodeOption)
        {
            return BarCodeOptions.IndexOf(barcodeOption);
        }

        public static List<string> DiodeTypes = new List<string>() { "2 DIODES", "3 DIODES" };
        public static List<string> BarCodeOptions { get; set; } = new List<string> { "ENABLED", "DISABLED" };








    }



}

