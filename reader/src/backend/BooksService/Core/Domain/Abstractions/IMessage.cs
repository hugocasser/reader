using Newtonsoft.Json;

namespace Domain.Abstractions;

public interface IMessage
{
    public string Serialize()
    {
        return JsonConvert.SerializeObject(this,
            new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All
            });
    }
}