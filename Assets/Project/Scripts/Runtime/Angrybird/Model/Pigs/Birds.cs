using System;
using System.Collections;
using UnityEngine;

namespace Project.Scripts.Runtime.Angrybird.Model.Pigs
{
  public class Birds : MonoBehaviour
  {
    private static readonly int IsDead = Animator.StringToHash("isDead");
    [SerializeField] private float MaxHealth;
    private float m_CurrentHealth;
    private const float k_Threshhold = 0.2f;
    private Animator m_Animator;
    public int Id { get; set; }
    public event EventHandler OnDeath;
    private void Awake()
    {
      m_CurrentHealth = MaxHealth;
      m_Animator = GetComponent<Animator>();
      OnDeath += OnBirdDeath_PlayAnimation;
      OnDeath += OnBirdDeath_Destroy;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
      var velocity = other.relativeVelocity.magnitude;
      if (!(velocity > k_Threshhold)) return;
      m_CurrentHealth -= velocity;
      if (m_CurrentHealth <= 0f)
      {
        OnDeath?.Invoke(this, EventArgs.Empty);
      }
    }
    private void OnDestroy()
    {
      OnDeath -= OnBirdDeath_PlayAnimation;
      OnDeath -= OnBirdDeath_Destroy;
    }
    private IEnumerator DestroyCoroutine(float ms)
    {
      yield return new WaitForSeconds(ms);
      Destroy(gameObject);
    }
    private void OnBirdDeath_Destroy(object sender, EventArgs e) => DestroyAfter(1f);
    private void DestroyAfter(float ms) => StartCoroutine(DestroyCoroutine(ms));
    private void OnBirdDeath_PlayAnimation(object sender, EventArgs e) => m_Animator.SetBool(IsDead , true);
  }
}