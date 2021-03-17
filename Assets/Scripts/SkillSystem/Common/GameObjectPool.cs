using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mr_T;

/*
 * 使用方式:
 * 1.所有频繁创建/销毁的物体,都通过对象池创建/回收
 *       GameObjectPool.Instance.CreatOjbect("类别","预制件","位置","旋转")
 *       GameObjectPool.Instance.CollectOjbect("预制件对象")
 *       
 * 2.需要通过对象池创建的物体,如需每次创建时执行,则让脚本实现IResetable接口
 */
public class GameObjectPool : MonoSingleton<GameObjectPool>
{

    public interface IResetable
    {
        void OnReset();
    }
    private Dictionary<string, List<GameObject>> cache;

    protected override void Init()
    {
        base.Init();
        cache = new Dictionary<string, List<GameObject>>();
    }

    /// <summary>
    /// 创建对象
    /// </summary>
    /// <param name="key">类别</param>
    /// <param name="prefab">需要创建实例的预制件</param>
    /// <param name="pos"></param>
    /// <param name="rot"></param>
    /// <returns></returns>
    public GameObject CreatObject(string key, GameObject prefab, Vector3 pos, Quaternion rot)
    {
        //查找对象
        GameObject go = FindUsableObject(key);
        //如果对象没有,则创建对象
        if (go == null)
            go = AddObject(key, prefab);
        //设置对象
        UserObject(pos, rot, go);
        return go;
    }

    /// <summary>
    /// 销毁对象
    /// </summary>
    /// <param name="go">对象</param>
    /// <param name="delay">延迟时间,默认为0</param>
    public void CollectObject(GameObject go, float delay = 0) => StartCoroutine(CollectObjectDelay(go, delay));

    /// <summary>
    /// 清除某一类别的
    /// </summary>
    /// <param name="key"></param>
    public void Clear(string key)
    {
        if (cache.ContainsKey(key))
        {
            for (int i = cache[key].Count - 1; i >= 0; i--)
            {
                Destroy(cache[key][i].gameObject);
            }
            cache.Remove(key);
        }
    }

    /// <summary>
    /// 清除字典全部元素
    /// </summary>
    public void ClearAll()
    {
        List<string> keyList = new List<string>(cache.Keys);
        foreach (var item in keyList)
        {
            Clear(item);
        }
    }

    /// <summary>
    /// 使用对象,为对象赋值
    /// </summary>
    /// <param name="pos">位置</param>
    /// <param name="rot">旋转</param>
    /// <param name="go">状态</param>
    private void UserObject(Vector3 pos, Quaternion rot, GameObject go)
    {
        go.transform.position = pos;
        go.transform.rotation = rot;
        go.SetActive(true);
        //遍历物体中所有需要重置的逻辑,接口实现
        for (int i = 0; i < go.GetComponents<IResetable>().Length; i++)
        {
            go.GetComponents<IResetable>()[i].OnReset();
        }
    }

    /// <summary>
    /// 添加对象
    /// </summary>
    /// <param name="key"></param>
    /// <param name="prefab"></param>
    /// <returns></returns>
    private GameObject AddObject(string key, GameObject prefab)
    {
        //创建对象
        GameObject go = Instantiate(prefab);
        //如果池中没有key 就添加记录
        if (!cache.ContainsKey(key))
            cache.Add(key, new List<GameObject>());
        cache[key].Add(go);
        return go;
    }

    /// <summary>
    /// 查找指定累表中可以使用的对象
    /// </summary>
    /// <param name="key">类型</param>
    /// <returns></returns>
    private GameObject FindUsableObject(string key)
    {
        if (cache.ContainsKey(key))
            return cache[key].Find(go => !go.activeInHierarchy);
        return null;
    }

    public IEnumerator CollectObjectDelay(GameObject go, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (go != null)
            go.SetActive(false);
    }
}
