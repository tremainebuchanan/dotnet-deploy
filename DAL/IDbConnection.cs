using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Spendr.DAL.Interfaces
{
    public interface IDbConnection
    {
        MySqlConnection GetConnection();
    }
}