using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.Data.SQLite;
using Models;

namespace DataAccess
{

    public class SQLiteDataAccess : ISQLiteDataAccess
    {
        private readonly IConfiguration configuration;

        public SQLiteDataAccess(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// Get all data from table where condition
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <param name="condition"></param>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetAllById<T>(string table, string condition, string connectionId = "Default")
        {
            using IDbConnection connection = new SQLiteConnection(configuration.GetConnectionString(connectionId));
            var result = await connection.QueryAsync<T>("SELECT * FROM " + table + " WHERE "+ condition, commandType: CommandType.Text);
            return result;
        }

        /// <summary>
        /// Get all the table data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Table"></param>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetAll<T>(string Table, string connectionId = "Default")
        {
            using IDbConnection connection = new SQLiteConnection(configuration.GetConnectionString(connectionId));
            var result = await connection.QueryAsync<T>("SELECT * FROM "+Table, commandType: CommandType.Text);
            return result;
        }

        /// <summary>
        /// Insert values into a table
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <param name="columns"></param>
        /// <param name="values"></param>
        /// <param name="parameters"></param>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        public async Task<bool> Insert<T>(string table, string columns, string values, T parameters, string connectionId = "Default")
        {
            using IDbConnection connection = new SQLiteConnection(configuration.GetConnectionString(connectionId));

            bool result = false;
            if ((await connection.ExecuteAsync("INSERT INTO " + table + " (" + columns + ") " +
                "VALUES(" + values + "); ", parameters, commandType: CommandType.Text)) >0)
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Insert values into a table
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <param name="columns"></param>
        /// <param name="values"></param>
        /// <param name="parameters"></param>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> Insert<T,U>(string table, string columns, string values, U parameters, string connectionId = "Default")
        {
            using IDbConnection connection = new SQLiteConnection(configuration.GetConnectionString(connectionId));
            await connection.QueryAsync("INSERT INTO " + table + " (" + columns + ") " +
                "VALUES(" + values + "); ", parameters, commandType: CommandType.Text);
            var result =await connection.QueryAsync<T>("SELECT * FROM " + table + " ORDER BY SpinId DESC LIMIT 1");
            return result;
        }

        /// <summary>
        /// Update a table
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <param name="columns"></param>
        /// <param name="WhereLogic"></param>
        /// <param name="parameters"></param>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        public async Task<bool> Update<T>(string table, string columns, string WhereLogic, T parameters, string connectionId = "Default")
        {
            using IDbConnection connection = new SQLiteConnection(configuration.GetConnectionString(connectionId));

            bool result = false;
            if ((await connection.ExecuteAsync("UPDATE " + table + " SET " + columns + " " +
                " WHERE " + WhereLogic + "; ", parameters, commandType: CommandType.Text)) > 0)
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Delete from a table
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <param name="columns"></param>
        /// <param name="WhereLogic"></param>
        /// <param name="parameters"></param>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        public async Task<bool> Delete<T>(string table, string columns, string WhereLogic, T parameters, string connectionId = "Default")
        {
            using IDbConnection connection = new SQLiteConnection(configuration.GetConnectionString(connectionId));

            bool result = false;
            if ((await connection.ExecuteAsync("DELETE FROM " + table +
                " WHERE " + WhereLogic + "; ", parameters, commandType: CommandType.Text)) > 0)
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Use for for unique querys not covered by CRUD that don't need to return data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="SQL"></param>
        /// <param name="parameters"></param>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        public async Task<bool> SQLTask<T>(string SQL, T parameters, string connectionId = "Default")
        {
            using IDbConnection connection = new SQLiteConnection(configuration.GetConnectionString(connectionId));

            bool result = false;
            if ((await connection.ExecuteAsync(SQL, parameters, commandType: CommandType.Text)) > 0)
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Use for for unique querys not covered by CRUD that return need to return data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="SQL"></param>
        /// <param name="parameters"></param>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> SQLQuery<T,U>(string SQL, U parameters, string connectionId = "Default")
        {
            using IDbConnection connection = new SQLiteConnection(configuration.GetConnectionString(connectionId));
            var result = await connection.QueryAsync<T>(SQL, parameters, commandType: CommandType.Text);
            return result;
        }

    }
}
