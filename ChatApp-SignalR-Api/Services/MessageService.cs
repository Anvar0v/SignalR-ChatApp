namespace ChatApp_SignalR_Api.Services;
public class MessageService
{
    public Dictionary<string, List<Tuple<string, string>>> Messages =
        new Dictionary<string, List<Tuple<string, string>>>()
        {
            { "Dotnet",new List<Tuple<string, string>>() },
            {"Blazor", new List<Tuple<string, string>>()},
            {"Javascript", new List<Tuple<string, string>>()}
        };

}
