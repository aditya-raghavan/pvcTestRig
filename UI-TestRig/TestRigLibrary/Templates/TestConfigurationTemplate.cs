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

        public double positiveTolerenceVoltageMax;
        public double negativeTolerenceVoltageMax;
        public double nominalForwardDropVoltsMax;
        public double positiveTolerenceCurrentMax;
        public double negativeTolerenceCurrentMax;
        public double nominalReverseCurrentMax;
        public double forwardTestCurrentMax;
        public double reverseTestVoltageMax;
        public double forwardMaxVoltageMax;
        public double positiveTolerenceResistanceMax;
        public double negativeTolerenceResistanceMax;
        public double contactResistanceMax;

        public double positiveTolerenceVoltageMin;
        public double negativeTolerenceVoltageMin;
        public double nominalForwardDropVoltsMin;
        public double positiveTolerenceCurrentMin;
        public double negativeTolerenceCurrentMin;
        public double nominalReverseCurrentMin;
        public double forwardTestCurrentMin;
        public double reverseTestVoltageMin;
        public double forwardMaxVoltageMin;
        public double positiveTolerenceResistanceMin;
        public double negativeTolerenceResistanceMin;
        public double contactResistanceMin;

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

