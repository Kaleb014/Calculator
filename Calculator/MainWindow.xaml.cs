using Calculator.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace Calculator
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void btn_Click(object sender, RoutedEventArgs e)
		{
			MainWindowViewModel vm = (MainWindowViewModel)DataContext;
			vm.selectedButtonValue = ((Button)sender).Content.ToString();
		}
	}
}