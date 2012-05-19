using Xunit;

namespace Nancy.Session.KeyValueStoreSession.Tests.InProcessSessionStoreTests
{
    public class IntegrationTests
    {
        [Fact]
        public void InProcessSessionStore_IntegrationTests()
        {
            var store = new InProcessSessionStore();

            // Should be empty to begin with
            Assert.Equal("", store.Load<string>("Test"));
            Assert.Null(store.Load<object>("Test"));

            // Can add data
            store.Save("Test", "Value");
            Assert.Equal("Value", store.Load<string>("Test"));

            // Can load data from a second instance
            var store2 = new InProcessSessionStore();
            Assert.Equal("Value", store2.Load<string>("Test"));
        }
    }
}