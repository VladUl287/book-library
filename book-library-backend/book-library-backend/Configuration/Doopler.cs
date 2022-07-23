using System.Text;
using System.Text.Json;
using System.Net.Http.Headers;

namespace Domain.Configuration;

public class Doopler
{
    private const string DopplerToken = "dp.st.dev.QwbNT8Vz02KiaVlu0x9Wl4geEBdUc8MaPsw7CMYfHQu";
    private const string Address = "https://api.doppler.com/v3/configs/config/secrets/download?format=json";

    public static async Task<T> GetSecretsAsync<T>()
    {
        using var client = new HttpClient();

        var basicAuthHeaderValue = Convert.ToBase64String(Encoding.Default.GetBytes($"{DopplerToken}:"));

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", basicAuthHeaderValue);

        using var streamTask = await client.GetStreamAsync(Address);

        var result = await JsonSerializer.DeserializeAsync<T>(streamTask);

        if (result is null)
        {
            throw new Exception("Ошибка получения конфигурации приложения.");
        }

        return result;
    }
}
