﻿using System;

namespace InfluxBD
{
    /// <summary>
    /// InfluxDB时序数据库操作HTTP客户端
    /// 参考:https://www.cnblogs.com/dehai/p/4887309.html
    /// </summary>
    public class HttpInfluxDBClient
    {
        string _baseAddress;
        string _username;
        string _password;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="baseAddress"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public HttpInfluxDBClient(string baseAddress, string username, string password)
        {
            this._baseAddress = baseAddress;
            this._username = username;
            this._password = password;
        }

        public async System.Threading.Tasks.Task<string> CreateDatabaseAsync(string database)
        {
            string sql = "CREATE DATABASE " + database;
            string url = _baseAddress + string.Format("/query", "");
            string result = await HttpHelper.PostAsync(url, _username, _password, sql);
            return result;
        }


        public async System.Threading.Tasks.Task<string> DeleteDatabaseAsync(string database)
        {
            string sql = "DROP DATABASE " + database;
            string url = _baseAddress + string.Format("/query", "");
            string result = await HttpHelper.PostAsync(url, _username, _password, sql);
            return result;
        }


        public async System.Threading.Tasks.Task<string> GetDatabasesAsync()
        {
            string sql = "SHOW DATABASES";
            string url = _baseAddress + string.Format("/query?q={0}", sql);
            string result =await HttpHelper.GetAsync(url, _username, _password);
            return result;
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="database"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public async System.Threading.Tasks.Task<string> QueryAsync(string database, string sql)
        {
            string url = _baseAddress + string.Format("/query?db={0}&q={1}", database, sql);
            string result = await HttpHelper.GetAsync(url, _username, _password);
            return result;
        }

        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="database"></param>
        /// <param name="sql">示例:test,tag=logs Field0=10,Field1=10,Field2=20</param>
        /// <returns></returns>
        public async System.Threading.Tasks.Task<string> WriteAsync(string database, string sql)
        {
            string url = _baseAddress + string.Format("/write","");
            string result = await HttpHelper.PostAsync(url, database,_username, _password, sql);
            return result;
        }

        /// <summary>
        /// Ping服务是否能够打开
        /// </summary>
        /// <returns></returns>
        public async System.Threading.Tasks.Task<bool> PingAsync()
        {
            string url = _baseAddress + string.Format("/ping", "");
            string result = await HttpHelper.GetAsync(url, _username, _password);
            return string.IsNullOrEmpty(result);
        }
    }
}