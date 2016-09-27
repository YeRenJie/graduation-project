//============================================================
//==Name：在线留言
//==Coder：新生帝
//==DTime：2014/2/19 16:01:46
//============================================================

using System;
using System.Collections.Generic;
using System.Text;

namespace MODEL
{
    [Serializable()]
    public class Feedback
    {
        public int FID { get; set; }
        public int FUID { get; set; }
        public string FThemb { get; set; }
        public string FMsg { get; set; }
        public DateTime FTime { get; set; }
        public string FIP { get; set; }
        public string FEmail { get; set; }
        public bool FIsDel { get; set; }
    }
}
