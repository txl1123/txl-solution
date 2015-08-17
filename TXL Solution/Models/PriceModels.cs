using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TxlMvc.Models
{
    public class PriceType
    {
        public int Id { get; set; }

        public string TypeName { get; set; }

    }
    public class Module
    {
        public int Mid { get; set; }

        public string MName { get; set; }

        public int Pid { get; set; }
        public bool Atype { get; set; }

    }
    public class FunModuleShip
    {
        public int Fid { get; set; }

        public string FunName { get; set; } 
        public int id { get; set; }
        public int Mid { get; set; }

      

    }



}