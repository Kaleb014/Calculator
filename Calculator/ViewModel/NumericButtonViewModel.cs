using Calculator.MVVM;

namespace Calculator.ViewModel
{
	internal class NumericButtonViewModel : ViewModelBase
	{
		private string? _name { get; set; }
		private string? _value { get; set; }
		private RelayCommand? _command { get; set; }

		public string Name
		{
			get => _name;
			set => _name = value;
		}

		public string? Value
		{
			get => _value;
			set => _value = value;
		}

		public RelayCommand? Command
		{
			get => _command;
			set => _command = value;
		}

		public NumericButtonViewModel(string name, string value, RelayCommand command)
		{
			_name = name;
			_value = value;
			_command = command;
		}
	}
}
