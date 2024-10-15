using Avalonia;
using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Deck.Client.Data;
using System.Collections.Generic;
using System.Globalization; 

namespace Deck.Client.Views.Panels;
public class CardConverter : IMultiValueConverter
{
	private static readonly string RootPath = "deck.ui/Assets/Images/Cards";
	/// <inheritdoc/>
	public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
	{
		if(values.Count == 2)
		{
			if(values[0] is UnsetValueType ||
				values[1] is UnsetValueType)
			{
				return AvaloniaProperty.UnsetValue;
			}

			var suit = values[0] != null ?
					   (Suit)(values[0]) :
					   Suit.Spades;
			var powerValue = values[1] != null ?
						     (int)values[1]
						     : -1; 

			var uri = GetCardIconUri(suit, powerValue);
			if(uri != null)
			{
				Stream? asset;
				try
				{
					asset = AssetLoader.Open(uri); 
					return new Bitmap(asset);
				}
				catch(Exception e)
				{
					return AvaloniaProperty.UnsetValue;
				}  
			} 
			return AvaloniaProperty.UnsetValue;
		}
		else
		{
			return AvaloniaProperty.UnsetValue;
		} 
	}

	private Uri GetCardIconUri(Suit suit, int powerValue)
	{
		if(suit != Suit.JokerLight && suit != Suit.JokerDark)
		{
			return new Uri($"avares://{RootPath}/{suit}{powerValue}.png");
		}
		return new Uri($"avares://{RootPath}/{suit}.png");
	}
}
