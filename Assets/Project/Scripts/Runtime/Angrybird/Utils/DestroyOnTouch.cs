using Project.Scripts.Runtime.Angrybird.Utils.Interfaces;
using UnityEngine;

namespace Project.Scripts.Runtime.Angrybird.Utils
{
    public class DestroyOnTouch : MonoBehaviour, IDestroyable
    {
        private void Awake()
        {
           // GameEvents.current.OnTouchDestroyableItem += OnDistroyed;
        }
        public void OnDistroyed()
        {
            this.gameObject.SetActive(false);
        }
    }

    public class DestroyOnDamage : MonoBehaviour, IDestroyable
    {
        public float MaxHealth { get; set; }
        private float m_CurrentHealth;
        private const float k_Threshhold = 0.2f;

        private void Awake()
        {
            m_CurrentHealth = MaxHealth;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            var velocity = other.relativeVelocity.magnitude;
            if (!(velocity > k_Threshhold)) return;
            m_CurrentHealth -= velocity;
            if (m_CurrentHealth <= 0f)
            {
                OnDistroyed();
            }
        }

        public void OnDistroyed()
        {
            Destroy(gameObject);
        }
    }
}