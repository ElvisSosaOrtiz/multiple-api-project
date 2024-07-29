namespace ServiceContracts
{
    public interface IJsonConvertWrapper
    {
        T? DeserializeObject<T>(string value);
    }
}
