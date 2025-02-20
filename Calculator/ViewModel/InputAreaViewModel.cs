using Calculator.MVVM;

namespace Calculator.ViewModel
{
	internal class InputAreaViewModel : ViewModelBase
	{
		private string _inputText { get; set; }
		public string InputText 
		{
			get => _inputText; 
			set { if (_inputText != value && IsInputValid(value)) { _inputText = value; } OnPropertyChanged(); }
		}
		public InputAreaViewModel(string inputText)
		{
			_inputText = inputText;
		}

		private static bool IsInputValid(string value)
		{
			if (value.Length > 0)
			{
				char lastChar = value[value.Length - 1];
				if (lastChar != '+' && lastChar != '-' && lastChar != '.' && !char.IsDigit(lastChar))
				{
					value = value.Remove(value.Length - 1);
					return false;
				}
			}

			return true;
		}
	}
}
