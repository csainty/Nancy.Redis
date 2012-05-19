namespace Nancy.Sessions.Redis
{
    public interface IKeyValueStore : IObjectSerializerSelector
    {
        object Load(string key);

        void Save(string key, string value);
    }
}