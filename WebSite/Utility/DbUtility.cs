using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AllPay.ShareLib;
using System.Data;
using PetaPoco;
using ShareLib.DBPetaPoco;

namespace WebSite.Utility
{
    public class DbUtility
    {
        private Database _databaseContext;
        private DataBase _databaseContextForAllPay;
        public enum ConnectionString
        {
            LocalDBConnStr,
            DBConnStr,
            AdminConnStr,
            DataLogConnStr,
            PaymentConnStr,
            ShareConnStr,
            PaymentDataLogConnStr,
            CoinsConnStr,
            SerialNoConnStr,
            PaymentCenterDBConnStr,
            AllPayConnStr,
            LogisticsConnStr,

            BetaAdminConnStr,
            BetaDataLogConnStr,
            BetaPaymentConnStr,
            BetaShareConnStr,
            BetaPaymentDataLogConnStr,
            BetaCoinsConnStr,
            BetaSerialNoConnStr,
            BetaPaymentCenterDBConnStr,
            BetaAllPayConnStr,
            BetaLogisticsConnStr,
            BetaDBConnStr,


            StageDBConnStr




        }

        private string _connectionString = "LocalDBConnStr";

        private ConnectionString _setConnStr;

        public ConnectionString SetConnectionString
        {
            get { return _setConnStr; }
            set
            {
                _setConnStr = value;

                switch (_setConnStr)
                {
                    case ConnectionString.AdminConnStr:
                        _connectionString = "AdminConnStr";
                        break;
                    case ConnectionString.DataLogConnStr:
                        _connectionString = "DataLogConnStr";
                        break;
                    case ConnectionString.PaymentConnStr:
                        _connectionString = "PaymentConnStr";
                        break;
                    case ConnectionString.ShareConnStr:
                        _connectionString = "ShareConnStr";
                        break;
                    case ConnectionString.PaymentDataLogConnStr:
                        _connectionString = "PaymentDataLogConnStr";
                        break;
                    case ConnectionString.CoinsConnStr:
                        _connectionString = "CoinsConnStr";
                        break;
                    case ConnectionString.SerialNoConnStr:
                        _connectionString = "SerialNoConnStr";
                        break;
                    case ConnectionString.PaymentCenterDBConnStr:
                        _connectionString = "PaymentCenterDBConnStr";
                        break;
                    case ConnectionString.AllPayConnStr:
                        _connectionString = "AllPayConnStr";
                        break;
                    case ConnectionString.LogisticsConnStr:
                        _connectionString = "LogisticsConnStr";
                        break;
                    case ConnectionString.DBConnStr:
                        _connectionString = "DBConnStr";
                        break;
                    case ConnectionString.LocalDBConnStr:
                        _connectionString = "LocalDBConnStr";
                        break;

                    case ConnectionString.BetaAdminConnStr:
                        _connectionString = "BetaAdminConnStr";
                        break;
                    case ConnectionString.BetaDataLogConnStr:
                        _connectionString = "BetaDataLogConnStr";
                        break;
                    case ConnectionString.BetaPaymentConnStr:
                        _connectionString = "BetaPaymentConnStr";
                        break;
                    case ConnectionString.BetaShareConnStr:
                        _connectionString = "BetaShareConnStr";
                        break;
                    case ConnectionString.BetaPaymentDataLogConnStr:
                        _connectionString = "BetaPaymentDataLogConnStr";
                        break;
                    case ConnectionString.BetaCoinsConnStr:
                        _connectionString = "BetaCoinsConnStr";
                        break;
                    case ConnectionString.BetaSerialNoConnStr:
                        _connectionString = "BetaSerialNoConnStr";
                        break;
                    case ConnectionString.BetaPaymentCenterDBConnStr:
                        _connectionString = "BetaPaymentCenterDBConnStr";
                        break;
                    case ConnectionString.BetaAllPayConnStr:
                        _connectionString = "BetaAllPayConnStr";
                        break;
                    case ConnectionString.BetaLogisticsConnStr:
                        _connectionString = "BetaLogisticsConnStr";
                        break;
                    case ConnectionString.BetaDBConnStr:
                        _connectionString = "BetaDBConnStr";
                        break;

                    default:
                        _connectionString = "LocalDBConnStr";
                        break;
                }

                //只要new 一個新的DataBase，就是另一條新的connection
                if (_connectionString == "LocalDBConnStr")
                {
                    _databaseContext = new PetaPoco.Database(_connectionString);
                }
                else
                {
                    _databaseContextForAllPay = new ShareLib.DBPetaPoco.DataBase(_connectionString);
                }

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
            // var databaseContext = new ShareLib.DBPetaPoco.DataBase(_connectionString);
            if (_connectionString == "LocalDBConnStr")
                _databaseContext.EnableAutoSelect = false;
            else//讓PetaPOCO的SELECT SQL不會自動產生table的欄位
                _databaseContextForAllPay.EnableAutoSelect = false;

            if (_connectionString == "LocalDBConnStr")
                return _databaseContext.ExecuteScalar<T>(sql, args);
            else
                return _databaseContextForAllPay.ExecuteScalar<T>(sql, args);
        }


        /// <summary>
        /// 針對PetaPOCO所提供的Execute方法再包一層
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public int Execute(string sql, params object[] args)
        {
            //var databaseContext = new ShareLib.DBPetaPoco.DataBase(_connectionString);
            if (_connectionString == "LocalDBConnStr")
                _databaseContext.EnableAutoSelect = false;
            else
                _databaseContextForAllPay.EnableAutoSelect = false;

            if (_connectionString == "LocalDBConnStr")
                return _databaseContext.Execute(sql, args);
            else
                return _databaseContextForAllPay.Execute(sql, args);
        }

        /// <summary>
        /// 針對PetaPOCO所提供的Query方法再包一層
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public IEnumerable<T> Query<T>(string sql)
        {

            //var databaseContext = new ShareLib.DBPetaPoco.DataBase(_connectionString);
            if (_connectionString == "LocalDBConnStr")
                _databaseContext.EnableAutoSelect = false;
            else
                _databaseContextForAllPay.EnableAutoSelect = false;
            if (_connectionString == "LocalDBConnStr")
                return _databaseContext.Query<T>(sql);
            else
                return _databaseContextForAllPay.Query<T>(sql);
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
            //var databaseContext = new ShareLib.DBPetaPoco.DataBase(_connectionString);
            if (_connectionString == "LocalDBConnStr")
                _databaseContext.EnableAutoSelect = false;
            else
                _databaseContextForAllPay.EnableAutoSelect = false;

            if (_connectionString == "LocalDBConnStr")
                return _databaseContext.Query<T>(sql, args);
            else
                return _databaseContextForAllPay.Query<T>(sql, args);
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
            //var databaseContext = new ShareLib.DBPetaPoco.DataBase(_connectionString);
            if (_connectionString == "LocalDBConnStr")
                return _databaseContext.Query<T, T2, T3>(sql, args);
            else
                return _databaseContextForAllPay.Query<T, T2, T3>(sql, args);
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
            //var databaseContext = new ShareLib.DBPetaPoco.DataBase(_connectionString);
            if (_connectionString == "LocalDBConnStr")
                return _databaseContext.Query<T, T2, T3, T4>(sql, args);
            else
                return _databaseContextForAllPay.Query<T, T2, T3, T4>(sql, args);
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

            //var databaseContext = new ShareLib.DBPetaPoco.DataBase(_connectionString);

            return _databaseContext.Update<T>(sql, args);
        }

        public IEnumerable<T> Fetch<T>(string sql, params object[] args)
        {
            //var databaseContext = new ShareLib.DBPetaPoco.DataBase(_connectionString);
            if (_connectionString == "LocalDBConnStr")
                _databaseContext.EnableAutoSelect = false;
            else
                _databaseContextForAllPay.EnableAutoSelect = false;
            if (_connectionString == "LocalDBConnStr")
                return _databaseContext.Fetch<T>(sql, args);
            else
                return _databaseContextForAllPay.Fetch<T>(sql, args);
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
            //var databaseContext = new ShareLib.DBPetaPoco.DataBase(_connectionString);
            if (_connectionString == "LocalDBConnStr")
                _databaseContext.EnableAutoSelect = false;
            else
                _databaseContextForAllPay.EnableAutoSelect = false;

            if (_connectionString == "LocalDBConnStr")
                return _databaseContext.SingleOrDefault<T>(sql, args);
            else
                return _databaseContextForAllPay.SingleOrDefault<T>(sql, args);
        }

    }
}