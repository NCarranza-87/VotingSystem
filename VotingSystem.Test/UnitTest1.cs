using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace VotingSystem.Test
{
    // MathOne constructor class
    public class MathOne
    {
        private readonly ITestOne testOne;

        public MathOne(ITestOne testOne)
        {
            this.testOne = testOne;
        }

        public int Add(int a, int b) => testOne.Add(a, b);
        public void Out(string msg) => testOne.Out(msg);
    }

    // MathOneTest utilizes the Moq library to test against the interface
    public class MathOneTests
    {
        [Fact]
        public void MathOneAddTwoNumber()
        {
            var testOneMock = new Mock<ITestOne>();
            testOneMock.Setup(x => x.Add(1, 1)).Returns(2);

            var mathOne = new MathOne(testOneMock.Object);

            Assert.Equal(2, mathOne.Add(1, 1));
        }

        [Fact]
        public void VerifyFunctionHasBeenCalled()
        {
            var testOneMock = new Mock<ITestOne>();
            var msg = "Hello";

            var mathOne = new MathOne(testOneMock.Object);
            mathOne.Out(msg);

            testOneMock.Verify(x => x.Out(msg), Times.Once);
        }
    }

    // ITestOne interface allowes to test against cases
    public interface ITestOne
    {
        public int Add(int a, int b);
        public void Out(string msg);
    }

    // TestOne class allows to test for cases
    public class TestOne : ITestOne
    {
        public int Add(int a, int b) => a + b;
        public void Out(string msg)
        {
            Console.WriteLine(msg);
        }
    }

    public class TestOneTests
    {
        // Testing a single scenario
        [Fact]
        public void Add_AddsTwoNumberTogether()
        {
            var result = new TestOne().Add(1, 1);
            Assert.Equal(2, result);
        }

        // Testing scenario, where the test is given
        // multiple data parameters from a data source
        [Theory]
        [InlineData(1, 1, 2)]
        [InlineData(1, 0, 1)]
        [InlineData(0, 1, 1)]
        public void Add_AddsTwoNumberTogether_Theory(int a, int b, int expected)
        {
            var result = new TestOne().Add(a, b);
            Assert.Equal(expected, result);
        }

        // Testing a list that contains a value
        [Fact]
        public void TestListContainsValue()
        {
            var list = new List<int> { 1, 2, 3, 5 };
            Assert.Contains(1, list);
        }
    }
}
