using System;

using L3ave.Drawing;

namespace L3ave
{
	public static class Program
	{
		[STAThread]
		private static void Main()
		{
			using (var window = new Window()) {
				window.Run();
			}
		}
	}
}
