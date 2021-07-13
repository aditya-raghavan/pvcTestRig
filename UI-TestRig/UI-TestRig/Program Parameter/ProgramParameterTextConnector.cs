using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI_TestRig
{
    public class ProgramParameterTextConnector : IProgramParameterDataConnection
    {
        public TestConfigurationTemplate LoadTestConfigurationFromFile(string fileName)
        {
            TestConfigurationTemplate template = new TestConfigurationTemplate();
            template = fileName.FullFilePath().LoadFile().ConvertToTestConfigurationTemplate();
            return template;
        }
        public void SaveTestConfigurationToFile(TestConfigurationTemplate template)
        {
            //TextFile saving format.
            //testconfigname,diodecode,customercode,additionalcode,diodeindex,barcodeindex,postolvoltage,negtolvol,NFDV,postolcur,negtolcur,NRC,fortestcurrent,revtestvoltage,formaxvoltage,
            //pottolres,negtolres,contactres

            List<string> lines = new List<string>();

            string fileName = template.testConfigurationName + ".csv";

            lines.Add($"TEST CONFIGURATION NAME,{template.testConfigurationName}");
            lines.Add($"DIODE CODE,{template.diodeCode}");
            lines.Add($"CUSTOMER CODE,{template.customerCode}");
            lines.Add($"ADDITIONAL CODE,{template.additionalCode}");
            lines.Add($"DIODE TYPE,{template.diodeType}");
            lines.Add($"BAR CODE OPTION,{template.barCodeOption}");

            lines.Add($"POSITIVE TOLERANCE DROP VOLTAGE,{template.positiveTolerenceVoltage.ToString("N3")},mV");
            lines.Add($"NEGATIVE TOLERANCE DROP VOLTAGE,{template.negativeTolerenceVoltage.ToString("N3")},mV");
            lines.Add($"NOMINAL FORWARD DROP VOLTAGE,{template.nominalForwardDropVolts.ToString("N3")},mV");
            lines.Add($"POSITIVE TOLERANCE REVERSE CURRENT,{template.positiveTolerenceCurrent.ToString("N3")},uA");
            lines.Add($"NEGATIVE TOLERANCE REVERSE CURRENT,{template.negativeTolerenceCurrent.ToString("N3")},uA");
            lines.Add($"NOMINAL REVERSE CURRENT,{template.nominalReverseCurrent.ToString("N3")},uA");
            lines.Add($"FORWARD TEST CURRENT,{template.forwardTestCurrent.ToString("N3")},A");
            lines.Add($"REVERSE TEST VOLTAGE,{template.reverseTestVoltage.ToString("N3")},V");
            lines.Add($"FORWARD MAX VOLTAGE,{template.forwardMaxVoltage.ToString("N3")},V");
            lines.Add($"POSITIVE TOLERANCE CONTACT RESISTANCE,{template.positiveTolerenceResistance.ToString("N3")},Ohms");
            lines.Add($"NEGATIVE TOLERANCE CONTACT RESISTANCE,{template.negativeTolerenceResistance.ToString("N3")},Ohms");
            lines.Add($"CONTACT RESISTANCE,{template.contactResistance.ToString("N3")},Ohms");


            File.WriteAllLines(fileName.FullFilePath(), lines);
        }

        public void DeleteTestConfiguration(string testConfigurationName)
        {
            string fileName = testConfigurationName + ".csv";
            File.Delete(fileName.FullFilePath());
        }
    }
}
