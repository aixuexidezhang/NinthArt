using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

/*
 * 编译器类:只在Unity编译器中执行代码
 * 只要预制件有变化就进行刷新,自动获取文件名和路径,并生成txt文件
 * AssetDatabase:只在Unity编译器中操作资源中的相关功能
 */
public class GenerateResConfig : Editor
{
    [MenuItem("Tools/Resources/Generate ResConfig File")]
    private static void Generate()
    {
        //生成资源配置文件
        //1.查找Resources目录下所有预制件的路径
        string[] resFiles = AssetDatabase.FindAssets("t:prefab", new string[] { "Assets/Resources" });
        for (int i = 0; i < resFiles.Length; i++)
        {
            resFiles[i] = AssetDatabase.GUIDToAssetPath(resFiles[i]);
            //Assets/Resources/Skills/普通攻击.prefab
            string fileName = Path.GetFileNameWithoutExtension(resFiles[i]);
            string filePath = resFiles[i].Replace("Assets/Resources/", string.Empty).Replace(".prefab", string.Empty);

            //2.生成对应关系
            //名称===>路径
            resFiles[i] = fileName + ">" + filePath;
            Debug.Log(resFiles[i]);
        }
        //3.写入文件 兼容PC Android ios 
        File.WriteAllLines("Assets/StreamingAssets/ConfigMap.txt", resFiles);
        //刷新
        AssetDatabase.Refresh();
    }
}
