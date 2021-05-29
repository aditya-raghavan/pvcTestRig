using System;
using System.Collections.Generic;
using System.Text;

namespace TestRigLibrary.Templates
{
    /// <summary>
    /// Represents a model
    /// </summary>
    public class TestConfigurationTemplate
    {

        public string Name { get; set; }

        /// <summary>
        /// Type Informations of a model
        /// </summary>
        public TypeInformationTemplate TypeInformation { get; set; }


        /// <summary>
        /// Reading Informations of a model
        /// </summary>
        public ReadingsTemplate ModelReadings { get; set; }
    }
}
