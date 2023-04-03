using System;
using System.IO;
using SQLite;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace imagegallery.Models
{
    public class ImageModel : ObservableObject
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string FileName { get; set; }
        public string Annotations { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public byte[] Content { get; set; }
        private ImageSource _imageSource;
        [Ignore]
        public ImageSource ImageSource
        {
            get => ImageSource.FromStream(() => new MemoryStream(Content));
        }
    }
}