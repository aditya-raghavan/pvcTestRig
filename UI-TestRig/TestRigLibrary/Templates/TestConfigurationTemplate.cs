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
    public class TestConfigurationTemplate : INotifyPropertyChanged, IDataErrorInfo
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged(); }
        }


        private string diodeCode;

        public string DiodeCode
        {
            get { return diodeCode; }
            set { diodeCode = value; OnPropertyChanged(); }
        }


        private string additionalCode;

        public string AdditionalCode
        {
            get { return additionalCode; }
            set { additionalCode = value; OnPropertyChanged(); }
        }


        private string customerCode;

        public string CustomerCode
        {
            get { return customerCode; }
            set { customerCode = value; OnPropertyChanged(); }
        }


        private string diodeType;

        public string DiodeType
        {
            get { return diodeType; }
            set { diodeType = value; OnPropertyChanged(); }
        }


        private string barCodeOption;

        public string BarCodeOption
        {
            get { return barCodeOption; }
            set { barCodeOption = value; OnPropertyChanged(); }
        }


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
        private double positiveTolerenceVoltage;
        private double negativeTolerenceVoltage;
        private double nominalForwardDropVolts;
        private double positiveTolerenceCurrent;
        private double negativeTolerenceCurrent;
        private double nominalReverseCurrent;
        private double forwardTestCurrent;
        private double reverseTestVoltage;
        private double forwardMaxVoltage;
        private double positiveTolerenceResistance;
        private double negativeTolerenceResistance;
        private double contactResistance;

        public static List<string> BarCodeOptions { get; set; } = new List<string> { "ENABLED", "DISABLED" };

        public static List<string> GetDiodeTypes()
        {
            return DiodeTypes;
        }

        public static List<string> GetBarcodeOptions()
        {
            return BarCodeOptions;
        }


        /// <summary>
        /// Reading Informations of a model
        /// </summary>

        public double PositiveTolerenceVoltage { get => positiveTolerenceVoltage; set { if (value != positiveTolerenceVoltage) { positiveTolerenceVoltage = value; OnPropertyChanged("PositiveTolerenceVoltage"); } } }
        public double NegativeTolerenceVoltage { get => negativeTolerenceVoltage; set { if (value != negativeTolerenceVoltage) { negativeTolerenceVoltage = value; OnPropertyChanged("NegativeTolerenceVoltage"); } } }
        public double NominalForwardDropVolts { get => nominalForwardDropVolts; set { if (value != nominalForwardDropVolts) { nominalForwardDropVolts = value; OnPropertyChanged("NominalForwardDropVolts"); } } }
        public double PositiveTolerenceCurrent { get => positiveTolerenceCurrent; set { if (value != positiveTolerenceCurrent) { positiveTolerenceCurrent = value; OnPropertyChanged("PositiveTolerenceCurrent"); } } }
        public double NegativeTolerenceCurrent { get => negativeTolerenceCurrent; set { if (value != negativeTolerenceCurrent) { negativeTolerenceCurrent = value; OnPropertyChanged("NegativeTolerenceCurrent"); } } }
        public double NominalReverseCurrent { get => nominalReverseCurrent; set { if (value != nominalReverseCurrent) { nominalReverseCurrent = value; OnPropertyChanged("NominalReverseCurrent"); } } }
        public double ForwardTestCurrent { get => forwardTestCurrent; set { if (value != forwardTestCurrent) { forwardTestCurrent = value; OnPropertyChanged("ForwardTestCurrent"); } } }
        public double ReverseTestVoltage { get => reverseTestVoltage; set { if (value != reverseTestVoltage) { reverseTestVoltage = value; OnPropertyChanged("ReverseTestVoltage"); } } }
        public double ForwardMaxVoltage { get => forwardMaxVoltage; set { if (value != forwardMaxVoltage) { forwardMaxVoltage = value; OnPropertyChanged("ForwardMaxVoltage"); } } }
        public double PositiveTolerenceResistance { get => positiveTolerenceResistance; set { if (value != positiveTolerenceResistance) { positiveTolerenceResistance = value; OnPropertyChanged("PositiveTolerenceResistance"); } } }
        public double NegativeTolerenceResistance { get => negativeTolerenceResistance; set { if (value != negativeTolerenceResistance) { negativeTolerenceResistance = value; OnPropertyChanged("NegativeTolerenceResistance"); } } }
        public double ContactResistance { get => contactResistance; set { if (value != contactResistance) { contactResistance = value; OnPropertyChanged("ContactResistance"); } } }

        public string Error
        {
            get
            {
                return null;
            }
        }

        public string this[string propertyName]
        {
            get
            {
                double x;
                string result = "";
                switch (propertyName)
                {
                    case "DiodeCode":
                        if (string.IsNullOrEmpty(DiodeCode))
                        {
                            result = "Diode Code cannot be empty";
                        }
                        break;
                    case "CustomerCode":
                        if (string.IsNullOrEmpty(CustomerCode))
                        {
                            result = "Customer Code cannot be empty";
                        }
                        break;
                    case "AdditionalCode":
                        if (string.IsNullOrEmpty(AdditionalCode))
                        {
                            result = "Additional Code cannot be empty";
                        }
                        break;
                    case "PositiveTolerenceVoltage":
                        if(!double.TryParse(positiveTolerenceVoltage.ToString(),out x))
                        {
                            result = "Value must be a number";
                        }
                        
                        else if(positiveTolerenceVoltage > GlobalConfig.maxValues[0])
                        {
                            result = "Value is higher than limit";
                        }
                        else if(positiveTolerenceVoltage < GlobalConfig.minValues[0])
                        {
                            result = "Value is lower than Minimum Value";
                        }
                        break;
                    case "NegativeTolerenceVoltage":
                        if (!double.TryParse(NegativeTolerenceVoltage.ToString(), out x))
                        {
                            result = "Value must be a number";
                        }

                        else if (NegativeTolerenceVoltage > GlobalConfig.maxValues[1])
                        {
                            result = "Value is higher than limit";
                        }
                        else if (NegativeTolerenceVoltage < GlobalConfig.minValues[1])
                        {
                            result = "Value is lower than Minimum Value";
                        }
                        break;
                    case "NominalForwardDropVolts":
                        if (!double.TryParse(NominalForwardDropVolts.ToString(), out x))
                        {
                            result = "Value must be a number";
                        }

                        else if (NominalForwardDropVolts > GlobalConfig.maxValues[2])
                        {
                            result = "Value is higher than limit";
                        }
                        else if (NominalForwardDropVolts < GlobalConfig.minValues[2])
                        {
                            result = "Value is lower than Minimum Value";
                        }
                        break;
                    case "PositiveTolerenceCurrent":
                        if (!double.TryParse(PositiveTolerenceCurrent.ToString(), out x))
                        {
                            result = "Value must be a number";
                        }

                        else if (PositiveTolerenceCurrent > GlobalConfig.maxValues[3])
                        {
                            result = "Value is higher than limit";
                        }
                        else if (PositiveTolerenceCurrent < GlobalConfig.minValues[3])
                        {
                            result = "Value is lower than Minimum Value";
                        }
                        break;
                    case "NegativeTolerenceCurrent":
                        if (!double.TryParse(NegativeTolerenceCurrent.ToString(), out x))
                        {
                            result = "Value must be a number";
                        }

                        else if (NegativeTolerenceCurrent > GlobalConfig.maxValues[4])
                        {
                            result = "Value is higher than limit";
                        }
                        else if (NegativeTolerenceCurrent < GlobalConfig.minValues[4])
                        {
                            result = "Value is lower than Minimum Value";
                        }
                        break;
                    case "NominalReverseCurrent":
                        if (!double.TryParse(NominalReverseCurrent.ToString(), out x))
                        {
                            result = "Value must be a number";
                        }

                        else if (NominalReverseCurrent > GlobalConfig.maxValues[5])
                        {
                            result = "Value is higher than limit";
                        }
                        else if (NominalReverseCurrent < GlobalConfig.minValues[5])
                        {
                            result = "Value is lower than Minimum Value";
                        }
                        break;
                    case "ForwardTestCurrent":
                        if (!double.TryParse(ForwardTestCurrent.ToString(), out x))
                        {
                            result = "Value must be a number";
                        }

                        else if (ForwardTestCurrent > GlobalConfig.maxValues[6])
                        {
                            result = "Value is higher than limit";
                        }
                        else if (ForwardTestCurrent < GlobalConfig.minValues[6])
                        {
                            result = "Value is lower than Minimum Value";
                        }
                        break;
                    case "ReverseTestVoltage":
                        if (!double.TryParse(ReverseTestVoltage.ToString(), out x))
                        {
                            result = "Value must be a number";
                        }

                        else if (ReverseTestVoltage > GlobalConfig.maxValues[7])
                        {
                            result = "Value is higher than limit";
                        }
                        else if (ReverseTestVoltage < GlobalConfig.minValues[7])
                        {
                            result = "Value is lower than Minimum Value";
                        }
                        break;
                    case "ForwardMaxVoltage":
                        if (!double.TryParse(ForwardMaxVoltage.ToString(), out x))
                        {
                            result = "Value must be a number";
                        }

                        else if (ForwardMaxVoltage > GlobalConfig.maxValues[8])
                        {
                            result = "Value is higher than limit";
                        }
                        else if (ForwardMaxVoltage < GlobalConfig.minValues[8])
                        {
                            result = "Value is lower than Minimum Value";
                        }
                        break;
                    case "PositiveTolerenceResistance":
                        if (!double.TryParse(PositiveTolerenceResistance.ToString(), out x))
                        {
                            result = "Value must be a number";
                        }

                        else if (PositiveTolerenceResistance > GlobalConfig.maxValues[9])
                        {
                            result = "Value is higher than limit";
                        }
                        else if (PositiveTolerenceResistance < GlobalConfig.minValues[9])
                        {
                            result = "Value is lower than Minimum Value";
                        }
                        break;
                    case "NegativeTolerenceResistance":
                        if (!double.TryParse(NegativeTolerenceResistance.ToString(), out x))
                        {
                            result = "Value must be a number";
                        }

                        else if (NegativeTolerenceResistance > GlobalConfig.maxValues[10])
                        {
                            result = "Value is higher than limit";
                        }
                        else if (NegativeTolerenceResistance < GlobalConfig.minValues[10])
                        {
                            result = "Value is lower than Minimum Value";
                        }
                        break;
                    case "ContactResistance":
                        if (!double.TryParse(ContactResistance.ToString(), out x))
                        {
                            result = "Value must be a number";
                        }

                        else if (ContactResistance > GlobalConfig.maxValues[11])
                        {
                            result = "Value is higher than limit";
                        }
                        else if (ContactResistance < GlobalConfig.minValues[11])
                        {
                            result = "Value is lower than Minimum Value";
                        }
                        break;


                }
                
                return result;
            }
        }


        public TestConfigurationTemplate()
        {
            Name = "";
            DiodeCode = "";
            CustomerCode = "";
            AdditionalCode = "";
            DiodeType = "3 DIODES";
            BarCodeOption = "DISABLED";
            PositiveTolerenceVoltage = 0;
            NegativeTolerenceVoltage = 0;
            NominalForwardDropVolts = 0;
            PositiveTolerenceCurrent = 0;
            NegativeTolerenceCurrent = 0;
            NominalReverseCurrent = 0;
            ForwardTestCurrent = 0;
            ReverseTestVoltage = 0;
            ForwardMaxVoltage = 0;
            PositiveTolerenceResistance = 0;
            NegativeTolerenceResistance = 0;
            ContactResistance = 0;

        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        

        
    }



}

