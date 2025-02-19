using Calculator.MVVM;

namespace Calculator.ViewModel
{
	internal class InputAreaViewModel : ViewModelBase
	{
		private string _inputText { get; set; }
		public string InputText 
		{
			get => _inputText; 
			set { if (_inputText != value) { _inputText = value; } OnPropertyChanged(); }
		}
		public InputAreaViewModel(string inputText)
		{
			_inputText = inputText;
		}
	}
}
