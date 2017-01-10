using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace ezBot
{
	public static class EnumerableExtensions
	{
		public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
		{
			return source.Shuffle(new Random());
		}

		public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random rng)
		{
			bool flag = source == null;
			if (flag)
			{
				throw new ArgumentNullException("source");
			}
			bool flag2 = rng == null;
			if (flag2)
			{
				throw new ArgumentNullException("rng");
			}
			return source.ShuffleIterator(rng);
		}
        
		private static IEnumerable<T> ShuffleIterator<T>(this IEnumerable<T> source, Random rng)
		{
			List<T> list = source.ToList<T>();
			int num;
			for (int i = 0; i < list.Count; i = num + 1)
			{
				int index = rng.Next(i, list.Count);
				yield return list[index];
				list[index] = list[i];
				num = i;
			}
			yield break;
		}
	}
}
