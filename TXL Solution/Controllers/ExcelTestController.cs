using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NPOI;
using NPOI.HSSF.UserModel;
using System.IO;
using NPOI.SS.UserModel;
using TxlMvc.Models;
using System.Data.Entity;
using System.Data;
using NPOI.HPSF;
namespace TxlMvc.Controllers
{
    public class ExcelTestController : Controller
    {
        private MovieDbContext db = new MovieDbContext();
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 导出数据库数据到excel
        /// </summary>
        /// <returns></returns>
        public ActionResult ExportToExcel()
        {
            MemoryStream ms = WriteToStream();
            string fileName = "test.xls";
            return File(ms, "application/vnd.ms-excel", fileName);
        }

        MemoryStream WriteToStream()
        {
            MemoryStream ms = GenerateData();
            string fileName = @"c:\2.xls";
            SaveToFile(ms, fileName);
            return ms;
        }
        private void SaveToFile(MemoryStream ms, string fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                byte[] data = ms.ToArray();

                fs.Write(data, 0, data.Length);
                fs.Flush();

                data = null;
            }
        }

        MemoryStream GenerateData()
        {
            HSSFWorkbook hssfworkbook = new HSSFWorkbook();
            MemoryStream ms = new MemoryStream();
            var movies = (from m in db.Movies
                          select m).ToList();
            ISheet sheet1 = hssfworkbook.CreateSheet("Sheet1");
            IRow headerRow = sheet1.CreateRow(0);
            headerRow.CreateCell(0).SetCellValue("ID");
            headerRow.CreateCell(1).SetCellValue("电影名");
            headerRow.CreateCell(2).SetCellValue("发布日期");
            headerRow.CreateCell(3).SetCellValue("类型");
            headerRow.CreateCell(4).SetCellValue("价格");
            for (int i = 0; i < movies.Count; i++)
            {
                IRow Row = sheet1.CreateRow(i + 1);
                Row.CreateCell(0).SetCellValue(movies[i].ID);
                Row.CreateCell(1).SetCellValue(movies[i].Title);
                Row.CreateCell(2).SetCellValue(movies[i].ReleaseDate);
                Row.CreateCell(3).SetCellValue(movies[i].Genre);
                Row.CreateCell(4).SetCellValue(movies[i].Price.ToString());
            }
            hssfworkbook.Write(ms);
            return ms;
        }
    }
}
