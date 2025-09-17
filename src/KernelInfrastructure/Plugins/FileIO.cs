using System.ComponentModel;
using Microsoft.SemanticKernel;


namespace KernelInfrastructure.Plugins;

public class FilePlugin
{
    [KernelFunction("read_file")]
    [Description("Read text content from a file path.")]
    public string ReadFile(string path)
    {
        return File.Exists(path) ? File.ReadAllText(path) : "File not found.";
    }

    [KernelFunction("write_file")]
    [Description("Write text content to a file path.")]
    public string WriteFile(string path, string content)
    {
        File.WriteAllText(path, content);
        return $"Wrote {content.Length} characters to {path}";
    }
}
