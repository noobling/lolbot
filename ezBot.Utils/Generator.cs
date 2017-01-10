using System;
using System.Threading;

namespace ezBot.Utils
{
	public static class Generator
	{
		public static Random r
		{
			get;
			private set;
		}

		static Generator()
		{
			Generator.r = new Random();
		}

		public static int CreateRandom(int min, int max)
		{
			return Generator.r.Next(min, max);
		}

		public static void CreateRandomThread(int min, int max)
		{
			Thread.Sleep(Generator.r.Next(min, max));
		}
	}
}
