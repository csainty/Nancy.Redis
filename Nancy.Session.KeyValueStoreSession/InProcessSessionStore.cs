using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nancy.Session
{
    public class InProcessSessionStore : IKeyValueStore
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