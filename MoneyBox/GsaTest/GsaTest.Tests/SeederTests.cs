using System;
using System.IO;
using System.Reflection;
using GsaTest.Core;
using GsaTest.Seeder;
using NUnit.Framework;

namespace GsaTest.Tests
{
    [TestFixture]
    public class SeederTests
    {
        [Test]
        [Explicit("Manual Test")]
        public void CanSeedDatabase()
        {
            var applicationDirectory = new Uri(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase)).LocalPath;
            var fileLoc = applicationDirectory + @"\Resources";

            var seeder = new DatabaseSeeder(fileLoc, new CsvInputer(), new InMemoryStore());
            seeder.SeedStrategies();
        }
    }
}
