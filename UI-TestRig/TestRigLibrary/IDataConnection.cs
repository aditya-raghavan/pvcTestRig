using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRigLibrary.Templates;

namespace TestRigLibrary
{
    /// <summary>
    /// Any class acting as a data connection class must have the following methods.
    /// </summary>
    public interface IDataConnection
    {
        void SaveModel(ModelTemplate model);

        ModelTemplate LoadModel(string modelName);

        List<string> GetAllModelNames();

        void DeleteModel(string modelName);
    }
}
