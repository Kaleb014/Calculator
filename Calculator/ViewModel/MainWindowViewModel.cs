using Calculator.Model;
using Calculator.MVVM;
using System.Collections.ObjectModel;

namespace Calculator.ViewModel
{
	class MainWindowViewModel : ViewModelBase
	{
		public RelayCommand UpdateStringCommand => new RelayCommand(execute => UpdateString(selectedButtonValue));
		public RelayCommand ClearStringCommand => new RelayCommand(execute => ClearString(), canExecute => string.IsNullOrEmpty(input.value) == false);
		public RelayCommand CalculateValueCommand => new RelayCommand(execute => CalculateValue(input.value), canExecute => string.IsNullOrEmpty(input.value) == false);

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

			//Add operator buttons
			buttons.Add(new Button { name = "btnDecimal", value = ".", isSelected = false, command = new RelayCommand(execute => UpdateString(".")) });
			buttons.Add(new Button { name = "btnClear", value = "Clr", isSelected = false, command = ClearStringCommand });
			buttons.Add(new Button { name = "btnAdd", value = "+", isSelected = false, command = new RelayCommand(execute => UpdateString("+")) });
			buttons.Add(new Button { name = "btnSubtract", value = "-", isSelected = false, command = new RelayCommand(execute => UpdateString("-")) });
			buttons.Add(new Button { name = "btnEquals", value = "=", isSelected = false, command = CalculateValueCommand });
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

		private void CalculateValue(string _inputValue = "")
		{
			if (!string.IsNullOrWhiteSpace(_inputValue))
			{
				//TODO: Implement calculation logic
			}
		}
	}
}