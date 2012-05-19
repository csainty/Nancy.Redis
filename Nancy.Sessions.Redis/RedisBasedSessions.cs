using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nancy.Bootstrapper;

namespace Nancy.Sessions.Redis
{
    public class RedisBasedSessions : IKeyValueStore
    {
        public object Load(string key)
        {
            throw new NotImplementedException();
        }

        public void Save(string key, object value)
        {
            throw new NotImplementedException();
        }
    }
}