using UnityEngine;
using System;

namespace TDS.HealthSystem
{
    public sealed class HealthComponent : MonoBehaviour
    {
        public event Action<float> OnMaxHealthChanged;
        public event Action<float> OnCurrentHealthChanged;
        public event Action<float> OnAdd;
        public event Action<float> OnRemove;

        public float CurrentHealth { get => _currentHealth; }

        [SerializeField]
        [HideInInspector]
        private float _currentHealth = 50;

        public float MaxHealth { get => _maxHealth; }

        [SerializeField]
        [HideInInspector]
        private float _maxHealth = 100;

        public void AddHealth(float amount)
        {

            if ((_currentHealth + amount) > _maxHealth)
                _currentHealth = _maxHealth;
            else
                _currentHealth += amount;

            OnAdd?.Invoke(amount);
            OnCurrentHealthChanged?.Invoke(_currentHealth);
        }

        public void RemoveHealth(float amount)
        {
            if ((_currentHealth - amount) < 0)
                _currentHealth = 0;
            else
                _currentHealth -= amount;

            OnRemove?.Invoke(amount);
            OnCurrentHealthChanged?.Invoke(_currentHealth);
        }

        public void SetMaxHealth(float value)
        {
            _maxHealth = value;
            OnMaxHealthChanged?.Invoke(value);
        }

        public void SetCurrentHealth(float value)
        {
            if (value < 0)
                return;

            if (value > _maxHealth)
                _currentHealth = _maxHealth;
            else
                _currentHealth = value;

            OnCurrentHealthChanged?.Invoke(_currentHealth);
        }
    }
}
