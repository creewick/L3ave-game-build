using System;
using System.IO;
using System.Threading.Tasks;

namespace L3ave
{
    static class Program
    {
        private static async Task Main(string[] args)
        {
            Environment.SetEnvironmentVariable("FNA_PLATFORM_BACKEND", "SDL2");

            var window = new L3ave.View.Window();
            window.Run();

            while (window.IsRunning)
            {
                await Task.Delay(100);
            }
        }
    }
}
