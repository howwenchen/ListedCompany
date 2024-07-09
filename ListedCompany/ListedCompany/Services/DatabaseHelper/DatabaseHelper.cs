using Microsoft.Data.SqlClient;
using System.Data;

namespace ListedCompany.Services.DatabaseHelper
{
    /// <summary>
    /// DatabaseHelper
    /// </summary>
    public class DatabaseHelper : IDatabaseHelper
    {
        private readonly string _connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseHelper"/> class.
        /// </summary>
        /// <param name="connectionString">The database connection string.</param>
        public DatabaseHelper(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        /// <summary>
        /// 取得連線
        /// </summary>
        /// <returns>開啟的資料庫連線</returns>
        public IDbConnection GetConnection()
        {
            var conn = new SqlConnection(_connectionString);
            conn.Open();
            return conn;
        }
    }
}
