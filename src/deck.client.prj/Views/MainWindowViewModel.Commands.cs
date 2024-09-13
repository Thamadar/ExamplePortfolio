using Avalonia.Controls;

using ReactiveUI;

using System.Reactive; 

namespace Deck.Client.Views;
public sealed partial class MainWindowViewModel
{
	public sealed class MainWindowViewModelCommands
	{
		public MainWindowViewModelCommands(MainWindowViewModel vm)
		{ 
			Minimize = ReactiveCommand.Create<Window, Unit>(
				wnd =>
				{
					var a = Thread.CurrentThread;
					wnd.WindowState = WindowState.Minimized;
					return Unit.Default;
				});

			Maximize = ReactiveCommand.Create<Window, Unit>(
				wnd =>
				{
					wnd.WindowState = wnd.WindowState == WindowState.Normal ?
										WindowState.Maximized :
										WindowState.Normal;
					return Unit.Default;
				});

			Close = ReactiveCommand.Create<Window, Unit>(
				wnd =>
				{
					wnd.Close();
					return Unit.Default;
				});
		}

		public ReactiveCommand<Window, Unit> Minimize { get; }

		public ReactiveCommand<Window, Unit> Maximize { get; }

		public ReactiveCommand<Window, Unit> Close { get; }
	}

	private MainWindowViewModelCommands _commands;

	public MainWindowViewModelCommands Commands => _commands ??= new(this);
}
