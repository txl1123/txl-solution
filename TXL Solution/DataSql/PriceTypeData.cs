using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using TxlMvc.Helper;
using TxlMvc.Models;
namespace TxlMvc
{
   public class PriceTypeData
    {
        #region select
        /// <summary>
        /// 
        /// </summary>
        /// <param name="top"></param>
        /// <param name="content">需要的列名</param>
        /// <param name="condition">条件</param>
        /// <returns></returns>
       public List<PriceType> select(int top, string content, string condition, string orderby)
        {
            string sql;
            sql = string.Concat("select",
                top > 0 ? " top " + top : string.Empty, string.IsNullOrEmpty(content) ? " * " : content
                , "  from [PriceType]",
                string.IsNullOrEmpty(condition) ? string.Empty : " where " + condition,
                string.IsNullOrEmpty(orderby) ? string.Empty : " order by " + orderby);

            DataSet dts = new DataSet();
            dts = SqlHelper.ExecuteDataSetText(sql, null);
            List<PriceType> lst = SqlHelper.DataSetToIList<PriceType>(dts,0).ToList();
            return lst;
        }

        public DataSet select(int top, string content, string condition)
        {
            string sql;
            sql = string.Concat("select",
                top > 0 ? " top " + top : string.Empty, string.IsNullOrEmpty(content) ? " * " : content
                , "  from [PriceType]",
                string.IsNullOrEmpty(condition) ? string.Empty : " where " + condition);
            DataSet ds = new DataSet();
            ds = SqlHelper.ExecuteDataSetText(sql, null);
            return ds;
        }
        #endregion
    }
}
