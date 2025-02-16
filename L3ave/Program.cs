using System;

using L3ave.View;

namespace L3ave
{
	public static class Program
	{
		private static void Main()
		{
			using (var window = new Window()) {
				window.Run();
			}
		}
	}
}
