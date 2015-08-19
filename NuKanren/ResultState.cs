using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuKanren
{
    public class ResultState<T>
    {
        internal int nextId;
        public ImmutableDictionary<Variable, T> Bindings { get; internal set; }

        static ResultState<T> empty = null;
        static object emptyLock = new object();

        public static ResultState<T> Empty
        {
            get
            {
                if (empty == null)
                {
                    lock (emptyLock)
                    {
                        if (empty == null)
                        {
                            empty = new ResultState<T>();
                            empty.nextId = 0;
                            empty.Bindings = ImmutableDictionary<Variable, T>.Empty;
                        }
                    }
                }
                return empty;
            }
        }
    }
}
