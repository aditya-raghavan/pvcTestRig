using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace UI_TestRig
{

    /// <summary>
    /// Utility class for text connector class.
    /// </summary>
    public static class TextConnectorProcessor
    {
        

        
        /// <summary>
        /// Loads content from a file path.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static List<string> LoadFile(this string file)
        {
            if (!File.Exists(file))
            {
                return null;
            }
            return File.ReadAllLines(file).ToList();

        }

        /// <summary>
        /// returns a populated TestConfigurationTemplate Object with values read from the specified test config file.
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        

        
    }
}
