using System;
using System.Linq;
using System.Threading.Tasks;
using imagegallery.Config;
using imagegallery.Models;
using SQLite;
using Xamarin.Forms;

namespace imagegallery.Helpers
{
    public class DBHelper
    {
        public static SQLiteConnection SQLiteAsyncConnection = default;
        protected DBHelper()
        {
            SQLiteAsyncConnection = new SQLiteConnection(AppConfigurations.DatabasePath, AppConfigurations.Flags);
        }

        private static void GetOrCreateTableInfoAsync<T>() where T : new()
        {
            string tableName = typeof(T).Name;
            var tabeInfo =  SQLiteAsyncConnection.GetTableInfo(tableName);
            if (!tabeInfo.Any())
            {
                 SQLiteAsyncConnection.CreateTable<T>();
            }
        }

        public static void InitializeDatabase()
        {
            _ = new DBHelper();

            //create the table if it doesn't exist 
             GetOrCreateTableInfoAsync<ImageModel>();
        }
    }
}