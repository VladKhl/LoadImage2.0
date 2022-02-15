using LoadImage.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace LoadImage.db
{
    public class CRUDOperation
    {
        readonly SQLiteConnection db;
        public CRUDOperation(string databasePath)
        {
            db = new SQLiteConnection(databasePath);
            db.CreateTable<ImageModel>();
        }
        public IEnumerable<ImageModel> GetObjects()
        {
            return db.Table<ImageModel>();
        }

        public int DelProj(int id) { return db.Delete<ImageModel>(id); }
        public int SaveItem(ImageModel imageModel)
        {
            if (imageModel.Id != 0)
            {
                db.Update(imageModel);
                return imageModel.Id;
            }
            else
            {
                return db.Insert(imageModel);
            }
        }
    }
}
