using System;
using System.Json;
using NUnit.Framework;
using Currency;

namespace CurrencyTests
{
    [TestFixture]
    public class CurrencyUpdateFixtures
    {
        [Test]
        public void ShouldCreateByJsonValue()
        {
            var json = "{ \"timestamp\" => 1336197653, \"base\" => \"USD\", \"rates\" => { \"AED\" => 3.6732 } }";
            var jsonValue = JsonObject.Parse(json);

            var update = new CurrencyUpdate(jsonValue);

            Assert.Equals("USD", update.Base);
            Assert.Equals("20120505160053", update.Timestamp.ToString("yyyyMMddHHmmss"));
            Assert.Equals("AED", update.Infos[0].Code);
            Assert.Equals(3.6732, update.Infos[0].Rate);
        }

        [Test]
        public void ShouldGetLatest()
        {
            var update = CurrencyUpdate.Latest();
            Assert.Equals("USD", update.Base);
            Assert.True(update.Infos.Count > 0);
        }
    }
}

