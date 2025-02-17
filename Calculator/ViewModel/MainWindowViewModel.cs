using Calculator.Model;
using Calculator.MVVM;
using System.Collections.ObjectModel;

namespace Calculator.ViewModel
{
	class MainWindowViewModel : ViewModelBase
	{
		public RelayCommand UpdateStringCommand => new RelayCommand(execute => UpdateString(selectedButtonValue));
		public RelayCommand ClearStringCommand => new RelayCommand(execute => ClearString(), canExecute => string.IsNullOrEmpty(input.value) == false);
		public RelayCommand BuildExpressionCommand => new RelayCommand(execute => BuildExpression(input.value), canExecute => string.IsNullOrEmpty(input.value) == false);

		private Input input;
		private Button selectedButton; //TODO: Implement selected button logic <-- may end up not needing this
		public string selectedButtonValue { get; set; }
		private ObservableCollection<Button> buttons { get; set; }

		public MainWindowViewModel()
		{
			input = new Input();
			buttons = new ObservableCollection<Button>();

			//Add numeric buttons
			for (int i = 0; i < 10; i++)
			{
				buttons.Add(
					new Button
					{
						name = $"Btn{i}",
						value = i.ToString(),
						isSelected = false,
						command = new RelayCommand(execute => UpdateString($"{i}"))
					});
			}

			//Add non-numeric buttons
			buttons.Add(new Button { name = "btnDecimal", value = ".", isSelected = false, command = new RelayCommand(execute => UpdateString(".")) });
			buttons.Add(new Button { name = "btnClear", value = "Clr", isSelected = false, command = ClearStringCommand });
			buttons.Add(new Button { name = "btnAdd", value = "+", isSelected = false, command = new RelayCommand(execute => UpdateString("+")) });
			buttons.Add(new Button { name = "btnSubtract", value = "-", isSelected = false, command = new RelayCommand(execute => UpdateString("-")) });
			buttons.Add(new Button { name = "btnEquals", value = "=", isSelected = false, command = BuildExpressionCommand });
		}

		public ObservableCollection<Button> Buttons
		{
			get { return buttons; }
			set
			{
				buttons = value;
				OnPropertyChanged();
			}
		}

		public string inputvalue
		{
			get { return input.value; }
			set
			{
				input.value = value;
				OnPropertyChanged();
			}
		}

		private void UpdateString(string _inputValue)
		{
			inputvalue += _inputValue.ToString();
		}

		private void ClearString()
		{
			inputvalue = string.Empty;
		}

		private string ResolveExpression(char _operator, double _rightExpression, double _leftExpression)
		{
			switch(_operator)
			{
				case '+':
					return (_leftExpression + _rightExpression).ToString();
				case '-':
					return (_leftExpression - _rightExpression).ToString();
				default:
					return "0";
			}
		}

		private void BuildExpression(string _inputValue = "")
		{
			if (!string.IsNullOrWhiteSpace(_inputValue))
			{
				bool skipValues = false;
				string leftExpressionStr = string.Empty;
				string rightExpressionStr = string.Empty;
				double leftExpressionDbl = 0;
				double rightExpressionDbl = 0;
				char lastChar = ' ';
				char operatorChar = ' ';
				int stringLength = _inputValue.Length;
				int counter = 0;

				foreach (char c in _inputValue)
				{
					bool updateInputValue = false;
					bool resolve = false;
					counter++;

					if(c != '+' && c != '-' && c != '.' && !char.IsDigit(c))
					{
						inputvalue = "Invalid input";
						return;
					}

					//If we have multiple decimals, we skip the rest of the values until finding an operator or end of string
					if (skipValues)
					{
						if (c == '+' || c == '-')
						{
							lastChar = c;
							operatorChar = c;
							skipValues = false;
						}
						else if (counter != stringLength)
						{
							continue;
						}
					}
					else
					{
						//If we have an operator, we are building the right expression
						if (operatorChar != ' ')
						{
							if (c != '+' && c != '-')
							{
								if (c == '.' && rightExpressionStr.Contains('.'))
								{
									skipValues = true;
									continue;
								}

								lastChar = c;
								rightExpressionStr += c;
							}
							else if (lastChar == '+' || lastChar == '-')
							{
								lastChar = c;
								operatorChar = c;
							}
							else
							{
								lastChar = c;
								operatorChar = c;
								resolve = true;
							}
						}
						//We are building the left expression
						else
						{
							if (c != '+' && c != '-')
							{
								if (c == '.' && leftExpressionStr.Contains('.'))
								{
									skipValues = true;
									continue;
								}

								lastChar = c;
								leftExpressionStr += c;
							}
							else
							{
								if (leftExpressionStr.Length == 0)
								{
									lastChar = c;
									leftExpressionStr += c;
								}
								else if (lastChar == '+' || lastChar == '-')
								{
									lastChar = c;
									leftExpressionStr = c.ToString();
								}
								else
								{
									lastChar = c;
									operatorChar = c;
								}
							}
						}
					}
					

					//If we are at the end of the string, we need to resolve the expression
					if (counter == stringLength)
					{
						updateInputValue = true;
						resolve = true;
					}

					//Resolve the expression
					if (resolve)
					{
						if (leftExpressionStr.Length == 0)
							leftExpressionDbl = 0;
						else
							leftExpressionDbl = double.Parse(leftExpressionStr);

						if (rightExpressionStr.Length == 0)
							rightExpressionDbl = 0;
						else
							rightExpressionDbl = double.Parse(rightExpressionStr);

						leftExpressionStr = ResolveExpression(operatorChar, rightExpressionDbl, leftExpressionDbl);
						rightExpressionStr = string.Empty;
						operatorChar = ' ';

						if (updateInputValue)
							inputvalue = leftExpressionStr;
					}
				}
			}		
		}
	
	}
}