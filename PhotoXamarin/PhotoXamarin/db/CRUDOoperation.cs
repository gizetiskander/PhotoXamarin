using SQLite;
using System;
using System.Collections.Generic;
using PhotoXamarin.Models;
using System.Text;

namespace PhotoXamarin.db
{
    public class CRUDOperation
    {
        SQLiteConnection db;
        public CRUDOperation(string database)
        {
            db = new SQLiteConnection(database);
            db.CreateTable<ProjectPhoto>();
        }
        public IEnumerable<ProjectPhoto> GetProjects()
        {
            return db.Table<ProjectPhoto>();
        }
        public int SaveItem(ProjectPhoto projectphoto)
        {
            if (projectphoto.Id != 0)
            {
                db.Update(projectphoto);
                return projectphoto.Id;
            }
            else
            {
                return db.Insert(projectphoto);
            }
        }

    }
}
