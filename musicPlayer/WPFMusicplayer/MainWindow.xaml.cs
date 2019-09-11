using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using Microsoft.WindowsAPICodePack.Dialogs;


namespace WPFMusicplayer
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window, INotifyPropertyChanged
	{
		ObservableCollection<MusicElement> _playList = new ObservableCollection<MusicElement>();

		public ObservableCollection<MusicElement> PlayList
		{
			get { return _playList; }
		}

		public event PropertyChangedEventHandler PropertyChanged;
		string _filePath;
		public string FilePath
		{
			get { return _filePath; }
			set
			{
				_filePath = value;
				PropertyChanged(this, new PropertyChangedEventArgs("FilePath"));
			}
		}
		public MainWindow()
		{
			InitializeComponent();

			this.MouseLeftButtonDown += MainWindowMove_MouseLeftButtonDown;
		}

		
		private void AddPath_Click(object sender, RoutedEventArgs e)
		{
			CommonOpenFileDialog cofd = new CommonOpenFileDialog();
			cofd.IsFolderPicker = true;
			if (cofd.ShowDialog() == CommonFileDialogResult.Ok)
			{
				FilePath = cofd.FileName;
				_playList.Clear();
				DirectoryInfo folder = new DirectoryInfo(FilePath);
				FileInfo[] files = folder.GetFiles("*.mp3"); 
				foreach(FileInfo file in files)
				{
					Console.Write(file.FullName);
					_playList.Add(new MusicElement(file.FullName));
				}

			}
		}

		private void ExitBtn_Click_1(object sender, RoutedEventArgs e)
		{
			Application.Current.Shutdown();
		}

		protected void MainWindowMove_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.DragMove();
		}

		public class MusicElement
		{
			public string musicPath { get; set; }
			public string Length { get; set; }
			public string FileName { get; set; }
			public MusicElement(string path)
			{
				this.musicPath = path;

				using (TagLib.File file = TagLib.File.Create(path))
				{
					this.Length = file.Properties.Duration.ToString();
					this.FileName = System.IO.Path.GetFileName(file.Name);
				}
			}
		}
	}
}
