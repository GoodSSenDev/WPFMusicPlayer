using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;



namespace WPFMusicplayer
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{

		public MainWindow()
		{
			InitializeComponent();
			
			this.DataContext = new ViewModel();
			this.MouseLeftButtonDown += MainWindowMove_MouseLeftButtonDown;
		}


		
		//Exit button
		private void ExitBtn_Click_1(object sender, RoutedEventArgs e)
		{
			Application.Current.Shutdown();
		}
		//Allow the mouse can drag the window
		protected void MainWindowMove_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.DragMove();
		}


		private void button_Click(object sender, RoutedEventArgs e)
		{

		}

		private void playList_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

		}
	}
}
