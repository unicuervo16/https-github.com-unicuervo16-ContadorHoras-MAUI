using Contador_de_Horas.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Contador_de_Horas.Models;

namespace Contador_de_Horas.Services
{
    public class RegistroDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public RegistroDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<RegistroDiario>().Wait();
        }

        public Task<List<RegistroDiario>> GetRegistrosAsync() =>
            _database.Table<RegistroDiario>().ToListAsync();

        public Task<int> SaveRegistroAsync(RegistroDiario registro) =>
            _database.InsertOrReplaceAsync(registro);

        //public Task<int> EliminarTodo() =>
        // _database.DeleteAllAsync<RegistroDiario>();


    }

}

