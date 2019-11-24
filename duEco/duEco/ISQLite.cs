using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace duEco
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
        SQLiteAsyncConnection GetConnectionAsync();
    }
}
