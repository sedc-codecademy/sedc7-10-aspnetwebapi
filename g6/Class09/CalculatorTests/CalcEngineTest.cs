using System;
using Calculator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CalculatorTests
{
    [TestClass]
    public class CalcEngineTest
    {
        static double Delta = 0.00001;

        [TestMethod]
        public void Engine_Multiply_Integers_ReturnsCorrectResult()
        {
            // 1. Arrange
            var engine = new CalcEngine();
            var first = 10;
            var second = 7;
            var expected = 70;
            // 2. Act
            var actual = engine.Multiply(first, second);
            // 3. Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Engine_Multiply_Doubles_ReturnsCorrectResult()
        {
            // 1. Arrange
            var engine = new CalcEngine();
            var first = 1.4142135623730950488016887242097;
            var second = 1.4142135623730950488016887242097;
            var expected = 2;
            // 2. Act
            var actual = engine.Multiply(first, second);
            // 3. Assert
            Assert.AreEqual(expected, actual, Delta);
        }

        [TestMethod]
        public void Engine_Divide_Integers_ReturnsCorrectResult()
        {
            // 1. Arrange
            var engine = new CalcEngine();
            var first = 70;
            var second = 7;
            var expected = 10;
            // 2. Act
            var actual = engine.Divide(first, second);
            // 3. Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Engine_Divide_Integers_DivideByZero()
        {
            // 1. Arrange
            var engine = new CalcEngine();
            var first = 70;
            var second = 0;
            // 2. Act
            Action actualCall = () => engine.Divide(first, second);
            // 3. Assert
            Assert.ThrowsException<ApplicationException>(actualCall);
        }
    }
}
