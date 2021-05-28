using System;
using System.Collections.Generic;
using System.Text;

namespace TestRigLibrary.Templates
{
    public class TypeInformationTemplate
    {
        public string DiodeCode { get; set; }

        public string CustomerCode { get; set; }
        public string AdditionalCode { get; set; }


        /// <summary>
        /// Selected Diode Index (Read from a combobox).
        /// </summary>
        public int DiodeIndex { get; set; }

        /// <summary>
        /// selected barcode (Read from a combobox).
        /// </summary>
        public int BarCodeIndex { get; set; }


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
