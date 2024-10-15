using Deck.UI;
using ReactiveUI; 

namespace Deck.Client.Views.Panels;

public sealed partial class DeckEditDialogWindowViewModel : ReactiveViewModelBase
{
	private string _newName;

	public string NewName
	{
		get => _newName;
		set => this.RaiseAndSetIfChanged(ref _newName, value);
	}

	public bool IsSave { get; private set; }

	public DeckEditDialogWindowViewModel(
		string text = ""
		)
	{
		NewName = text;
	}

	public void SaveNewname() => IsSave = true; 
}
