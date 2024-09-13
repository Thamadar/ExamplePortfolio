using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deck.Client.Extensions;
public static class ArraySwapExtension
{
	public static void Swap<T>(this Array array, ref T a, ref T b)
	{
		(a, b) = (b, a);
	}
}
