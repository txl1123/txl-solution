using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TxlMvc.Models;
using TxlMvc;
namespace TxlMvc.Controllers
{
    public class TreeTestController : Controller
    {
        //
        // GET: /TreeTest/
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 绑定菜单栏数据
        /// </summary>
        /// <returns></returns>
        public ActionResult getData()
        {
            //总类 父节点
            List<PriceType> plst = new List<PriceType>();
            //一级节点
            List<Module> mlst = new List<Module>();
            //根节点
            List<FunModuleShip> flst = new List<FunModuleShip>();
            PriceTypeData pd = new PriceTypeData();
            ModuleData md = new ModuleData();
            FunModuleData fd = new FunModuleData();
            plst = pd.select(0, "", "","");
            mlst = md.select(0, "", "", "");
            flst = fd.select(0, "", "", "");
            List<menuTree> Treelst = new List<menuTree>();
            if (plst.Count > 0)
            {
                for (int i = 0; i < plst.Count; i++)
                {
                    Treelst.Add(new menuTree { 
                        id="p"+plst[i].Id,
                        pId = "0",
                        name=plst[i].TypeName

                    });
                }
            }
            if (mlst.Count > 0)
            {
                for (int i = 0; i < mlst.Count; i++)
                {
                    Treelst.Add(new menuTree
                    {
                        id = "m" + mlst[i].Mid  ,
                        pId ="p"+mlst[i].Pid,
                        name = mlst[i].MName

                    });
                }
            }
            if (flst.Count > 0)
            {
                for (int i = 0; i < flst.Count; i++)
                {
                    Treelst.Add(new menuTree
                    {
                        id = "f" + flst[i].Fid ,
                        pId = "m" + flst[i].Mid,
                        name = flst[i].FunName

                    });
                }
            }
           
            object data = Treelst;
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public class menuTree
        {
            public string id { get; set; }
            public string pId { get; set; } // 增加父节点id 
            public string name { get; set; }
        } 
    }

}
