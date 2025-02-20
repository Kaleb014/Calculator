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

		private List<string> _inputText = [string.Empty];
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
			if (_inputText[_listIndex] == string.Empty)
			{
				if (_inputText.Count > 1 &&
				(_inputText[_listIndex - 1] == "+" || _inputText[_listIndex - 1] == "="))
				{
					_inputText[_listIndex] += param;
					InputArea.InputText += param;
				}
				else
				{
					_inputText[_listIndex] += param;
					InputArea.InputText += param;
				}
			}

			else if (_inputText[_listIndex] != "+" && _inputText[_listIndex] != "-")
			{
				_inputText.Add(param);
				_listIndex++;
				_inputText.Add(string.Empty);
				_listIndex++;
				InputArea.InputText += param;
			}
		}
		private void HandleDecimalInput()
		{
			if (_inputText[_listIndex].Contains("."))
				return;

			InputArea.InputText += ".";
			_inputText[_listIndex] += ".";
		}
		private void ClearInputText()
		{
			InputArea.InputText = string.Empty;
			_inputText.Clear();
			_inputText.Add(string.Empty);
			_listIndex = 0;
		}
		private void ResolveExpression()
		{
			double leftDouble = 0;
			double rightDouble = 0;
			char operation = ' ';

			if (_inputText.Count == 0)
				return;

			foreach (string text in _inputText)
			{
				if (text == "+" || text == "-")
				{
					operation = text[0];
					continue;
				}
					
				string _text = text;
				if (double.TryParse(_text, out double result))
				{
					if (leftDouble == 0 && _text.Length > 0)
						leftDouble = result;
					else if (rightDouble == 0 && _text.Length > 0)
						rightDouble = result;
				}

				if (_text != string.Empty && operation != ' ')
				{
					switch (operation)
					{
						case '+':
							leftDouble += rightDouble;
							break;
						case '-':
							leftDouble -= rightDouble;
							break;
						default:
							return;
					}

					rightDouble = 0;
				}
			}

			InputArea.InputText = leftDouble.ToString();
			_inputText.Clear();
			_inputText.Add(leftDouble.ToString());
			_listIndex = 0;
		}
	}
}