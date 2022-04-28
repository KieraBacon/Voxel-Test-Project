using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace ResourceSystem
{
    public class HealthBarManager : MonoBehaviour
    {
        #region Singleton
        private static HealthBarManager _instance;
        private static object _lock = new object();
        protected static bool applicationIsQuitting = false;

        protected static HealthBarManager Instance
        {
            get
            {
                if (applicationIsQuitting)
                    return null;

                lock (_lock)
                {
                    if (_instance == null)
                    {
                        new GameObject(typeof(HealthBarManager).Name + " (Singleton)").AddComponent<HealthBarManager>();
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

            DontDestroyOnLoad(gameObject);
            _instance = GetComponent<HealthBarManager>();
            OnAwake();
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
        #endregion
        #region Object Pooling
        private static ObjectPool<HealthBar> pool = new ObjectPool<HealthBar>(OnCreate, OnGet, OnRelease, OnDestroy);

        private static HealthBar healthBarPrefab;
        private static HealthBar OnCreate()
        {
            if (!healthBarPrefab)
                healthBarPrefab = Resources.Load<HealthBar>("Default Health Bar");

            HealthBar healthBar = Instantiate(healthBarPrefab, Instance.transform);
            return healthBar;
        }

        private static void OnGet(HealthBar healthBar)
        {
            healthBar.gameObject.SetActive(true);
        }

        private static void OnRelease(HealthBar healthBar)
        {
            healthBar.gameObject.SetActive(false);
        }

        private static void OnDestroy(HealthBar healthBar)
        {
            pool.Release(healthBar);
        }
        #endregion

        private Dictionary<Health, HealthBar> healthBars = new Dictionary<Health, HealthBar>();

        private void OnAwake()
        {
            Canvas canvas = gameObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            Health.OnHealthEnabled += AddHealthBar;
            Health.OnHealthDisabled += RemoveHealthBar;
        }

        private void AddHealthBar(Health health)
        {
            HealthBar healthBar = pool.Get();
            healthBars.Add(health, healthBar);
            healthBar.health = health;
        }

        private void RemoveHealthBar(Health health)
        {
            if (!healthBars.TryGetValue(health, out HealthBar healthBar)) return;
            if (!healthBar) return;

            pool.Release(healthBar);
            healthBar.health = null;
            healthBars.Remove(health);
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void AutoRun()
        {
            HealthBarManager manager = Instance;
        }
    }
}
