using System;
using ServiceStack.Redis;

namespace Nancy.Redis
{
    public class RedisSessionStore : IKeyValueStore
    {
        private readonly IRedisClient client;
        private readonly TimeSpan sessionLifetime;
        private readonly string keyPrefix;

        public RedisSessionStore(IRedisClient client) : this(client, "nancy-session:", TimeSpan.FromMinutes(20)) { }

        public RedisSessionStore(IRedisClient client, string keyPrefix, TimeSpan sessionLifetime)
        {
            this.client = client;
            this.sessionLifetime = sessionLifetime;
            this.keyPrefix = keyPrefix;
        }

        public T Load<T>(string key)
        {
            key = FormatKey(key);
            if (!client.ContainsKey(key))
                return default(T);
            client.ExpireEntryIn(key, sessionLifetime);
            return client.Get<T>(key);
        }

        public void Save<T>(string key, T value)
        {
            client.Set(FormatKey(key), value, sessionLifetime);
        }

        private string FormatKey(string key)
        {
            return keyPrefix + key;
        }
    }
}