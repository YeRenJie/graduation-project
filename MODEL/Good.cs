//============================================================
// Producnt name:		消费表
// Version: 			1.0
// Coded by:			新生帝
// Create time:			2014/2/21 21:43:03
//============================================================

using System;
using System.Collections.Generic;
using System.Text;

namespace MODEL
{
    [Serializable()]
    public class Good
    {
        public int GID { get; set; }
        public DateTime GBuyTime { get; set; }
        public string GName { get; set; }
        public decimal GPrice { get; set; }
        public int GCount { get; set; }
        public string GBuyPlace { get; set; }
        public int GBuyUser { get; set; }
        public string GDes { get; set; }
        public string GPayWay { get; set; }
        public DateTime GTime { get; set; }
        public bool GIsDel { get; set; }
    }
}
