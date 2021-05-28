using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRigLibrary.Templates
{
    public class ReadingsTemplate
    {
        public decimal PositiveTolerenceVoltage { get; set; }
        public decimal NegativeTolerenceVoltage { get; set; }
        public decimal NominalForwardDropVolts { get; set; }
        public decimal PositiveTolerenceCurrent { get; set; }
        public decimal NegativeTolerenceCurrent { get; set; }
        public decimal NominalReverseCurrent { get; set; }
        public decimal ForwardTestCurrent { get; set; }
        public decimal ReverseTestVoltage { get; set; }
        public decimal ForwardMaxVoltage { get; set; }
        public decimal PositiveTolerenceResistance { get; set; }
        public decimal NegativeTolerenceResistance { get; set; }
        public decimal ContactResistance { get; set; }

    }
}
