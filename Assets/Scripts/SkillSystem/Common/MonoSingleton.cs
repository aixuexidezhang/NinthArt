using UnityEngine;

namespace Mr_T
{
    /// <summary>
    /// 单例类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        private static object lockHelper = new object();//lock语句的一个需要,必须为object的子类。

        private static volatile T instance;//volatile防止重新排序。
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockHelper)//双重检查
                    {
                        instance = FindObjectOfType<T>();
                        if (instance == null)
                        {
                            instance = new GameObject("Singleton of" + typeof(T)).AddComponent<T>();
                        }
                    }
                }
                return instance;
            }
        }
        private void Awake()
        {
            if (instance == null)
            {
                lock (lockHelper)//双重检查
                {
                    if (instance == null)
                        instance = this as T;
                }
            }
            Init();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        protected virtual void Init()
        {

        }
    }
}