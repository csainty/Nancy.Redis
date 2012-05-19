using System;
using System.Collections.Generic;
using System.Configuration;
using ServiceStack.Redis;
using Xunit;

namespace Nancy.Redis.Tests.Integration
{
    public class Tests
    {
        [Fact]
        public void CanLoadData()
        {
            string cs = ConfigurationManager.AppSettings["RedisServer"];
            if (String.IsNullOrEmpty(cs))
                return;

            // Check we don't crash when session is missing
            using (var client = new RedisClient(new Uri(cs)))
            {
                var store = new RedisSessionStore(client);
                var data = store.Load<IDictionary<string, object>>("foo");
                Assert.Null(data);
            }
            // Check we can save and reload some data
            using (var client = new RedisClient(new Uri(cs)))
            {
                var store = new RedisSessionStore(client);
                store.Save<IDictionary<string, object>>("Test", new Dictionary<string, object> { { "Key", "Value" }, { "Key2", "Value2" } });
                var data = store.Load<IDictionary<string, object>>("Test");
                Assert.Equal(2, data.Count);
                Assert.Equal("Value", data["Key"]);
                Assert.Equal("Value2", data["Key2"]);
            }

            // Test we can load an object from another connection/store isntance
            using (var client = new RedisClient(new Uri(cs)))
            {
                var store = new RedisSessionStore(client);
                var data = store.Load<IDictionary<string, object>>("Test");
                Assert.Equal(2, data.Count);
                Assert.Equal("Value", data["Key"]);
                Assert.Equal("Value2", data["Key2"]);
            }
        }
    }
}