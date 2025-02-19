using Calculator.MVVM;
using System.Collections.ObjectModel;

namespace Calculator.ViewModel
{
	class MainWindowViewModel : ViewModelBase
	{
		//Should Clr and Decimal be considered as operators?
		//Should they be there own type?

		public ObservableCollection<NumericButtonViewModel> NumericButtons { get; set; }
		public ObservableCollection<OperatorButtonViewModel> OperatorButtons { get; set; }
		public EqualButtonViewModel EqualButton { get; set; }

		public RelayCommand NumericButtonCommand => new RelayCommand(execute => AddInputToString());
		public RelayCommand OperatorButtonCommand => new RelayCommand(execute => HandleOperatorInput());
		public RelayCommand EqualButtonCommand => new RelayCommand(execute => ResolveExpression());

		private List<string> _inputText = new List<string>();
		private int _listIndex = 0;

		public MainWindowViewModel()
		{
			NumericButtons = new ObservableCollection<NumericButtonViewModel>();

			for (int i = 0; i < 10; i++)
			{
				NumericButtons.Add(new NumericButtonViewModel(
					$"btn{i}",
					i.ToString(),
					NumericButtonCommand
				));
			}

			OperatorButtons = new ObservableCollection<OperatorButtonViewModel>
			{
				new OperatorButtonViewModel("btnDecimal", ".", NumericButtonCommand),
				new OperatorButtonViewModel("btnClear", "Clr", NumericButtonCommand),
				new OperatorButtonViewModel("btnAdd", "+", NumericButtonCommand),
				new OperatorButtonViewModel("btnSubtract", "-", NumericButtonCommand)
			};

			EqualButton = new EqualButtonViewModel("btnEqual", "=", NumericButtonCommand);
		}

		private void AddInputToString()
		{
			//Add the value of the button to the current list index
			//Index will be incremented when an operator is pressed or entered
		}
		private void HandleOperatorInput()
		{
			//Add/Subtract, Clr, Decimal
		}
		private void ResolveExpression()
		{
			//Equal button has been pressed
		}
	}
}