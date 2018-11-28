using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inVent.Contracts
{
    public interface ICommand
    {
        bool Execute();
        bool Revert();
    }
}
