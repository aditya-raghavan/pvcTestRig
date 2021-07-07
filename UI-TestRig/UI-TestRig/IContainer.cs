using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace UI_TestRig
{
    public interface IContainer
    {
        void ChangeFrame(IPage page);
    }
}
