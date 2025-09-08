using Microsoft.SemanticKernel;
using System.Threading.Tasks;
using System.ComponentModel;

namespace HelloKernel.Plugins;

public class MathPluginNative
{
    [KernelFunction("add_numbers")]
    [Description("Adds two integers together.")]
    public int AddNumbers(int a, int b) => a + b;

    [KernelFunction("today_date")]
    [Description("Returns today's date as yyyy-MM-dd.")]
    public string TodayDate() => DateTime.UtcNow.ToString("yyyy-MM-dd");
}
