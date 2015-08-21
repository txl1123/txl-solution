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
using System.Text;
using TxlMvc.Resource;
using TxlMvc.Helper;
namespace TxlMvc.Controllers
{
    public class HomeController : Controller
    {
        private MovieDbContext db = new MovieDbContext();
        //
        // GET: /Home/
        
        public ActionResult Index()
        {
            return View();
        }
        #region "导出Excel文件"
        HSSFWorkbook hssfworkbook;
        public ActionResult ExportToExcel()
        {

            string fileName = "test.xls";
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", fileName));
            Response.Clear();

            //SaveToFile(ms, fileName);
           
            InitializeWorkbook();
            GenerateData();
            Response.BinaryWrite(WriteToStream().GetBuffer());
            Response.End();
            return new EmptyResult();
        }


        MemoryStream WriteToStream()
        {
            //Write the stream data of workbook to the root directory
            MemoryStream file = new MemoryStream();
            hssfworkbook.Write(file);
            return file;
        }

        void GenerateData()
        {
            MemoryStream ms = new MemoryStream();
            //查询数据的数据
            var movies = (from m in db.Movies
                          select m).ToList();
            ISheet sheet1 = hssfworkbook.CreateSheet("Sheet1");
            IRow headerRow = sheet1.CreateRow(0);
            headerRow.CreateCell(0).SetCellValue("ID");
            headerRow.CreateCell(1).SetCellValue("电影名");
            headerRow.CreateCell(2).SetCellValue("发布日期");
            headerRow.CreateCell(3).SetCellValue("类型");
            headerRow.CreateCell(4).SetCellValue("价格");
            int rowIndex = 1;
            for (int i = 0; i < movies.Count; i++)
            {
                IRow Row = sheet1.CreateRow(rowIndex);
                Row.CreateCell(0).SetCellValue(movies[i].ID);
                Row.CreateCell(1).SetCellValue(movies[i].Title);
                Row.CreateCell(2).SetCellValue(movies[i].ReleaseDate.ToShortDateString());
                Row.CreateCell(3).SetCellValue(movies[i].Genre);
                Row.CreateCell(4).SetCellValue(movies[i].Price.ToString());
                rowIndex++;
            }
            hssfworkbook.Write(ms);
         
        }

        void InitializeWorkbook()
        {
            hssfworkbook = new HSSFWorkbook();

            ////create a entry of DocumentSummaryInformation
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "NPOI Team";
            hssfworkbook.DocumentSummaryInformation = dsi;

            ////create a entry of SummaryInformation
            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Subject = "NPOI SDK Example";
            hssfworkbook.SummaryInformation = si;
        }
        #endregion

        #region "导入Excel数据 "
        public ActionResult ImportToDatatable( string filename)
        {

            return new EmptyResult();
        }

        public ActionResult ImportToDb()
        {
            string filename = Server.MapPath("~/Resource/excel/test.xls");
            ExcelHelper 
                exhelper = new ExcelHelper(filename);
            DataTable dt = new DataTable();
            dt = exhelper.ExcelToDataTable("sheet1", true);

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                Movie m = new Movie();
                m.Title = dt.Rows[i][4].ToString().Trim();
                m.ReleaseDate = Convert.ToDateTime(dt.Rows[i][3].ToString());
                m.Genre = dt.Rows[i][1].ToString();
                // m.Price =decimal( dt.Rows[i][2]);
                

                db.Movies.Add(m);
                db.SaveChanges();

            }
            return View();
        }
        #endregion

        #region "发送邮件"
        public ActionResult SendEmailClick()
        {
            SendEmails();
            return new EmptyResult();
        }
        void SendEmails()
        {
            //选择模板并替换内容
            var mailcontent = email.EmailContent;
            mailcontent = mailcontent.Replace("{username}", "txl");
            mailcontent = mailcontent.Replace("{name}", "tuxiaoli");
            mailcontent = mailcontent.Replace("{jobtitle}", "");
            mailcontent = mailcontent.Replace("{organization}", "tuxiaoli");
            mailcontent = mailcontent.Replace("{email}", "21341");
            mailcontent = mailcontent.Replace("{tel}", "tuxiaoli");
            mailcontent = mailcontent.Replace("{address}", "tuxiaoli");
            mailcontent = mailcontent.Replace("{title}", "tuxiaoli");
            mailcontent = mailcontent.Replace("{content}", "tuxiaoli");
            mailcontent = mailcontent.Replace("{dblist}", "tuxiaoli");
            mailcontent = mailcontent.Replace("{date}", DateTime.Now.ToShortDateString());

            TxlMvc.Helper.EmailHelper.SendMail("hahaha", mailcontent, "450593364@qq.com");
        }
        #endregion

        [HttpGet]
        public ActionResult Login()
        {
        
            return View();
        }
        #region "登录注册"
        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(LoginModel userInfo)
        {
            string errorMsg = "";
          
            
            bool success = UserHelper.Login(userInfo,out errorMsg);

            return Json(new { success = success, msg = errorMsg },
                JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(UserProfile userInfo)
        {
            string errorMsg = "";
           
            bool success = UserHelper.Register(userInfo, out errorMsg);

            return Json(new { success = success, msg = errorMsg },
                JsonRequestBehavior.AllowGet);
 
        }
        #endregion 
    }

}
