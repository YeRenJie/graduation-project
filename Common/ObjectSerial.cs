using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Common
{
    /// <summary>
    /// ObjectSerial 序列号的类
    /// </summary>
    public class ObjectSerial
    {
        public ObjectSerial()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        #region 序列化 +  public static void ObjectSerializable(object obj, string filePath)
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="obj">要序列的对象</param>
        /// <param name="filePath">序列保存文件的路径</param>
        public static void ObjectSerializable(object obj, string filePath)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(filePath, FileMode.Create);
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, obj);
            }
            catch (IOException ex)
            {
                Console.WriteLine("序列化是出错！");
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }
        }
        #endregion

        #region 反序列化 +  public static object ObjectUnSerializable(string filePath)
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="filePath">序列保存文件的路径</param>
        /// <returns></returns>
        public static object ObjectUnSerializable(string filePath)
        {
            FileStream fs = null;
            object obj = null;
            try
            {
                fs = new FileStream(filePath, FileMode.OpenOrCreate);
                BinaryFormatter bf = new BinaryFormatter();
                obj = bf.Deserialize(fs);
            }
            catch (IOException ex)
            {
                Console.WriteLine("反序列化时出错！");
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }
            return obj;
        }
        #endregion
    }
}