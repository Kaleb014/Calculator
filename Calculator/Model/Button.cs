using Calculator.MVVM;

namespace Calculator.Model
{
	internal class Button
	{
		public string name { get; set; }
		public string? value { get; set; }
		public bool isSelected { get; set; }
		public RelayCommand? command { get; set; }
	}
}
