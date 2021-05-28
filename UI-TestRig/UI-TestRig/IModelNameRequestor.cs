using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI_TestRig
{
    public interface IModelNameRequestor
    {
        /// <summary>
        /// A model Name requesting form must have a modelName complete method as callback method.
        /// </summary>
        /// <param name="modelName"></param>
        void ModelNameComplete(string modelName);
    }
}
