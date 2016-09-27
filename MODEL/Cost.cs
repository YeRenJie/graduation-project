//============================================================
//==Name：收支表
//==Coder：新生帝
//==DTime：2014/2/25 13:58:13
//============================================================

using System;
using System.Collections.Generic;
using System.Text;

namespace MODEL
{
    [Serializable()]
    public class Cost
    {
        public int CID { get; set; }
        public decimal CMoney { get; set; }
        public DateTime CDate { get; set; }
        public DateTime CTime { get; set; }
        public string CDes { get; set; }
        public bool CIsDel { get; set; }
        public decimal CBlack { get; set; }
    }
}
