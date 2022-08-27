using System;
using System.Collections.Generic;
using System.Data.SqlClient;
namespace MessagerServer
{
    static class MDatabaseOperator
    {
        static SqlConnection _conn { set; get; }
        static public bool ConnectDatabase(String connetStr)
        {
            _conn = new SqlConnection(connetStr);
            try
            {
                _conn.Open();
                return true;
            }
            catch (Exception ex)
            {
                   throw new Exception(ex.ToString());
            }
        }
        static public bool DisconnectDatabase()
        {
            _conn.Close();
            //if (_conn.State == 0)
            //    return true;
            //else
            //    return false;
            return _conn.State == 0;
        }
        static public String QueryFirstOperation(string sqlstr)
        {
            SqlCommand cmd = new SqlCommand(sqlstr, _conn);
            Object result = cmd.ExecuteScalar();
            //if (result != null)
            //{
            //    return result.ToString();
            //}
            //else
            //{
            //    return null;
            //}
            return result == null ? null : result.ToString();


        }
        // todo
        static public int ExecuteOperation(string sqlstr)
        {
            SqlCommand cmd = new SqlCommand(sqlstr, _conn);
            return cmd.ExecuteNonQuery();
        }
        static public List<string> QueryOperation(string sqlstr,string arrayStr)
        {
            List<string> listStr;
            SqlCommand cmd = new SqlCommand(sqlstr, _conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                for(int i=0;i< arrayStr.Length;i++)
                {
                    //listStr.Add(reader.GetString());
                }
                
            }
            return null;
        }
    }
}
