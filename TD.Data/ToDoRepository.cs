using System;
using System.Collections.Generic;
using System.Text;
using Model;
using System.Data;
using System.Data.SqlClient;
using TD.Model;
using TD.Data.Interface;

namespace TD.Data
{
    public class ToDoRepository
    {
        string dbConnection = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=ToDoItems;Data Source=DESKTOP-2AS0J7C\\SQLEXPRESS"; // "binaryguid=False" is essential for doing find-by-id if id is guid
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd;
        public ToDoRepository()
        {
            conn = new SqlConnection(dbConnection);
            conn.Open();
        }


        public List<ToDoItem> GetByDescription(string description)
        {
            //string sqlSel = $"select Description from ToDoItems where description like '%{description}%'";
            string crit = $"where description like '%{description}%'";
            return GetAll(crit);
        }

        public List<ToDoItem> getAll()
        {
            throw new NotImplementedException();
        }

        public List<ToDoItem> GetByDeadline(DateTime d)
        {
            string crit = $"where Deadline = '{d}'";
            return GetAll(crit);
        }

        public List<ToDoItem> GetByName(string name)
        {
            string crit = $"where Name = '{name}'";
            return GetAll(crit);
        }
        public List<ToDoItem> GetByStatus(ToDoStatus status)
        {
            string crit = $"where status = '{status}'";
            return GetAll(crit);
        }

        public List<ToDoItem> GetAll(string criteria = null)
        {
            try
            {
                string sql = "select * from TodoItems ";
                if (!string.IsNullOrEmpty(criteria))
                    sql += criteria;

                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                List<ToDoItem> lst = new List<ToDoItem>();
                foreach (DataRow dr in dt.Rows)
                {
                    ToDoItem t = new ToDoItem();
                    t.ID = (Guid)dr["ID"];
                    //t.ID = new Guid(dr["ID"].ToString());
                    t.Description = dr["Description"].ToString();
                    t.UserName = dr["Name"].ToString();
                    t.Category = dr["Category"].ToString();
                    ToDoStatus s = ToDoStatus.Unknown;
                    if (Enum.TryParse<ToDoStatus>(dr["Status"].ToString(), out s))
                        t.Status = s;
                    t.CreatedOn = Convert.ToDateTime(dr["CreatedOn"]);
                    t.Deadline = Convert.IsDBNull(dr["Deadline"]) ? null : (DateTime?)Convert.ToDateTime(dr["Deadline"]);
                    //if (Convert.IsDBNull(dr["Deadline"]))
                    //    t.Deadline = null;
                    //else
                    //    t.Deadline = (DateTime?)Convert.ToDateTime(dr["Deadline"]);
                             
                    lst.Add(t);
                }
                return lst;
            }
            catch (Exception ex)
            {
                // log the actual error
                return new List<ToDoItem>();
            }
        }
        public ToDoItem GetByID(Guid id)
        {
            try
            {
                string sql = $"select * from TodoItems where id = '{id}'";
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                ToDoItem t = new ToDoItem();
                DataRow dr = dt.Rows[0];

                t.ID = (Guid)dr["ID"];
                //t.ID = new Guid(dr["ID"].ToString());
                t.Description = dr["Description"].ToString();
                t.UserName = dr["Name"].ToString();
                t.Category = dr["Category"].ToString();
                ToDoStatus s = ToDoStatus.Unknown;
                if (Enum.TryParse<ToDoStatus>(dr["Status"].ToString(), out s))
                    t.Status = s;
                t.CreatedOn = Convert.ToDateTime(dr["Date"]);
                return t;
            }
            catch (Exception ex)
            {
                // log the actual error
                return null;
            }
        }

        public Boolean Exists(ToDoItem td)
        {
            string sql = $@"select count(id) from TodoItems where id='{td.ID}'";
            cmd = new SqlCommand(sql, conn);
            object n = cmd.ExecuteScalar();
            return (int)n > 0;
        }

        public void Update(ToDoItem td)
        {
            string sqlUpd = $@"UPDATE TodoItems 
                                SET
                                Description='{td.Description}',
                                CreatedOn = '{td.CreatedOn}', 
                                Name = '{td.UserName}',
                                Category = '{td.Category}',
                                Deadline = '{td.Deadline}'
                                where
                                ID ='{td.ID}'
                                ";
            cmd = new SqlCommand(sqlUpd, conn);
            cmd.ExecuteNonQuery();
        }
        public void Add(ToDoItem t)
        {
            string sqlIns = @$"insert into TodoItems
                                 (ID, Description, CreatedOn, name, status, Category, Deadline) 
                               VALUES 
                                 ('{t.ID}', '{t.Description}', '{t.CreatedOn}', '{t.UserName}', '{t.Status}', '{t.Category}', '{t.Deadline}')";
            cmd = new SqlCommand(sqlIns, conn);
            cmd.ExecuteNonQuery();
        }

        public void Delete(ToDoItem t)
        {
            string sqlDel = $"Delete from ToDoItems WHERE ID = '{t.ID}' ";
            cmd = new SqlCommand(sqlDel, conn);
            cmd.ExecuteNonQuery();
        }
        public void DeleteByStatus(ToDoStatus status)
        {
            string sqlDel = $"Delete from ToDoItems WHERE Status = '{status}' ";
            cmd = new SqlCommand(sqlDel, conn);
            cmd.ExecuteNonQuery();
        }

        public void DeleteByID(Guid id)
        {
            string sqlDel = $"Delete from ToDoItems WHERE ID = '{id}' ";
            cmd = new SqlCommand(sqlDel, conn);
            cmd.ExecuteNonQuery();
        }
        //delete entry, primary key or completed by author
        // DeleteByID(Guid id)
        // sql delete -delete from ToDoItems where id = 'C078993E-EA1A-4B84-985E-2E0897283CDA'

        //Adding update methods 
        public void UpdateStatusByDescription(ToDoStatus status, string desc)
        {
            string sqlUpd = $"UPDATE ToDoItems SET Status = 'In Progress' WHERE Description = 'Description of added item'";
            cmd = new SqlCommand(sqlUpd, conn);
            cmd.ExecuteNonQuery();
        }

        public void UpdateDescriptionById(Guid ID, string desc)
        {
            string sqlUpd = $"UPDATE ToDoItems SET Description = 'Mountain Skiing' WHERE Id = 'b5f19f9c-2b71-4363-8e1a-4283a0d3575a'";
            cmd = new SqlCommand(sqlUpd, conn);
            cmd.ExecuteNonQuery();
        }

        public List<ToDoItem> GetAll()
        {
            throw new NotImplementedException();
        }

        public void DeleteById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
