using Avalonia.Controls;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace Deck.Client.Views.Panels;
public sealed partial class DeckEditDialogWindowViewModel
{
	public sealed class DeckEditDialogWindowViewModelCommands
	{
		public DeckEditDialogWindowViewModelCommands(DeckEditDialogWindowViewModel vm)
		{
			Accept = ReactiveCommand.Create<Window, Unit>(
			window =>
			{
				if(vm.NewName != "")
				{
					vm.IsSave = true;
					window.Close();
				}
				else
				{
					//MessageBox.Show();
				}
				return Unit.Default;
			});
			Close = ReactiveCommand.Create<Window, Unit>(
			window =>
			{
				window.Close();
				return Unit.Default;
			});
		}

		public ReactiveCommand<Window, Unit> Accept { get; }
		public ReactiveCommand<Window, Unit> Close { get; } 
	}

	private DeckEditDialogWindowViewModelCommands _commands;

	public DeckEditDialogWindowViewModelCommands Commands => _commands ??= new(this);
}
