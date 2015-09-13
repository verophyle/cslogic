using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Verophyle.CSLogic
{
    public interface IUnifiable<T>
        where T : IUnifiable<T>
    {
        IEnumerable<State<T>> Unify(T other, State<T> s);
    }
}
