using Deck.Client.Views.Panels;
using Deck.UI; 

namespace Deck.Client.Views;

public sealed partial class MainWindowViewModel : ReactiveViewModelBase, IDisposable
{

	public string ApplicationName => "SupeDeckPro";

	public PanelsViewModel Panels { get; }

	public MainWindowViewModel(
		PanelsViewModel panelsViewModel) 
	{
		Panels = panelsViewModel;
	}

	#region Dispose

	public bool IsDisposed { get; private set; }

	public async void Dispose()
	{
		if(!IsDisposed)
		{
			IsDisposed = true; 
		}
	}

	#endregion

}
