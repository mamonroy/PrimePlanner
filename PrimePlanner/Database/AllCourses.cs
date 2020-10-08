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
    class AllCourses
    {
        private static ObservableCollection<Course> allCourses = new ObservableCollection<Course>();
        public async static void InitializeDatabase()
        {
            await ApplicationData.Current.LocalFolder.CreateFileAsync("primePlanner.db", CreationCollisionOption.OpenIfExists);
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "primePlanner.db");
            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                String tableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS Courses (ID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, " +
                    "CourseName TEXT NOT NULL," +
                    "Grade TEXT NOT NULL," +
                    "Credits TEXT NOT NULL)";

                SqliteCommand createTable = new SqliteCommand(tableCommand, db);

                createTable.ExecuteReader();
            }
        }

        // Retrieve all data from database
        public static ObservableCollection<Course> RetrieveFromDB()
        {
            allCourses.Clear();
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "primePlanner.db");
            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT * from Courses", db);

                SqliteDataReader query = selectCommand.ExecuteReader();
                while (query.Read())
                {
                    int ID = Int32.Parse(query.GetString(0));
                    string courseName = query.GetString(1);
                    string grade = query.GetString(2);
                    string credits = query.GetString(3);
                    allCourses.Add(new Course
                    {
                        ID = ID,
                        CourseName = courseName,
                        Grade = grade,
                        Credits = credits
                    });
                }
                db.Close();
            }
            return allCourses;
        }


        // Insert data into Database
        public static void AddData(string courseName, string grade, string credit)
        {
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "primePlanner.db");
            using (SqliteConnection db =
              new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                insertCommand.CommandText = "INSERT INTO Courses VALUES (NULL, @CourseName, @Grade, @Credits);";

                insertCommand.Parameters.AddWithValue("@CourseName", courseName);
                insertCommand.Parameters.AddWithValue("@Grade", grade);
                insertCommand.Parameters.AddWithValue("@Credits", credit);

                insertCommand.ExecuteReader();

                db.Close();
            }
        }


        // Updates values for a specific record in Database
        public static void EditCourseDB(string ID, string courseName, string grade, string credit)
        {
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "primePlanner.db");
            using (SqliteConnection db =
              new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                insertCommand.CommandText = "UPDATE Courses SET CourseName = @CourseName, Grade = @Grade, Credits = @Credits WHERE ID = @ID;";

                insertCommand.Parameters.AddWithValue("@ID", ID);
                insertCommand.Parameters.AddWithValue("@CourseName", courseName);
                insertCommand.Parameters.AddWithValue("@Grade", grade);
                insertCommand.Parameters.AddWithValue("@Credits", credit);

                insertCommand.ExecuteReader();

                db.Close();
            }
        }

        public static void DeleteCourseDB(string ID)
        {
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "primePlanner.db");
            using (SqliteConnection db =
              new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                insertCommand.CommandText = "DELETE FROM Courses WHERE ID = @ID;";
                insertCommand.Parameters.AddWithValue("@ID", ID);
                insertCommand.ExecuteReader();

                db.Close();
            }
        }
    }
}
