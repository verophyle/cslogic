using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuKanren
{
    public struct Variable : IEquatable<Variable>
    {
        internal int id;

        public Variable(int id)
        {
            this.id = id;
        }

        public bool Equals(Variable other)
        {
            return this.id == other.id;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (obj is Variable)
                return this.id == ((Variable)obj).id;
            return false;
        }

        public override int GetHashCode()
        {
            return id.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("#{0}", id);
        }
    }
}
