using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Common;
using TSS.Entities.Concretes;
using TSS.DataLayer.Abstracts;

namespace TSS.DataLayer.Concretes.Adonet
{
    public class ComponentDAL : IComponentServices
    {
        TSS.DataLayer.Concretes.AdonetManagerObj.SQLiteObj sqliteobj = new TSS.DataLayer.Concretes.AdonetManagerObj.SQLiteObj();
        string tableName = "Elemanlar", dataBaseTableName = "Components";
        public ComponentDAL()
        {
            
        }
        public void connection()
        {
            if (sqliteobj.Connection.State == System.Data.ConnectionState.Closed)
            {
                sqliteobj.Connection.Open();
            }
        }

        public List<Components> GetAllEntities()
        {
            List<Components> returnedList = new List<Components>();
            TSS.Tools.TryManager.Run(() =>
            {
                connection();
                sqliteobj.Command.CommandText = "SELECT * FROM " + dataBaseTableName + " ORDER BY ID ASC";
                sqliteobj.Command.Connection = sqliteobj.Connection;
                using (var Reader = sqliteobj.Command.ExecuteReader())
                {
                    if (Reader.HasRows)
                    {
                        while (Reader.Read())
                        {
                            Components component = new Components();
                            component.ID = Reader.GetInt32(0);
                            component.Name = Reader.GetString(1);
                            component.Description = Reader.GetString(2);
                            component.ComponentTypeId = Reader.GetInt32(3);
                            returnedList.Add(component);
                        }
                    }
                    Reader.Close();
                }
                sqliteobj.Connection.Close();
            });
            return returnedList;
        }

        public List<Components> getEntities(string data)
        {
            List<Components> returnedList = new List<Components>();
            TSS.Tools.TryManager.Run(() =>
            {
                connection();
                sqliteobj.Command.CommandText = "SELECT * FROM " + dataBaseTableName + " WHERE Name LIKE @aranan";

                DbParameter parametre = sqliteobj.Command.CreateParameter();
                parametre.ParameterName = "@aranan";
                parametre.Value = '%' + data + '%';
                sqliteobj.Command.Parameters.Add(parametre);

                sqliteobj.Command.Connection = sqliteobj.Connection;
                using (var Reader = sqliteobj.Command.ExecuteReader())
                {
                    if (Reader.HasRows)
                    {
                        while (Reader.Read())
                        {
                            Components component = new Components();
                            component.ID = Reader.GetInt32(0);
                            component.Name = Reader.GetString(1);
                            component.Description = Reader.GetString(2);
                            component.ComponentTypeId = Reader.GetInt32(3);

                            returnedList.Add(component);
                        }
                    }
                    Reader.Close();
                }
                sqliteobj.Connection.Close();
            });
            return returnedList;
        }

        public Components getEntitie(int id)
        {
            Components returnData = new Components();
            TSS.Tools.TryManager.Run(() =>
            {
                connection();
                sqliteobj.Command.CommandText = "SELECT * FROM " + dataBaseTableName + " WHERE ID=@id";
                sqliteobj.Command.Connection = sqliteobj.Connection;

                DbParameter parametre = sqliteobj.Command.CreateParameter();
                parametre.ParameterName = "@id";
                parametre.Value = id;
                sqliteobj.Command.Parameters.Add(parametre);

                using (var Reader = sqliteobj.Command.ExecuteReader())
                {
                    if (Reader.HasRows)
                    {
                        while (Reader.Read())
                        {
                            Components component = new Components();
                            component.ID = Reader.GetInt32(0);
                            component.Name = Reader.GetString(1);
                            component.Description = Reader.GetString(2);
                            component.ComponentTypeId = Reader.GetInt32(3);
                            returnData = component;
                        }
                    }
                    Reader.Close();
                }
                sqliteobj.Connection.Close();
            });
            return returnData;
        }

