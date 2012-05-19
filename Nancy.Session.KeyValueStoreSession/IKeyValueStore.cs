namespace Nancy
{
    public interface IKeyValueStore
    {
        object Load(string key);

        void Save(string key, object value);
    }
}