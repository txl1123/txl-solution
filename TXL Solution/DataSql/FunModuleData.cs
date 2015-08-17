using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using TxlMvc.Helper;
using TxlMvc.Models;
namespace TxlMvc
{
  public   class FunModuleData
    {
        #region select
        /// <summary>
        /// 
        /// </summary>
        /// <param name="top"></param>
        /// <param name="content">需要的列名</param>
        /// <param name="condition">条件</param>
        /// <returns></returns>
      public List<FunModuleShip> select(int top, string content, string condition, string orderby)
        {
            string sql;
            sql = string.Concat("select",
                top > 0 ? " top " + top : string.Empty, string.IsNullOrEmpty(content) ? " id,FunModule.fid,mid,funname " : content
                , "  from [Tempship] left join [FunModule] on funmodule.fid=tempship.fid ",
                string.IsNullOrEmpty(condition) ? string.Empty : " where " + condition,
                string.IsNullOrEmpty(orderby) ? string.Empty : " order by " + orderby);

            DataSet dts = new DataSet();
            dts = SqlHelper.ExecuteDataSetText(sql, null);
            List<FunModuleShip> lst = SqlHelper.DataSetToIList<FunModuleShip>(dts, 0).ToList();
            return lst;
        }

        public DataSet select(int top, string content, string condition)
        {
            string sql;
            sql = string.Concat("select",
                top > 0 ? " top " + top : string.Empty, string.IsNullOrEmpty(content) ? " id,FunModule.fid,mid,funname " : content
                , "  from [Tempship] left join [FunModule] on funmodule.fid=tempship.fid ",
                string.IsNullOrEmpty(condition) ? string.Empty : " where " + condition);
            DataSet ds = new DataSet();
            ds = SqlHelper.ExecuteDataSetText(sql, null);
            return ds;
        }
        #endregion
    }
}
