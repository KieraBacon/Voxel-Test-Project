using System;
using UnityEngine;

namespace ResourceSystem
{
    public class Health : MonoBehaviour
    {
        public static event Action<Health> OnHealthEnabled;
        public static event Action<Health> OnHealthDisabled;
        public event Action<Health> OnHealthAmountChanged;

        [SerializeField]
        private Transform _uiPosition;
        public Transform uiPosition => _uiPosition;

        [SerializeField]
        private float _maxHealth;
        public float maxHealth => _maxHealth;

        [SerializeField]
        private float _currentHealth;
        public float currentHealth
        {
            get => _currentHealth;
            set
            {
                if (_currentHealth == value) return;
                _currentHealth = Mathf.Clamp(value, 0, maxHealth);
                OnHealthAmountChanged?.Invoke(this);
            }
        }

        private void OnValidate()
        {
            OnHealthAmountChanged?.Invoke(this);
        }

        private void OnEnable()
        {
            OnHealthEnabled?.Invoke(this);
        }

        private void OnDisable()
        {
            OnHealthDisabled?.Invoke(this);
        }
    }
}
