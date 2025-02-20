using Calculator.MVVM;
using System.Collections.ObjectModel;

namespace Calculator.ViewModel
{
	class MainWindowViewModel : ViewModelBase
	{
		public ObservableCollection<NumericButtonViewModel> NumericButtons { get; set; }
		public ObservableCollection<OperatorButtonViewModel> OperatorButtons { get; set; }
		public DecimalButtonViewModel DecimalButton { get; set; }
		public ClearButtonViewModel ClearButton { get; set; }
		public EqualButtonViewModel EqualButton { get; set; }
		public InputAreaViewModel InputArea { get; set; }

		public RelayCommand NumericButtonCommand => new RelayCommand(execute => HandleNumericInput(execute.ToString()));
		public RelayCommand OperatorButtonCommand => new RelayCommand(execute => HandleOperatorInput(execute.ToString()));
		public RelayCommand DecimalButtonCommand => new RelayCommand(execute => HandleDecimalInput());
		public RelayCommand ClearInputTextCommand => new RelayCommand(execute => ClearInputText());
		public RelayCommand EqualButtonCommand => new RelayCommand(execute => ResolveExpression());

		private List<string> _inputText = new List<string>();
		private int _listIndex = 0;

		public MainWindowViewModel()
		{
			NumericButtons = new ObservableCollection<NumericButtonViewModel>();

			for (int i = 0; i < 10; i++)
			{
				NumericButtons.Add(new NumericButtonViewModel(
					$"btn{i}",
					i.ToString(),
					NumericButtonCommand
				));
			}

			OperatorButtons = new ObservableCollection<OperatorButtonViewModel>
			{
				new OperatorButtonViewModel("btnAdd", "+", OperatorButtonCommand),
				new OperatorButtonViewModel("btnSubtract", "-", OperatorButtonCommand)
			};

			DecimalButton = new DecimalButtonViewModel("btnDecimal", ".", DecimalButtonCommand);
			ClearButton = new ClearButtonViewModel("btnClear", "Clr", ClearInputTextCommand);
			EqualButton = new EqualButtonViewModel("btnEqual", "=", EqualButtonCommand);

			InputArea = new InputAreaViewModel(string.Empty);
		}

		private void HandleNumericInput(string? param)
		{
			InputArea.InputText += param;

			if (_inputText.Count > 0)
				_inputText[_listIndex] += param;
			else
				_inputText.Add(param);
		}
		private void HandleOperatorInput(string? param)
		{
			InputArea.InputText += param;
			_listIndex++;
		}
		private void HandleDecimalInput()
		{

		}
		private void ClearInputText()
		{
			InputArea.InputText = string.Empty;
		}
		private void ResolveExpression()
		{
			//Equal button has been pressed
		}
	}
}