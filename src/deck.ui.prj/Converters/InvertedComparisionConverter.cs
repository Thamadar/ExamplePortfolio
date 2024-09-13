using Avalonia.Data.Converters;
using Avalonia;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deck.UI.Converters;
public class InvertedComparisionConverter<T> : IValueConverter
{
	public T Comparand { get; set; }

	public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		if((value is T x))
		{
			var a = Comparer<T>.Default.Compare(x, Comparand);
		}
		return value;
		//return (value is T x) ? Comparer<T>.Default.Compare(x, Comparand) != 0 : value;
	}

	public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}
