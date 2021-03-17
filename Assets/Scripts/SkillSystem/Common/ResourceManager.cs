using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
namespace Mr_T
{
    /// <summary>
    /// 资源管理器
    /// </summary>
    public class ResourceManager
    {
        private static Dictionary<string, string> configMap;
        /// <summary>
        /// 静态构造函数
        /// 作用:初始化类的静态成员
        /// 时机:类被加载时执行一次
        /// </summary>
        static ResourceManager()
        {
            string fileContent = GetConfigFile("ConfigMap.txt");

            //解析文件(string--->Dictionary<string,string>)
            BuildMap(fileContent);
        }
        //加载文件
        public static string GetConfigFile(string path)
        {
            //file为本地文件,http为网络文件
            string url;
#if UNITY_EDITOR || UNITY_STANDALONE
            //如果在编译器下或者PC
            url = "file://" + Application.dataPath + "/StreamingAssets/" + path;
#elif UNITY_IPHONE
        //否则在IOS下
        url = "file://" + Application.dataPath + "/Raw/"+path;
#elif UNITY_ANDROID
        //否则在安卓下
        url = "jar:file://" + Application.dataPath + "!/assets/"+path;
#endif
            //移动端加载文件只可以www加载
            WWW www = new WWW(url);
            while (true)
            {
                if (www.isDone)
                {
                    return www.text;
                }
            }
        }

        public static void BuildMap(string fileContent)
        {
            configMap = new Dictionary<string, string>();
            //文件名=路径\r\n文件名===>路径
            //StringReader字符串读取器,提供了逐行读取字符串的功能
            using (StringReader reader = new StringReader(fileContent))
            {
                string line;
                //读一行满足条件就解析
                while ((line = reader.ReadLine()) != null)
                {
                    string[] keyValue = line.Split('>');
                    //文件名 keyValue[0],路径keyValue[1]
                    configMap.Add(keyValue[0], keyValue[1]);
                    Debug.Log(keyValue[0] + "  ===>  " + keyValue[1]);
                }

            }//当程序退出using代码块,将自动调用reader.Dispose()方法

        }


        public static T Load<T>(string prefabName) where T : Object
        {
            string prefabPath = configMap[prefabName];
            //prefabName--->prefabPath
            return Resources.Load<T>(prefabPath);
        }
    }
}