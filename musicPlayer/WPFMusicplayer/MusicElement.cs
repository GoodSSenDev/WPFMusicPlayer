using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFMusicplayer
{
    public abstract class MusicElement
    {
		public enum Extension { MP3,WAV }
		public string Path { get; set; }
		public string Length { get; set; }
		public string FileName { get; set; }
		public Extension Type { get; set; }

		public MusicElement(string path)
		{
			this.Path = path;

			using (TagLib.File file = TagLib.File.Create(path))
			{
				this.Length = file.Properties.Duration.ToString();
				this.FileName = System.IO.Path.GetFileName(file.Name);
				this.Type = ((System.IO.Path.GetExtension(file.Name) == ".mp3") ? Extension.MP3 : Extension.WAV );
			}
		}
	}
}
