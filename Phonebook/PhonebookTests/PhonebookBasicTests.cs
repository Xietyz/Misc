using NUnit.Framework;
using Phonebook;

namespace PhonebookTests.PhonebookBasicTests
{
    [TestFixture]
    public class PhonebookBasicTests
    {
        [Test]
        public void DictionariesCanInitialise()
        {
            FunctionDictionary dictionary = new FunctionDictionary();
            Assert.IsNotNull(dictionary.ContactDict);
            Assert.IsNotNull(dictionary.Functions);
        }
        [Test]
        public void CanGetNumberByContact()
        {

        }
        [Test]
        public void CanAddContact()
        {

        }
        [Test]
        public void CanDeleteContact()
        {

        }
        [Test]
        public void CanUpdateContact()
        {

        }
        [Test]
        public void DoesNotAllowNamesOver4Characters()
        {

        }
        [Test]
        public void DoesNotAllowNumbersOver11Digits()
        {

        }
    }
}