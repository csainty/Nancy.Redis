using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nancy.Bootstrapper;

namespace Nancy.Redis
{
    public class RedisBasedSessions : IKeyValueStore
    {
        public T Load<T>(string key)
        {
            throw new NotImplementedException();
        }

        public void Save<T>(string key, T value)
        {
            throw new NotImplementedException();
        }
    }
}