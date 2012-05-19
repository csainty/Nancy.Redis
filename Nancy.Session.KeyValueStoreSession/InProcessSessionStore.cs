using System.Collections.Concurrent;

namespace Nancy.Session
{
    public class InProcessSessionStore : IKeyValueStore
    {
        private static InProcessSessionStore _Instance = new InProcessSessionStore();

        public static InProcessSessionStore Instance { get { return _Instance; } }

        private ConcurrentDictionary<string, object> _Store = new ConcurrentDictionary<string, object>();

        private InProcessSessionStore() { }

        public T Load<T>(string key)
        {
            object o;
            if (!_Store.TryGetValue(key, out o))
                return default(T);
            return (T)o;
        }

        public void Save<T>(string key, T value)
        {
            _Store.AddOrUpdate(key, value, (k, v) => v);
        }

        public void Clear()
        {
            _Store.Clear();
        }
    }
}