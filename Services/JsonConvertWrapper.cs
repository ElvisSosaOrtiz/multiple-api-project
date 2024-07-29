namespace Services
{
    using Newtonsoft.Json;
    using ServiceContracts;

    public class JsonConvertWrapper : IJsonConvertWrapper
    {
        public T? DeserializeObject<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }
    }
}
