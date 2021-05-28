using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace NoteData
{
    public class NoteRepository
    {
        string dbName = @"Test1.db";
        string dbConnection = "Provider=SQLOLEDB.1;Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=ToDoItems;Data Source=DESKTOP-2AS0J7C\\SQLEXPRESS"; // "binaryguid=False" is essential for doing find-by-id if id is guid
                                                                                                                                                                            // Creating a query string with a parameter placeholder 
        string queryString = "SELECT Description, Category from dbo.ToDoItems"
                                + "WHERE Status > @Completed "
                                + "ORDER BY Date DESC;";
        SQLiteConnection conn = new SQLiteConnection();

        SQLiteCommand cmd;

        public NoteRepository()
        {
            conn = new SQLiteConnection(string.Format(dbConnection, dbName));
            conn.Open();
        }

        public void DeleteAll()
        {
            // Delete all records
            // delete from Notes
            string sqlDel = string.Format("delete from TodoItems");
            cmd = new SQLiteCommand(sqlDel, conn);
            cmd.ExecuteNonQuery();
        }

        public void Add(string title, string noteText)
        {
            // Add a note record:
            string sqlIns = string.Format("insert into TodoItems(NoteDate, Title, NoteText) VALUES ('{0}', '{1}', '{2}')",
              DateTime.Now, title, noteText);
            cmd = new SQLiteCommand(sqlIns, conn);
            cmd.ExecuteNonQuery();
        }


        public List<Note> GetAll()
        {
            // Read records via a SQL statement
            string sql = "select * from TodoItems order by ID";
            SQLiteDataAdapter da = new SQLiteDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            List<Note> notes = new List<Note>();
            foreach (DataRow dr in dt.Rows)
            {
                Note note = new Note();
                note.ID = Convert.ToInt32(dr["ID"]);
                note.NoteDate = Convert.ToDateTime(dr["NoteDate"]);
                note.NoteText = Convert.ToString(dr["NoteText"]);
                note.Title = Convert.ToString(dr["Title"]);
                notes.Add(note);
            }
            return notes;
        }

        public void Update(int id, string title, string noteText)
        {
            //string sqlUpd = string.Format("update Notes set Title = 'Code more' where Title = 'Laugh More' ");

            string SqUpd = string.Format("Update Notes set NoteDate = '{0}', title = '{1}', notetext = '{2}' where id = {3}",
              DateTime.Now, title, noteText, id);
            cmd = new SQLiteCommand(SqUpd, conn);
            cmd.ExecuteNonQuery();
        }

        private class TodoRepository
        {
            public static object tdr { get; internal set; }
        }
    }

}