using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Verophyle.CSLogic
{
    public struct Var
    {
        static int nextId = 0;
        public int Id;

        public static Var NewVar()
        {
            return new Var { Id = nextId++ };
        }
    }
}
