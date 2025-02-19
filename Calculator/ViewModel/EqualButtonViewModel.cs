using Calculator.MVVM;

namespace Calculator.ViewModel
{
	internal class EqualButtonViewModel : ViewModelBase
	{
		public string Name { get; set; }
		public string? Value { get; set; }
		public RelayCommand? command { get; set; }

		public EqualButtonViewModel(string name, string? value, RelayCommand? command)
		{
			Name = name;
			Value = value;
			this.command = command;
		}
	}
}
