namespace DataAccess
{
    public interface ISQLiteDataAccess
    {

        Task<IEnumerable<T>> GetAllById<T>(string table, string condition, string connectionId = "Default");
        Task<IEnumerable<T>> GetAll<T>(string Table, string connectionId = "Default");
        Task<bool> Insert<T>(string table, string columns, string values, T parameters, string connectionId = "Default");
        Task<IEnumerable<T>> Insert<T, U>(string table, string columns, string values, U parameters, string connectionId = "Default");
        Task<bool> Update<T>(string table, string columns, string WhereLogic, T parameters, string connectionId = "Default");
        Task<bool> Delete<T>(string table, string columns, string WhereLogic, T parameters, string connectionId = "Default");
        Task<bool> SQLTask<T>(string SQL, T parameters, string connectionId = "Default");
        Task<IEnumerable<T>> SQLQuery<T, U>(string SQL, U parameters, string connectionId = "Default");

    }
}