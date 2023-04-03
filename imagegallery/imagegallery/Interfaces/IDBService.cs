using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace imagegallery.Interfaces
{
	public interface IDBService<T>
    {
        void DeleteAsync(int id);
        void DeleteAllAsync();
        List<T> GetItemsAsync();
        void InsertAllAsync(IEnumerable<T> a);
        void InsertAsync(T a);
        void InsertOrReplaceAsync(T a);
        void UpdateAsync(T a);
        void UpdateAllAsync(IEnumerable<T> a);
    }
}