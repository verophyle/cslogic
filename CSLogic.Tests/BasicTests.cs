using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Verophyle.CSLogic.Tests
{
    [TestClass]
    public class BasicTests
    {
        Random rng = new Random();

        [TestMethod]
        public void TestUnifyAV()
        {
            var a = Var.NewVar();
            var v = new BasicVal { Value = rng.Next() };
            var g = Goal.Unify(a, v);
            var s = Goal.Eval(g).First();
            Assert.AreEqual(v.Value, s[a].Value);
        }

        [TestMethod]
        public void TestUnifyVA()
        {
            var a = Var.NewVar();
            var v = new BasicVal { Value = rng.Next() };
            var g = Goal.Unify(v, a);
            var s = Goal.Eval(g).First();
            Assert.AreEqual(v.Value, s[a].Value);
        }

        [TestMethod]
        public void TestUnifyVbV()
        {
            var a = Var.NewVar();
            var b = Var.NewVar();
            var v = new BasicVal { Value = rng.Next() };
            var g = Goal.Conj(Goal.Unify(a, v), Goal.Unify<BasicVal>(a, b));
            var s = Goal.Eval(g).First();
            Assert.AreEqual(v.Value, s[b].Value);
        }

        [TestMethod]
        public void TestUnifyVVb()
        {
            var a = Var.NewVar();
            var b = Var.NewVar();
            var v = new BasicVal { Value = rng.Next() };
            var g = Goal.Conj(Goal.Unify<BasicVal>(a, b), Goal.Unify(b, v));
            var s = Goal.Eval(g).First();
            Assert.AreEqual(v.Value, s[a].Value);
        }

        [TestMethod]
        public void TestDisjunction()
        {
            var a = Var.NewVar();
            var v1 = new BasicVal { Value = rng.Next() };
            var v2 = new BasicVal { Value = rng.Next() };
            var g = Goal.Disj(Goal.Unify(a, v1), Goal.Unify(a, v2));
            var s = Goal.Eval(g).ToArray();
            Assert.AreEqual(v1.Value, s[0][a].Value);
            Assert.AreEqual(v2.Value, s[1][a].Value);
        }
    }
}
