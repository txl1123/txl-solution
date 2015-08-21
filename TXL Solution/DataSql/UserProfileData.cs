using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using TxlMvc.Helper;
using TxlMvc.Models;
using System.Reflection;
namespace TxlMvc
{
    public class UserProfileData
    {
        #region insert
        public static  int insert( string UserName, string Password, int? Roleid)
        {
            string sql;
            int reval;
            sql = "insert into UserProfile(username,password,roleid) values (@username,@password,@roleid);";
            SqlParameter[] para = new SqlParameter[]{
                
               
                new SqlParameter("@username",SqlDbType.VarChar,200),
                new SqlParameter("@password",SqlDbType.VarChar,200),
                new SqlParameter("@roleid",SqlDbType.Int)
            };
            para[0].Value = UserName;
            para[1].Value = Password;
            para[2].Value = Roleid;
      
            reval = SqlHelper.ExecteNonQueryText(sql, para);
            return reval;
        }

        public static int insert(UserProfile users)
        {
            return insert( users.UserName, users.Password, users.RoleId);
        }
        #endregion

        #region update
        public static int update(int? UserId, string UserName, string Password, int? Roleid)
        {
            string sql;
            int reval;
            sql = "update UserProfile set userid=@userid,username=@username,password=@password,roleid=@roleid";
            SqlParameter[] para = new SqlParameter[]{
                
                new SqlParameter("@userid",SqlDbType.Int),
                new SqlParameter("@username",SqlDbType.VarChar,200),
                new SqlParameter("@password",SqlDbType.VarChar,200),
                new SqlParameter("@roleid",SqlDbType.Int)
            };
            para[0].Value = UserId;
            para[1].Value = UserName;
            para[2].Value = Password;
            para[3].Value = Roleid;
            reval = SqlHelper.ExecteNonQueryText(sql, para);
            return reval;
        }

        public static  int update(UserProfile users)
        {
            return update(users.UserId, users.UserName, users.Password, users.RoleId);
        }
        #endregion

        #region select
        /// <summary>
        /// 
        /// </summary>
        /// <param name="top"></param>
        /// <param name="content">需要的列名</param>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public List<UserProfile> select(int top, string content, string condition, string orderby)
        {
            string sql;
            sql = string.Concat("select",
                top > 0 ? " top " + top : string.Empty, string.IsNullOrEmpty(content) ? " * " : content
                , "  from [UserProfile]",
                string.IsNullOrEmpty(condition) ? string.Empty : " where " + condition,
                string.IsNullOrEmpty(orderby) ? string.Empty : " order by " + orderby);

            DataSet dts = new DataSet();
            dts = SqlHelper.ExecuteDataSetText(sql, null);
            List<UserProfile> lst =SqlHelper.DataSetToIList<UserProfile>(dts, 0).ToList();
            return lst;
        }

        public DataSet select(int top, string content, string condition)
        {
            string sql;
            sql = string.Concat("select",
                top > 0 ? " top " + top : string.Empty, string.IsNullOrEmpty(content) ? " * " : content
                , "  from [UserProfile]",
                string.IsNullOrEmpty(condition) ? string.Empty : " where " + condition);
            DataSet ds = new DataSet();
            ds = SqlHelper.ExecuteDataSetText(sql, null);
            return ds;
        }
        #endregion

        #region Login
        public static bool CheckLogin(LoginModel lm)
        {
            int reval;
            string sql = "select * from UserProfile where username=" + lm.UserName + " and password=" + lm.Password;
            reval = SqlHelper.ExecteNonQueryText(sql, null);
            if (reval > 0)
            {
                return true;
            }
            else return false;
        }
        #endregion
    }
}
