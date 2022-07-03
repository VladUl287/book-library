using System.Text;
using System.Text.Json;
using System.Net.Http.Headers;

namespace Common.Configuration;

public class Doopler
{
    private const string DopplerToken = "dp.st.dev.eTOCKr6C3xHcIkjjKedT6w9luyCsnLVsT0bL57DQrdQ";
    private const string Address = "https://api.doppler.com/v3/configs/config/secrets/download?format=json";

    public static async Task<T?> GetSecretsAsync<T>()
    {
        using var client = new HttpClient();

        var basicAuthHeaderValue = Convert.ToBase64String(Encoding.Default.GetBytes($"{DopplerToken}:"));

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", basicAuthHeaderValue);

        using var streamTask = await client.GetStreamAsync(Address);

        return await JsonSerializer.DeserializeAsync<T>(streamTask);
    }
}
