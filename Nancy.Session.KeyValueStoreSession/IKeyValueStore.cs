namespace Nancy
{
    public interface IKeyValueStore
    {
        T Load<T>(string key);

        void Save<T>(string key, T value);
    }
}