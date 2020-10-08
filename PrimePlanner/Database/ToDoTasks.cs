using Microsoft.Data.Sqlite;
using PrimePlanner.ClassesObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace PrimePlanner.Database
{
    class ToDoTasks
    {
        private static ObservableCollection<ToDo> ToDoLists = new ObservableCollection<ToDo>();
        public static ObservableCollection<ToDo> RetrieveFromDB()
        {
            ToDoLists.Clear();
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "primePlanner.db");
            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT * from ToDoTasks", db);

                SqliteDataReader query = selectCommand.ExecuteReader();
                while (query.Read())
                {
                    int ID = Int32.Parse(query.GetString(0));
                    string title = query.GetString(1);
                    string task = query.GetString(2);
                    string courseName = query.GetString(3);
                    string priorityLevel = query.GetString(4);
                    ToDoLists.Add(new ToDo
                    {
                        ID = ID,
                        Title = title,
                        Task = task,
                        CourseName = courseName,
                        PriorityLevel = priorityLevel
                    });
                }
                db.Close();
            }
            return ToDoLists;
        }

        public async static void InitializeDatabase()
        {
            await ApplicationData.Current.LocalFolder.CreateFileAsync("primePlanner.db", CreationCollisionOption.OpenIfExists);
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "primePlanner.db");
            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                String tableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS ToDoTasks (ID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, " +
                    "Title TEXT NOT NULL," +
                    "Task TEXT NOT NULL," +
                    "CourseName TEXT NOT NULL," +
                    "PriorityLevel TEXT NOT NULL)";

                SqliteCommand createTable = new SqliteCommand(tableCommand, db);

                createTable.ExecuteReader();
            }
        }
        public static void AddData(string title, string task, string courseName, string priorityLevel)
        {
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "primePlanner.db");
            using (SqliteConnection db =
              new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                insertCommand.CommandText = "INSERT INTO ToDoTasks VALUES (NULL, @Title, @Task, @CourseName, @PriorityLevel);";

                insertCommand.Parameters.AddWithValue("@Title", title);
                insertCommand.Parameters.AddWithValue("@Task", task);
                insertCommand.Parameters.AddWithValue("@CourseName", courseName);
                insertCommand.Parameters.AddWithValue("@PriorityLevel", priorityLevel);

                insertCommand.ExecuteReader();

                db.Close();
            }
        }

        public static void DeleteTaskDB(string ID)
        {
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "primePlanner.db");
            using (SqliteConnection db =
              new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                insertCommand.CommandText = "DELETE FROM ToDoTasks WHERE ID = @ID;";
                insertCommand.Parameters.AddWithValue("@ID", ID);
                insertCommand.ExecuteReader();

                db.Close();
            }
        }

        public static void EditTaskDB(string ID, string title, string task, string courseName, string priorityLevel)
        {
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "primePlanner.db");
            using (SqliteConnection db =
              new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                insertCommand.CommandText = "UPDATE ToDoTasks SET Title = @Title, Task = @Task, CourseName = @CourseName, PriorityLevel= @PriorityLevel WHERE ID = @ID;";

                insertCommand.Parameters.AddWithValue("@ID", ID);
                insertCommand.Parameters.AddWithValue("@Title", title);
                insertCommand.Parameters.AddWithValue("@Task", task);
                insertCommand.Parameters.AddWithValue("@CourseName", courseName);
                insertCommand.Parameters.AddWithValue("@PriorityLevel", priorityLevel);


                insertCommand.ExecuteReader();

                db.Close();
            }
        }

    }
}
