using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TSS.DataLayer.Abstracts;
using TSS.Entities.Concretes;

namespace TSS.DataLayer.Concretes.Dapper
{
    public class HardwaresComponentDAL : IHardwaresComponentService
    {
        public IDbConnection GetConnection()
        {
            return new SQLiteConnection("datasource=" + Application.StartupPath + "\\DataBase\\TechnicalService.db");
        }

        public void AddEntities(Hardwares_Component Tentities)
        {
            GetConnection().Execute("INSERT INTO Hardwares_Component(HardwareID,ComponentID,Description)VALUES(@HardwaresID,@ComponentID,@Description)", Tentities);
        }

        public void DeleteEntities(int id)
        {
            GetConnection().Execute("DELETE FROM Hardwares_Component WHERE ID=@id", id);
        }

        public List<Hardwares_Component> GetAllEntities()
        {
            string query = @"SELECT Components.name,Components.Description,Components_Type.name
from Hardwares_Component
INNER JOIN Components ON Components.ID = Hardwares_Component.ComponentID
INNER JOIN Components_Type ON Components_Type.ID = Components.ID
WHERE Hardwares_Component.HardwareID = 11
GROUP BY Components.name,Components_Type.name,components.Description";
            return GetConnection().Query<TSS.Entities.Concretes.Hardwares_Component>(query).ToList();
        }

        public void UpdateEntities(int id, Hardwares_Component Tentities)
        {
            GetConnection().Execute("INSERT INTO Hardwares_Component(Drive,Description)VALUES(@drive,@description)", Tentities);
        }
    }
}
