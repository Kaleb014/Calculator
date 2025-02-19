using Calculator.MVVM;

namespace Calculator.ViewModel
{
	internal class ClearButtonViewModel : ViewModelBase
	{
		private string? _name { get; set; }
		private string? _text { get; set; }
		private RelayCommand? _command { get; set; }

		public string Name
		{
			get => _name;
			set { if (_name != value) { _name = value; } }
		}

		public string? Text
		{
			get => _text;
			set { if (_text != value) { _text = value; } }
		}

		public RelayCommand? Command
		{
			get => _command;
			set { if (_command != value) { _command = value; } }
		}

		public ClearButtonViewModel(string name, string text, RelayCommand command)
		{
			_name = name;
			_text = text;
			_command = command;
		}
	}
}
