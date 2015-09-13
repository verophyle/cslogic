using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Verophyle.CSLogic
{
    public struct State<T>
        where T : IUnifiable<T>
    {
        ImmutableList<T> Slots;
        ImmutableDictionary<int, int> Bindings;

        public T this[Var v] { get { return Slots[Math.Abs(Bindings[v.Id]) - 1]; } }

        public bool Binds(Var a)
        {
            int slot;
            return Bindings.TryGetValue(a.Id, out slot) && slot > 0;
        }

        public State<T> UnifyUnboundVars(Var a, Var b)
        {
            var slot = -(Slots.Count + 1);
            return new State<T>
            {
                Slots = Slots.Add(default(T)),
                Bindings = Bindings.Add(a.Id, slot).Add(b.Id, slot)
            };
        }

        public State<T> Bind(Var v, T value)
        {
            int slot;
            if (Bindings.TryGetValue(v.Id, out slot))
            {
                if (slot >= 0) throw new Exception("Variable is already bound.");
                return new State<T>
                {
                    Slots = Slots.SetItem((-slot) - 1, value),
                    Bindings = Bindings
                };
            }
            else
            {
                slot = Slots.Count + 1;
                return new State<T>
                {
                    Slots = Slots.Add(value),
                    Bindings = Bindings.Add(v.Id, slot)
                };
            }
        }

        static State<T> empty = new State<T>
        {
            Slots = ImmutableList<T>.Empty,
            Bindings = ImmutableDictionary<int, int>.Empty
        };

        public static State<T> Empty { get { return empty; } }
    }
}
