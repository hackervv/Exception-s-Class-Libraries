using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Exception_ClassLibrary
{
    public class DBHelper
    {
        public static DataTable GetTable(string strSQL,string strConn)
        {
            return GetTable(strSQL,null,strConn);
        }

        public static DataTable GetTable(string strSQL,SqlParameter[] pas,string strConn)
        {
            return GetTable(strSQL, pas, CommandType.Text, strConn);
        }

        /// <summary>
        /// 执行查询，返回datatable对象
        /// </summary>
        /// <param name="strSQL">sql语句</param>
        /// <param name="pas">参数组合</param>
        /// <param name="cmdtype">command类型</param>
        /// <param name="strConn">数据库连接字符串</param>
        /// <returns>DataTable 对象</returns>
        public static DataTable GetTable(string strSQL,SqlParameter[] pas,CommandType cmdtype,string strConn)
        {
            DataTable dt = new DataTable();
            using(SqlConnection conn = new SqlConnection(strConn))
            {
                SqlDataAdapter da = new SqlDataAdapter(strConn, conn);
                da.SelectCommand.CommandType = cmdtype;
                if(pas != null)
                {
                    da.SelectCommand.Parameters.AddRange(pas);
                }
                da.Fill(dt);
            }
            return dt;
        }

        public static int ExcuteSQL(string strSQL,string strConn)
        {
            return ExcuteSQL(strSQL, null, strConn);
        }

        public static int ExcuteSQL(string strSQL,SqlParameter[] paras,string strConn)
        {
            return ExcuteSQL(strSQL, paras, CommandType.Text, strConn);
        }

        /// <summary>
        /// 执行非查询储存过程和SQL语句
        /// </summary>
        /// <param name="strSQL">要执行的sql</param>
        /// <param name="paras">参数列表，没有参数传入null</param>
        /// <param name="cmdType">commandtype类型</param>
        /// <param name="strConn"></param>
        /// <returns>返回影响的条数</returns>
        public static int ExcuteSQL(string strSQL,SqlParameter[] paras,CommandType cmdType,string strConn)
        {
            int i = 0;
            using(SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand cmd = new SqlCommand(strSQL, conn);
                cmd.CommandType = cmdType;
                if (paras != null)
                {
                    cmd.Parameters.AddRange(paras);
                }
                conn.Open();
                i = cmd.ExecuteNonQuery();
                conn.Close();
            }
            return i;
        }

        /// <summary>
        /// datable结构数据映射到数据库对应的表中
        /// </summary>
        /// <param name="sourceDt"></param>
        /// <param name="targetTable"></param>
        /// <param name="strConn"></param>
        /// <returns></returns>
        public static bool BuckToDB(DataTable sourceDt,string targetTable,string strConn)
        {
            bool result = false;
            SqlConnection conn = new SqlConnection(strConn);
            conn.Open();
            SqlTransaction transaction = conn.BeginTransaction();
            SqlBulkCopy bulkCopy = new SqlBulkCopy(conn, SqlBulkCopyOptions.KeepNulls, transaction);

            bulkCopy.DestinationTableName = targetTable;
            bulkCopy.BatchSize = sourceDt.Rows.Count;

            try
            {
                if(sourceDt != null && sourceDt.Rows.Count > 0)
                {
                    bulkCopy.WriteToServer(sourceDt);
                    transaction.Commit();
                }
                result = true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                result = false;
            }
            finally
            {
                conn.Close();
                if(bulkCopy != null)
                {
                    bulkCopy.Close();
                }

            }
            return result;
        }
    }
}
