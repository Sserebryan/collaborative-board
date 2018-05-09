using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollaborationBoard
{
    interface ICommand
    {
        void Do();
        IEnumerable UnDo();
    }

    public enum DrawState
    {
        move,
        down,
        outside,
        up        
    }
}
