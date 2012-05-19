using Xunit;

namespace Nancy.Session.KeyValueStoreSession.Tests.InProcessSessionStoreTests
{
    public class IntegrationTests
    {
        [Fact]
        public void InProcessSessionStore_IntegrationTests()
        {
            var store = InProcessSessionStore.Instance;
            store.Clear();

            // Should be empty to begin with
            Assert.Null(store.Load<string>("Test"));
            Assert.Null(store.Load<object>("Test"));

            // Can add data
            store.Save("Test", "Value");
            Assert.Equal("Value", store.Load<string>("Test"));

            // Can load data from a second instance
            var store2 = InProcessSessionStore.Instance;
            Assert.Equal("Value", store2.Load<string>("Test"));
        }
    }
}