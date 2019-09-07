using System;
using System.Collections.Generic;
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

			this.MouseLeftButtonDown += MainWindowMove_MouseLeftButtonDown;
		}

		
		private void AddPath_Click(object sender, RoutedEventArgs e)
		{

		}

		private void ExitBtn_Click_1(object sender, RoutedEventArgs e)
		{
			Application.Current.Shutdown();
		}

		protected void MainWindowMove_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.DragMove();
		}
	}
}
