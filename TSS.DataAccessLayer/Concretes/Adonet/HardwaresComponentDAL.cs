using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSS.DataLayer.Abstracts;
using TSS.Entities.Concretes;

namespace TSS.DataLayer.Concretes.Adonet
{
    public class HardwaresComponentDAL : IHardwaresComponentService
    {

        TSS.DataLayer.Concretes.AdonetManagerObj.SQLiteObj sqliteobj = new TSS.DataLayer.Concretes.AdonetManagerObj.SQLiteObj();
        string tableName = "Donanım Komponentleri", dataBaseTableName = "Hardwares_Component";

        public void connection()
        {
            if (sqliteobj.Connection.State == System.Data.ConnectionState.Closed)
            {
                sqliteobj.Connection.Open();
            }
        }

        public void AddEntities(Hardwares_Component Tentities)
        {
            TSS.Tools.TryManager.Run(() =>
             {
                 connection();
                 sqliteobj.Command.CommandText = "INSERT INTO " + dataBaseTableName + "(HardwareID,ComponentID,Description)VALUES(@hid,@cid,@des)";
                 sqliteobj.Command.Connection = sqliteobj.Connection;

                 DbParameter nameParametre = sqliteobj.Command.CreateParameter();
                 nameParametre.ParameterName = "@hid";
                 nameParametre.Value = Tentities.HardwaresID;
                 sqliteobj.Command.Parameters.Add(nameParametre);

                 DbParameter lnameParametre = sqliteobj.Command.CreateParameter();
                 lnameParametre.ParameterName = "@cid";
                 lnameParametre.Value = Tentities.ComponentID;
                 sqliteobj.Command.Parameters.Add(lnameParametre);

                 DbParameter addressParametre = sqliteobj.Command.CreateParameter();
                 addressParametre.ParameterName = "@des";
                 addressParametre.Value = Tentities.Description;
                 sqliteobj.Command.Parameters.Add(addressParametre);

                 sqliteobj.Command.ExecuteNonQuery();
                 sqliteobj.Connection.Close();
             });
        }

        public void DeleteEntities(int id)
        {
            TSS.Tools.TryManager.Run(() =>
            {
                connection();
                string mySelectQuery = "DELETE FROM " + dataBaseTableName + " WHERE ID=@id";
                sqliteobj.Command.CommandText = mySelectQuery;
                sqliteobj.Command.Connection = sqliteobj.Connection;

                DbParameter idParametre = sqliteobj.Command.CreateParameter();
                idParametre.ParameterName = "@id";
                idParametre.Value = id;
                sqliteobj.Command.Parameters.Add(idParametre);

                sqliteobj.Command.ExecuteNonQuery();
                sqliteobj.Connection.Close();
            });
        }

        public List<Hardwares_Component> GetAllEntities()
        {
            List<Hardwares_Component> returnedList = new List<Hardwares_Component>();
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
                            Hardwares_Component hComponent = new Hardwares_Component();
                            hComponent.ID = Reader.GetInt32(0);
                            hComponent.HardwaresID = Reader.GetInt32(1);
                            hComponent.ComponentID = Reader.GetInt32(2);
                            returnedList.Add(hComponent);
                        }
                    }
                    Reader.Close();

                }
                sqliteobj.Connection.Close();
            });
            return returnedList;
        }

        public Hardwares_Component getEntitie(int id)
        {
            Hardwares_Component returnData = new Hardwares_Component();
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
                            Hardwares_Component hComponent = new Hardwares_Component();
                            hComponent.ID = Reader.GetInt32(0);
                            hComponent.HardwaresID = Reader.GetInt32(1);
                            hComponent.ComponentID = Reader.GetInt32(2);
                            returnData = hComponent;
                        }

                    }
                    Reader.Close();

                }
                sqliteobj.Connection.Close();

            });

            return returnData;
        }

        public Hardwares_Component getEntitie(string data)
        {
            throw new NotImplementedException();
        }

        public List<Hardwares_Component> getEntities(string data)
        {
            // 
            List<Hardwares_Component> returnedList = new List<Hardwares_Component>();
            TSS.Tools.TryManager.Run(() =>
            {
                connection();
                sqliteobj.Command.CommandText = "SELECT Hardwares_Component.ID HardComId, Components.Name CompNam, Hardwares_Component.Description HardCompDes,Components.ID CompId, Components_Type.Name CompTypeName, Components.Description CompDesFROM Hardwares_Component INNER JOIN Components ON Components.ID = Hardwares_Component.ComponentID INNER JOIN Components_Type ON Components_Type.ID = Components.ID WHERE Hardwares_Component.HardwareID = 11 GROUP BY Components.Name, Components_Type.Name, Components.Description ORDER BY Hardwares_Component.ID ASC;";

                DbParameter nameParametre = sqliteobj.Command.CreateParameter();
                nameParametre.ParameterName = "@id";
                nameParametre.Value = Convert.ToInt32(data);
                sqliteobj.Command.Parameters.Add(nameParametre);

                sqliteobj.Command.Connection = sqliteobj.Connection;
                using (var Reader = sqliteobj.Command.ExecuteReader())
                {
                    if (Reader.HasRows)
                    {
                        while (Reader.Read())
                        {
                            Hardwares_Component hComponent = new Hardwares_Component();
                            hComponent.ID = Reader.GetInt32(0);
                            hComponent.HardwaresID = Convert.ToInt32(data) ;
                            hComponent.ComponentID = Reader.GetInt32(0);
                            returnedList.Add(hComponent);
                        }
                    }
                    Reader.Close();

                }
                sqliteobj.Connection.Close();
            });
            return returnedList;
        }

        public void UpdateEntities(int id, Hardwares_Component TEntities)
        {
            connection();
            sqliteobj.Command.CommandText = "UPDATE  " + dataBaseTableName + " SET HardwareID=@hid,ComponentID=@cid,Description=@des WHERE ID=@id";
            sqliteobj.Command.Connection = sqliteobj.Connection;

            DbParameter nameParametre = sqliteobj.Command.CreateParameter();
            nameParametre.ParameterName = "@hid";
            nameParametre.Value = TEntities.HardwaresID;
            sqliteobj.Command.Parameters.Add(nameParametre);

            DbParameter lnameParametre = sqliteobj.Command.CreateParameter();
            lnameParametre.ParameterName = "@cid";
            lnameParametre.Value = TEntities.ComponentID;
            sqliteobj.Command.Parameters.Add(lnameParametre);

            DbParameter addressParametre = sqliteobj.Command.CreateParameter();
            addressParametre.ParameterName = "@des";
            addressParametre.Value = TEntities.Description;
            sqliteobj.Command.Parameters.Add(addressParametre);

            DbParameter idParametre = sqliteobj.Command.CreateParameter();
            idParametre.ParameterName = "@id";
            idParametre.Value = TEntities.ID;
            sqliteobj.Command.Parameters.Add(idParametre);

            sqliteobj.Command.ExecuteNonQuery();
            sqliteobj.Connection.Close();
        }
    }
}