        public Components getEntitie(string data)
        {
            Components returnData = new Components();
            TSS.Tools.TryManager.Run(() =>
            {
                connection();
                sqliteobj.Command.CommandText = "SELECT * FROM " + dataBaseTableName + " WHERE Name LIKE @aranan";
                sqliteobj.Command.Connection = sqliteobj.Connection;

                DbParameter parametre = sqliteobj.Command.CreateParameter();
                parametre.ParameterName = "@aranan";
                parametre.Value = '%' + data + '%';
                sqliteobj.Command.Parameters.Add(parametre);

                using (var Reader = sqliteobj.Command.ExecuteReader())
                {
                    if (Reader.HasRows)
                    {
                        while (Reader.Read())
                        {
                            Components component = new Components(); //Description,Customer,Interested
                            component.ID = Reader.GetInt32(0);
                            component.Name = Reader.GetString(1);
                            component.Description = Reader.GetString(2);
                            component.ComponentTypeId = Reader.GetInt32(3);
                            returnData = component;
                        }
                    }
                    Reader.Close();
                }
                sqliteobj.Connection.Close();
            });
            return returnData;
        }

        public void DeleteEntities(int id)
        {
            TSS.Tools.TryManager.Run(() =>
            {
                connection();
                sqliteobj.Command.CommandText = "DELETE FROM " + dataBaseTableName + " WHERE ID=@id";
                sqliteobj.Command.Connection = sqliteobj.Connection;

                DbParameter idParametre = sqliteobj.Command.CreateParameter();
                idParametre.ParameterName = "@id";
                idParametre.Value = id;
                sqliteobj.Command.Parameters.Add(idParametre);

                sqliteobj.Command.ExecuteNonQuery();
                sqliteobj.Connection.Close();
            });

        }

        public void UpdateEntities(int id, Components TEntities)
        {
            TSS.Tools.TryManager.Run(() =>
            {
                connection();
                sqliteobj.Command.CommandText = "UPDATE " + dataBaseTableName + " SET Name=@name,Description=@des,ComponentTypeID=@CtID WHERE ID=@id";
                sqliteobj.Command.Connection = sqliteobj.Connection;

                DbParameter idParametre = sqliteobj.Command.CreateParameter();
                idParametre.ParameterName = "@id";
                idParametre.Value = id;
                sqliteobj.Command.Parameters.Add(idParametre);

                DbParameter nameParametre = sqliteobj.Command.CreateParameter();
                nameParametre.ParameterName = "@name";
                nameParametre.Value = TEntities.Name;
                sqliteobj.Command.Parameters.Add(nameParametre);

                DbParameter desParametre = sqliteobj.Command.CreateParameter();
                desParametre.ParameterName = "@des";
                desParametre.Value = TEntities.Description;
                sqliteobj.Command.Parameters.Add(desParametre);

                DbParameter CtIDParametre = sqliteobj.Command.CreateParameter();
                CtIDParametre.ParameterName = "@CtID";
                CtIDParametre.Value = TEntities.ComponentTypeId;
                sqliteobj.Command.Parameters.Add(CtIDParametre);

                sqliteobj.Command.ExecuteNonQuery();
                sqliteobj.Connection.Close();
            });
        }

        public void AddEntities(Components Tentities)
        {
            TSS.Tools.TryManager.Run(() =>
            {

                connection();
                sqliteobj.Command.CommandText = "INSERT INTO " + dataBaseTableName + "(Name,Description,ComponentTypeID)VALUES(@name,@des,@CtID)";
                sqliteobj.Command.Connection = sqliteobj.Connection;

                DbParameter nameParametre = sqliteobj.Command.CreateParameter();
                nameParametre.ParameterName = "@name";
                nameParametre.Value = Tentities.Name;
                sqliteobj.Command.Parameters.Add(nameParametre);

                DbParameter desParametre = sqliteobj.Command.CreateParameter();
                desParametre.ParameterName = "@des";
                desParametre.Value = Tentities.Description;
                sqliteobj.Command.Parameters.Add(desParametre);

                DbParameter CtIDParametre = sqliteobj.Command.CreateParameter();
                CtIDParametre.ParameterName = "@CtID";
                CtIDParametre.Value = Tentities.ComponentTypeId;
                sqliteobj.Command.Parameters.Add(CtIDParametre);

                sqliteobj.Command.ExecuteNonQuery();
                sqliteobj.Connection.Close();
            });
        }
    }
}
