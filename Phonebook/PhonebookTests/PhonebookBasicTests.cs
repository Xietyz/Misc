using NUnit.Framework;
using Phonebook;
using System;

namespace PhonebookTests.PhonebookBasicTests
{
    [TestFixture]
    public class PhonebookBasicTests
    {
        [Test]
        public void DictionariesCanInitialise()
        {
            FunctionDictionary dictionary = new FunctionDictionary(new PhonebookFileReader());
            Assert.IsNotNull(dictionary.ContactDict);
            Assert.IsNotNull(dictionary.Functions);
        }
        [Test]
        public void CanAddContact()
        {
            FunctionDictionary dictionary = new FunctionDictionary(new PhonebookFileReader());

            dictionary.Execute("STORE t1 10987654321");

            Assert.AreEqual(dictionary.Execute("GET t1"), "10987654321");
        }
        [Test]
        public void CanDeleteContact()
        {
            FunctionDictionary dictionary = new FunctionDictionary(new PhonebookFileReader());

            dictionary.Execute("STORE t1 10987654321");
            dictionary.Execute("DELETE t1");

            Assert.IsNull(dictionary.Execute("GET t1"));
        }
        [Test]
        public void CanUpdateContact()
        {
            FunctionDictionary dictionary = new FunctionDictionary(new PhonebookFileReader());

            dictionary.Execute("STORE t1 10987654321");
            dictionary.Execute("UPDATE t1 1234");

            Assert.AreEqual(dictionary.Execute("GET t1"), "1234");
        }
        [Test]
        public void DoesNotAllowNumbersOver11Digits()
        {
            FunctionDictionary dictionary = new FunctionDictionary(new PhonebookFileReader());

            Assert.AreEqual(dictionary.Execute("STORE t1 1234567891011"), "Number too large");
        }
        [Test]
        public void DisplaysErrorOnInvalidCommand()
        {
            FunctionDictionary dictionary = new FunctionDictionary(new PhonebookFileReader());

            Assert.AreEqual(dictionary.Execute("STORES t1 abc1"), "Invalid input");
            Assert.AreEqual(dictionary.Execute("WDAFWGfa 22"), "Invalid input");
        }
        [Test]
        public void DisplaysErrorOnNumberWithCharacters()
        {
            FunctionDictionary dictionary = new FunctionDictionary(new PhonebookFileReader());

            dictionary.Execute("STORE t2 123");

            Assert.AreEqual(dictionary.Execute("STORE t1 abc1"), "Number should only be digits");
            Assert.AreEqual(dictionary.Execute("UPDATE t2 72ds"), "Number should only be digits");
        }
        [Test]
        public void DisplaysErrorWhenContactNotFound()
        {
            FunctionDictionary dictionary = new FunctionDictionary(new PhonebookFileReader());

            Assert.AreEqual(dictionary.Execute("DELETE t1"), "Does not exist");
            Assert.AreEqual(dictionary.Execute("UPDATE t2 123"), "Does not exist");
        }
    }
}