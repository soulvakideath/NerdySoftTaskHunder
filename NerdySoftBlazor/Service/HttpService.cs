using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using NerdySoftBlazor.Models;

public class HttpService
{
    private readonly HttpClient _httpClient;

    public HttpService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    private async Task<ApiResult<T?>> SendRequestAsync<T>(HttpRequestMessage request, string? token)
    {
        if (!string.IsNullOrWhiteSpace(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        try
        {
            using var response = await _httpClient.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                // Handle unauthorized error (logout or refresh token)
                return ApiResult<T>.Failure("Unauthorized");
            }

            var json = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var data = JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return ApiResult<T>.Success(data);
            }

            return ApiResult<T>.Failure(json);
        }
        catch (Exception ex)
        {
            return ApiResult<T>.Failure(ex.Message);
        }
    }

    public async Task<ApiResult<T?>> GetAsync<T>(string uri, string? token = null)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, uri);
        return await SendRequestAsync<T>(request, token);
    }

    public async Task<ApiResult<T?>> PostAsync<T>(string uri, object value, string? token = null)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, uri)
        {
            Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json")
        };
        return await SendRequestAsync<T>(request, token);
    }

    public async Task<ApiResult<T?>> PutAsync<T>(string uri, object value, string? token = null)
    {
        var request = new HttpRequestMessage(HttpMethod.Put, uri)
        {
            Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json")
        };
        return await SendRequestAsync<T>(request, token);
    }

    public async Task<ApiResult<T?>> DeleteAsync<T>(string uri, string? token = null)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, uri);
        return await SendRequestAsync<T>(request, token);
    }
}
