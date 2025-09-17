using System.ComponentModel;
using Microsoft.SemanticKernel;

namespace KernelInfrastructure.Plugins;

public class HttpPlugin
{
    private readonly HttpClient _http = new HttpClient();

    [KernelFunction("get_json")]
    [Description("Fetch JSON from a given URL.")]
    public async Task<string> GetJsonAsync(string url)
    {
        var response = await _http.GetAsync(url);
        return await response.Content.ReadAsStringAsync();
    }
}
