using System.ComponentModel;
using Microsoft.SemanticKernel;


namespace KernelInfrastructure.Plugins;

public class GitHubPlugin
{
    private readonly HttpClient _http = new HttpClient();

    [KernelFunction("get_issues")]
    [Description("Get open GitHub issues for a repository in JSON format.")]
    public async Task<string> GetIssuesAsync(string repo)
    {
        _http.DefaultRequestHeaders.UserAgent.ParseAdd("semantic-kernel-demo");
        return await _http.GetStringAsync($"https://api.github.com/repos/{repo}/issues");
    }
}
