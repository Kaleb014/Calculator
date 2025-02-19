using Calculator.MVVM;

namespace Calculator.ViewModel
{
	internal class InputAreaViewModel : ViewModelBase
	{
		public string InputValue { get; set; }

		public InputAreaViewModel()
		{
			InputValue = "0";
		}
	}
}
