using Deck.Client.Views.Panels;
using Deck.UI;
using DynamicData;
using ReactiveUI;
using System.Text.RegularExpressions;
namespace Deck.Client.Views;

public sealed partial class MainWindowViewModel : ReactiveViewModelBase, IDisposable
{  
	private static readonly string _patternString   = "(\"[^\"\\\\]*(?:\\\\.[^\"\\\\]*)*\")|(\'[^\'\\\\]*(?:\\\\.[^\'\\\\]*)*\')";
	private static readonly string _patternFunc     = @"\w+\([\w.,'""\s]+\)";
	private static readonly string _patternBrackets = @"\([\w.,'""\s]+\)"; 
	public string ApplicationName => "SupeDeckPro"; 

	public PanelsViewModel Panels { get; }

	public MainWindowViewModel(
		PanelsViewModel panelsViewModel) 
	{
		Panels = panelsViewModel;
		TestTestTEST(null);
	}

	private async Task<string> TestTestTEST(string inputText)
	{
		inputText = "5, 6, 7, 4, line_ex(1, 2, 3, some_func(\"1.5, 6546, 5654, 7\", some_func(\"EndFUNC\"), 12, 5435656))";

		var inputTextForFuncMatches = inputText;
		var outputResult            = "";

		var regexFunc   = new Regex(_patternFunc);
		var regexString = new Regex(_patternString);

		var funcMatches = regexFunc.Matches(inputText);
		 
		var funcParts = new List<(string matchText, int positionInOriginalText)>();
		  
		while(funcMatches.Count() > 0)
		{
			var currentMatch = funcMatches.Select(x => (x.Value, x.Index)).FirstOrDefault();
			funcParts.Add((currentMatch.Value, currentMatch.Index));

			inputTextForFuncMatches = inputTextForFuncMatches.Replace(currentMatch.Value, "");
			funcMatches             = regexFunc.Matches(inputTextForFuncMatches);
		}

		if(inputTextForFuncMatches != "")
		{
			funcParts.Add(new (inputTextForFuncMatches, 0));
		}

		var quadCount = 0;
		var parsedParts = new List<(string matchText, int positionInOriginalText, bool isQuad)>();
		for(int i = 0; i < funcParts.Count; i++)
		{
			var currectMatch = funcParts[i];
			var (resultMatch, isHasQuad) = await CheckQuad(currectMatch.matchText);
			parsedParts.Add(new(resultMatch, funcParts[i].positionInOriginalText, isHasQuad)); 
		}
		for(int i = parsedParts.Count-1; i !> -1; i--)
		{
			if(i == parsedParts.Count-1)
			{
				outputResult = parsedParts[i].matchText;
			}
			else
			{
				outputResult = outputResult.Insert(parsedParts[i].positionInOriginalText + (quadCount), parsedParts[i].matchText);
			}
			if(parsedParts[i].isQuad)
			{
				quadCount++;
			}
		}

		return outputResult;
	}

	private async Task<(string, bool)> CheckQuad(string inputText)
	{
		var regexBrackets = new Regex(_patternBrackets);
		var regexString   = new Regex(_patternString);
		var isQuad        = false;

		var bracketsMatchText   = regexBrackets.Matches(inputText).FirstOrDefault();  
		if(bracketsMatchText == null)
		{    
			var stringMatches = regexString.Matches(inputText);

			var totalDigits = inputText.Where(x => x == ',').Count();
			var totalDigitsStringMatches = 0;
			foreach(var stringMatch in stringMatches.Select(x => x.Value))
			{
				totalDigitsStringMatches = totalDigitsStringMatches + stringMatch.Where(x => x == ',').Count();
			}
			if(totalDigits > totalDigitsStringMatches)
			{
				inputText = inputText.Insert(0, "[");
				inputText = inputText.Insert(inputText.Length, "]");
				isQuad = true;
			}

			return (inputText, isQuad);
		}
		else
		{
			var inputBracketsText      = bracketsMatchText.Value;
			var matchIndex             = bracketsMatchText.Index;
			inputText                  = inputText.Replace(inputBracketsText, "");
			var quadResultBracketsText = inputBracketsText;

			var stringMatches = regexString.Matches(inputBracketsText);

			var totalDigits = inputBracketsText.Where(x => x == ',').Count();
			var totalDigitsStringMatches = 0;
			foreach(var stringMatch in stringMatches.Select(x => x.Value))
			{
				totalDigitsStringMatches = totalDigitsStringMatches + stringMatch.Where(x => x == ',').Count();
			}
			if(totalDigits > totalDigitsStringMatches)
			{
				quadResultBracketsText = quadResultBracketsText.Insert(1, "[");
				quadResultBracketsText = quadResultBracketsText.Insert(quadResultBracketsText.Length, "]");
				isQuad = true;
			}

			return (inputText.Insert(matchIndex, quadResultBracketsText), isQuad);
		} 
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
