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

        public double maxPositiveTolerenceVoltage;
        public double maxNegativeTolerenceVoltage;
        public double maxNominalForwardDropVolts;
        public double maxPositiveTolerenceCurrent;
        public double maxNegativeTolerenceCurrent;
        public double maxNominalReverseCurrent;
        public double maxForwardTestCurrent;
        public double maxReverseTestVoltage;
        public double maxForwardMaxVoltage;
        public double maxPositiveTolerenceResistance;
        public double maxNegativeTolerenceResistance;
        public double maxContactResistance;

        public double minPositiveTolerenceVoltage;
        public double minNegativeTolerenceVoltage;
        public double minNominalForwardDropVolts;
        public double minPositiveTolerenceCurrent;
        public double minNegativeTolerenceCurrent;
        public double minNominalReverseCurrent;
        public double minForwardTestCurrent;
        public double minReverseTestVoltage;
        public double minForwardMaxVoltage;
        public double minPositiveTolerenceResistance;
        public double minNegativeTolerenceResistance;
        public double minContactResistance;

        public static List<string> DiodeTypes = new List<string>() { "2 DIODES", "3 DIODES" };
        public static List<string> BarCodeOptions { get; set; } = new List<string> { "ENABLED", "DISABLED" };

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

        

        

        

        
    }



}

