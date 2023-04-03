using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using imagegallery.Models;
using Xamarin.Forms;
namespace imagegallery.Interfaces
{
	public interface IImageService
    {
        void SaveItemToDbAsync(ImageModel request);

        void DeleteItemToDbAsync(int iD);

        void UpdateItemToDbAsync(ImageModel request);

        IEnumerable<ImageModel> GetAllItemsAsync();
    }
}