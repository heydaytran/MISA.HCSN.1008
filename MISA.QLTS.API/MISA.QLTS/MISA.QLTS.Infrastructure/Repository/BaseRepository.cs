using Dapper;
using MISA.QLTS.Core.Interfaces;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MISA.QLTS.Core.Entities;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MISA.QLTS.Infrastructure.Repository
{
    public class BaseRepository<T> : IBaseRepository<T>
    {
        //Tên của đối tượng
        protected string _tableName = string.Empty;

        //Chuỗi kết nối đến CSDL
        protected string _connectionString = "" +
               "Host=47.241.69.179; " +
               "Port=3306;" +
               "User Id= nvmanh; " +
               "Password=12345678;" +
               "Database= TEST.MISA.QLTS_copy; " +
                "convert zero datetime=True;";

        //protected string _connectionString = "" +
        //    "Host=47.241.69.179; " +
        //    "Port=3306;" +
        //    "User Id= dev; " +
        //    "Password=12345678;" +
        //    "Database= MF763_TQHuy_QLTS; " +
        //     "convert zero datetime=True;";


        //Khai báo kết nối
        protected IDbConnection _dbConnection;

        public BaseRepository()
        {
            _tableName = typeof(T).Name;
            _dbConnection = new MySqlConnection(_connectionString);
        }


        public IEnumerable<T> GetEntities()
        {
           // Thực hiện lấy dữ liệu từ Database
           var entities = _dbConnection.Query<T>(
               $"Proc_Get{_tableName}",
               commandType: CommandType.StoredProcedure);

            return entities;
        }

        public T GetById(Guid entityId)
        {
            // Thực hiện lấy thông tin một đối tượng

            var dynamicParameter = new DynamicParameters();
            dynamicParameter.Add($"@{_tableName}Id", entityId);
            var entity = _dbConnection.Query<T>($"Proc_Get{_tableName}ById", param: dynamicParameter, commandType: CommandType.StoredProcedure).FirstOrDefault();

            return entity;

        }

        public int Insert(T entity)
        {
            try
            {
                if (_dbConnection.State != ConnectionState.Open)
                    _dbConnection.Open();
                    var result = _dbConnection.Execute($"Proc_Insert{_tableName}", commandType: CommandType.StoredProcedure, param: entity);
                    return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }
        }

        public int Update(T entity)
        {
            // thực hiện cập nhật cơ sở dữ liệu
           
            try
            {
                if (_dbConnection.State != ConnectionState.Open)
                    _dbConnection.Open();

                using (var transaction = _dbConnection.BeginTransaction())
                {
                    var result = _dbConnection.Execute($"Proc_Update{typeof(T).Name}", commandType: CommandType.StoredProcedure, param: entity, transaction: transaction);
                    transaction.Commit();
                    return result;
                }
                //var result = _dbConnection.Execute($"Proc_Update{typeof(T).Name}", commandType: CommandType.StoredProcedure, param: entity);
                //return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }

        }


       
        public int Delete(Guid entityId)
        {
             // Thực hiện xóa dữ liệu từ Database:
            try
            {
                if(_dbConnection.State !=ConnectionState.Open) _dbConnection.Open();

                var dynamicProperty = new DynamicParameters();
                dynamicProperty.Add($"@{_tableName}Id", entityId);
                var rowEffects = _dbConnection.Execute($"Proc_Delete{_tableName}", commandType: CommandType.StoredProcedure, param: dynamicProperty);
                return rowEffects;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return -1;
            }

        }

    
        public PropertyInfo GetKeyProperty()
        {
            var keyProperty = typeof(T)
                .GetProperties()
                .Where(p => p.IsDefined(typeof(KeyAttribute), false))
                .FirstOrDefault();
            return keyProperty;
        }

        // hàm hủy kết nối
        public void Dispose()
        {
            _dbConnection.Dispose();
        }

       
    }
}
