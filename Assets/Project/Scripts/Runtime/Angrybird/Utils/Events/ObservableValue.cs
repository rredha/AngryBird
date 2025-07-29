using System;
using UnityEngine;

namespace Project.Scripts.Runtime.Angrybird.Utils.Events
{
    [Serializable]
    public class ObservableValue<T> where T : IEquatable<T>
    {
        [SerializeField] private T _value;
    
        public event EventHandler<ValueChangedEventArgs<T>> ValueChanged;
    
        public T Value
        {
            get => _value;
            set
            {
                if (!_value.Equals(value))
                {
                    T oldValue = _value;
                    _value = value;
                    OnValueChanged(oldValue, value);
                }
            }
        }
    
        public ObservableValue(T initialValue = default(T))
        {
            _value = initialValue;
        }
    
        protected virtual void OnValueChanged(T oldValue, T newValue)
        {
            ValueChanged?.Invoke(this, new ValueChangedEventArgs<T>(oldValue, newValue));
        }
    
        public static implicit operator T(ObservableValue<T> observable)
        {
            return observable.Value;
        }
    }
}