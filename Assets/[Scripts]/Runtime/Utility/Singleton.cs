using UnityEngine;

namespace VoxelProject.Utility
{
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        private static object _lock = new object();
        protected static bool applicationIsQuitting = false;

        protected static T Instance
        {
            get
            {
                if (applicationIsQuitting)
                    return null;

                lock (_lock)
                {
                    if (_instance == null)
                    {
                        new GameObject(typeof(T).Name + " (Singleton)").AddComponent<T>();
                        Debug.Log("1");
                    }

                    return _instance;
                }
            }
        }

        private void Awake()
        {
            if (applicationIsQuitting || (_instance != null && _instance != this))
            {
                Destroy(gameObject);
                return;
            }

            Debug.Log("2");

            DontDestroyOnLoad(gameObject);
            _instance = GetComponent<T>();
        }

        private void OnDestroy()
        {
            if (_instance == this)
                _instance = null;
        }

        private void OnApplicationQuit()
        {
            applicationIsQuitting = true;
            Destroy(gameObject);
            _instance = null;
        }
    }
}