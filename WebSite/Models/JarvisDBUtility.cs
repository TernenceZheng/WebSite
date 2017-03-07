using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PetaPoco;
using System.Data.SqlClient;

namespace WebSite.Models
{

    public class JarvisDBUtility
    {
        private Database _databaseContext;
        public enum ConnectionString
        {
            LocalDBConnStr,
        }

        private string _connectionString = "LocalDBConnStr";

        private ConnectionString _setConnStr;

        public ConnectionString SetConnectionString
        {
            get { return _setConnStr; }
            set
            {
                _setConnStr = value;
                SqlConnection conn = new SqlConnection("Data Source=PC-102;Initial Catalog=Northwind;Persist Security Info=True;User ID=sa;Password=2UauixdR;");

                   _databaseContext = new PetaPoco.Database(conn);

            }
        }


        public void BeginTransaction()
        {

            //var databaseContext = new ShareLib.DBPetaPoco.DataBase(_connectionString);

            _databaseContext.BeginTransaction();
        }

        public void Rollback()
        {
            //var databaseContext = new ShareLib.DBPetaPoco.DataBase(_connectionString);
            _databaseContext.AbortTransaction();
        }

        public void Commit()
        {
            //var databaseContext = new ShareLib.DBPetaPoco.DataBase(_connectionString);
            _databaseContext.CompleteTransaction();
        }


        /// <summary>
        /// 針對PetaPOCO所提供的ExecuteScalar方法再包一層
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public T ExecuteScalar<T>(string sql, params object[] args)
        {

                _databaseContext.EnableAutoSelect = false;

                return _databaseContext.ExecuteScalar<T>(sql, args);
        }


        /// <summary>
        /// 針對PetaPOCO所提供的Execute方法再包一層
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public int Execute(string sql, params object[] args)
        {
                _databaseContext.EnableAutoSelect = false;

                return _databaseContext.Execute(sql, args);

        }

        /// <summary>
        /// 針對PetaPOCO所提供的Query方法再包一層
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public IEnumerable<T> Query<T>(string sql)
        {

                _databaseContext.EnableAutoSelect = false;


                return _databaseContext.Query<T>(sql);

        }

        /// <summary>
        /// 針對PetaPOCO所提供的Query方法再包一層
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public IEnumerable<T> Query<T>(string sql, params object[] args)
        {

                _databaseContext.EnableAutoSelect = false;

                return _databaseContext.Query<T>(sql, args);

        }

        /// <summary>
        /// 針對PetaPOCO所提供的Query方法再包一層，JOIN TABLE數為3
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public IEnumerable<T> Query<T, T2, T3>(string sql, params object[] args)
        {

                return _databaseContext.Query<T, T2, T3>(sql, args);

        }

        /// <summary>
        /// 針對PetaPOCO所提供的Query方法再包一層，JOIN TABLE數為4
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public IEnumerable<T> Query<T, T2, T3, T4>(string sql, params object[] args)
        {

                return _databaseContext.Query<T, T2, T3, T4>(sql, args);

        }

        /// <summary>
        /// 針對PetaPOCO所提供的Update方法再包一層
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public int Update<T>(string sql, params object[] args)
        {

            return _databaseContext.Update<T>(sql, args);
        }

        public IEnumerable<T> Fetch<T>(string sql, params object[] args)
        {

                _databaseContext.EnableAutoSelect = false;

                return _databaseContext.Fetch<T>(sql, args);

        }

        /// <summary>
        /// 針對PetaPOCO所提供的SingleOrDefault方法再包一層(執行SP回傳單一行row可用此method)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public T SingleOrDefault<T>(string sql, params object[] args)
        {

                _databaseContext.EnableAutoSelect = false;

                return _databaseContext.SingleOrDefault<T>(sql, args);

        }

    }
}