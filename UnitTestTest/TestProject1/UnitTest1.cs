using ConsoleApp2;
using NUnit.Framework;

namespace TestProject1
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            //Arrange 
            var testdata = new string[] { "blueberry", "apple", "zuchini", "cherry", "tamarind"};

            // Act 
            var result = new OrderFunction().OrderByAssumingSpecial(testdata);

            // Assert
            Assert.That(result[0], Has.Length.EqualTo(2)) ;
            Assert.That(result[1], Is.EqualTo("zuchini" )) ;
            Assert.That(result[2], Is.EqualTo("blueberry" )) ;
            Assert.That(result[3], Is.EqualTo("cherry" )) ;
            Assert.That(result[4], Is.EqualTo("tamarind" )) ;
            //Assert.IsTrue(result.SequenceEqual(new string[] { "apple", "zuchini", "blueberry", "cherry", "tamarind" }));
        }
    }
}