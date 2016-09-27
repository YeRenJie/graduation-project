//============================================================
//==Name：会员表
//==Coder：新生帝
//==DTime：2014/2/17 15:30:27
//============================================================

using System;
using System.Collections.Generic;
using System.Text;

namespace MODEL
{
    [Serializable()]
    public class User
    {
        public int UID { get; set; }
        public string ULoginName { get; set; }
        public string ULoginPwd { get; set; }
        public DateTime URegTime { get; set; }
        public string URegIP { get; set; }
        public DateTime UCurrentTime { get; set; }
        public string UCurrentIP { get; set; }
        public DateTime ULastTime { get; set; }
        public string ULastIP { get; set; }
        public bool UIsRoot { get; set; }
        public bool UIsNormal { get; set; }
        public int ULevel { get; set; }
        public int ULoginCount { get; set; }
        public bool UIsDel { get; set; }
        public string UNick { get; set; }
        public string USex { get; set; }
        public string URealName { get; set; }
        public string UPic { get; set; }
        public string UEmail { get; set; }
        public string UPhone { get; set; }
        public string UQQ { get; set; }
        public string UAddress { get; set; }
    }
}
