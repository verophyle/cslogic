using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Verophyle.CSLogic.Tests
{
    struct BasicVal : IUnifiable<BasicVal>
    {
        public int Value;

        public IEnumerable<State<BasicVal>> Unify(BasicVal other, State<BasicVal> s)
        {
            if (this.Value == other.Value)
                yield return s;
        }
    }
}
