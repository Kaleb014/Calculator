using Calculator.MVVM;
using System.Collections.ObjectModel;

namespace Calculator.ViewModel
{
	class MainWindowViewModel : ViewModelBase
	{
		public RelayCommand AddNumberCommand => new RelayCommand(execute => AddNumber());
		private ObservableCollection<NumericButtonViewModel> _buttons { get; set; }

		public MainWindowViewModel()
		{
			Buttons = new ObservableCollection<NumericButtonViewModel>();

			for (int i = 0; i < 10; i++)
			{
				Buttons.Add(new NumericButtonViewModel(
					$"btn{i}",
					i.ToString(),
					AddNumberCommand
				));
			}
		}

		public ObservableCollection<NumericButtonViewModel> Buttons { get; private set; }

		private void AddNumber()
		{
			// Add number to the display
		}
	}
}