using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using imagegallery.Interfaces;
using imagegallery.Models;
using Xamarin.Forms;

namespace imagegallery.Services
{
    public class ImageService : IImageService
    {
        readonly IDBService<ImageModel> imageModelRepo;

        public ImageService(IDBService<ImageModel> imageModelRepo)
        {
            this.imageModelRepo = imageModelRepo;
        }

        public void SaveItemToDbAsync(ImageModel request)
        {
            imageModelRepo.InsertAsync(request);
        }

        public void DeleteItemToDbAsync(int iD)
        {
            imageModelRepo.DeleteAsync(iD);
        }

        public void UpdateItemToDbAsync(ImageModel request)
        {
            imageModelRepo.UpdateAsync(request);
        }

        public IEnumerable<ImageModel> GetAllItemsAsync()
        {
            return imageModelRepo.GetItemsAsync();
        }
    }
}