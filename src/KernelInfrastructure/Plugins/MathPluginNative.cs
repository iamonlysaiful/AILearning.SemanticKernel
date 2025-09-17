using Microsoft.SemanticKernel;
using System.Threading.Tasks;
using System.ComponentModel;

namespace KernelInfrastructure.Plugins;

public class MathPluginNative
{
    [KernelFunction("add_numbers")]
    [Description("Adds two integers together.")]
    public int AddNumbers(int a, int b) => a + b;

    [KernelFunction("today_date")]
    [Description("Returns today's date as yyyy-MM-dd.")]
    public string TodayDate() => DateTime.UtcNow.ToString("yyyy-MM-dd");

    [KernelFunction, Description("Multiply two integers")]
    public int Multiply(int a, int b) => a * b;
}
