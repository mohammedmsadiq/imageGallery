using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using imagegallery.Helpers;
using imagegallery.Interfaces;
using Xamarin.Forms;

namespace imagegallery.Services
{
    public class DBService<T> : IDBService<T> where T : new()
    {
        public void InsertAsync(T a)  
        {
             DBHelper.SQLiteAsyncConnection.Insert(a);
        }

        public void InsertAllAsync(IEnumerable<T> a)
        {
             DBHelper.SQLiteAsyncConnection.InsertAll(a);
        }

        public void InsertOrReplaceAsync(T a)
        {
             DBHelper.SQLiteAsyncConnection.InsertOrReplace(a);
        }

        public void UpdateAsync(T a)
        {
             DBHelper.SQLiteAsyncConnection.Update(a);
        }

        public void UpdateAllAsync(IEnumerable<T> a)
        {
             DBHelper.SQLiteAsyncConnection.UpdateAll(a, runInTransaction: false);
        }

        public void DeleteAsync(int id)
        {
             DBHelper.SQLiteAsyncConnection.Delete<T>(id);
        }

        public void DeleteAllAsync()
        {
             DBHelper.SQLiteAsyncConnection.DeleteAll<T>();
        }

        public List<T> GetItemsAsync()
        {
            var data = DBHelper.SQLiteAsyncConnection.Table<T>().ToList();
            return data;
        }
    }
}