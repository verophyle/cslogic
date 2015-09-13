using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Verophyle.CSLogic
{
    public delegate IEnumerable<State<T>> Goal<T>(State<T> state) 
        where T : IUnifiable<T>;

    public static class Goal
    {
        public static IEnumerable<State<T>> Eval<T>(Goal<T> goal) 
            where T : IUnifiable<T>
        {
            return goal(State<T>.Empty);
        }

        public static Goal<T> Unify<T>(Var v, T value) 
            where T : IUnifiable<T>
        {
            return (state) =>
            {
                if (state.Binds(v))
                    return state[v].Unify(value, state);
                else
                    return new[] { state.Bind(v, value) };
            };
        }

        public static Goal<T> Unify<T>(T value, Var v) 
            where T : IUnifiable<T>
        {
            return Unify(v, value);
        }

        public static Goal<T> Unify<T>(Var a, Var b) 
            where T : IUnifiable<T>
        {
            return (state) =>
            {
                bool ba = state.Binds(a);
                bool bb = state.Binds(b);

                if (ba && bb)
                {
                    return state[a].Unify(state[b], state);
                }
                else if (ba)
                {
                    return new[] { state.Bind(b, state[a]) };
                }
                else if (bb)
                {
                    return new[] { state.Bind(a, state[b]) };
                }
                else
                {
                    return new[] { state.UnifyUnboundVars(a, b) };
                }
            };
        }

        public static Goal<T> Unify<T>(T a, T b) 
            where T : IUnifiable<T>
        {
            return (state) => a.Unify(b, state);
        }

        public static Goal<T> Pred<T>(Var v, Func<Var, bool> pred)
            where T : IUnifiable<T>
        {
            return (state) => pred(v) ? new[] { state } : Enumerable.Empty<State<T>>();
        }

        public static Goal<T> Conj<T>(Goal<T> a, Goal<T> b) 
            where T : IUnifiable<T>
        {
            return (state) => Combine(a, b, state);
        }

        static IEnumerable<State<T>> Combine<T>(Goal<T> a, Goal<T> b, State<T> s) 
            where T : IUnifiable<T>
        {
            foreach (var sa in a(s))
            {
                foreach (var sb in b(sa))
                {
                    yield return sb;
                }
            }
        }

        public static Goal<T> Disj<T>(Goal<T> a, Goal<T> b) 
            where T : IUnifiable<T>
        {
            return (state) => Alternate(a, b, state);
        }

        static IEnumerable<State<T>> Alternate<T>(Goal<T> a, Goal<T> b, State<T> s) 
            where T : IUnifiable<T>
        {
            var ae = a(s).GetEnumerator();
            var be = b(s).GetEnumerator();

            bool hasA = ae.MoveNext();
            bool hasB = be.MoveNext();

            while (hasA || hasB)
            {
                if (hasA)
                {
                    yield return ae.Current;
                    hasA = ae.MoveNext();
                }
                if (hasB)
                {
                    yield return be.Current;
                    hasB = be.MoveNext();
                }
            }
        }
    }
}
