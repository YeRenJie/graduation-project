using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Common
{
    /// <summary>
    /// 返回json数据的类
    /// </summary>
    public class JsonMsg
    {
        private string msg;
        private string nextUrl;
        private string nickName;

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName
        {
            get { return nickName; }
            set { nickName = value; }
        }

        /// <summary>
        /// 下一个页面
        /// </summary>
        public string NextUrl
        {
            get { return nextUrl; }
            set { nextUrl = value; }
        }

        /// <summary>
        /// 信息
        /// </summary>
        public string Msg
        {
            get { return msg; }
            set { msg = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg">要返回的字符串</param>
        /// <param name="nextUrl">下一个页面</param>
        public JsonMsg(string msg, string nextUrl, string nickName)
        {
            this.msg = msg;
            this.nextUrl = nextUrl;
            this.nickName = nickName;
        }
        public JsonMsg() { }

        /// <summary>
        /// 返回Json数据格式
        /// </summary>
        /// <param name="js"></param>
        /// <returns></returns>
        public static string ReturnJsonMsg(JsonMsg js)
        {
            JavaScriptSerializer json = new JavaScriptSerializer();
            return json.Serialize(js);
        }
    }
}
