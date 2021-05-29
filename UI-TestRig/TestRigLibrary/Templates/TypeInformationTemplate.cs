using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestRigLibrary.Templates
{
    public class TypeInformationTemplate
    {
        public string DiodeCode { get; set; }

        public string CustomerCode { get; set; }
        public string AdditionalCode { get; set; }


        

        public string DiodeType { get; set; }

        public string BarCodeOption { get; set; }

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

        public static List<string> GetDiodeTypes()
        {
            return DiodeTypes;
        }

        public static List<string> GetBarcodeOptions()
        {
            return BarCodeOptions;
        }

    }
}
