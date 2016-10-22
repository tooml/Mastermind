using mastermind.contracts.data;
using mastermind.game;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace mastermind.tests
{
    [TestClass]
    public class CodeGeneratorTests
    {
        CodeGenerator _sut;

        [TestInitialize]
        public void ClassInitialize()
        {
            _sut = new CodeGenerator(new Random());
        }

        [TestMethod]
        public void Get_4_different_random_Pins()
        {
            IEnumerable<Pin> code_1 = _sut.Get_random_code();
            IEnumerable<Pin> code_2 = _sut.Get_random_code();

            Assert.AreEqual(4, code_1.Count());
            CollectionAssert.DoesNotContain(code_1.ToList(), Pin.None);
            CollectionAssert.AllItemsAreUnique(code_1.ToList());

            CollectionAssert.AllItemsAreUnique(code_2.ToList());
            CollectionAssert.AreNotEquivalent(code_1.ToList(), code_2.ToList());
        }

        [TestCleanup]
        public void ClassCleanup()
        {
            _sut = null;
        }
    }
}
