using System;
using System.Collections;
using Project.Scripts.Runtime.Angrybird.Presenter.Pigs;
using UnityEngine;

namespace Project.Scripts.Runtime.Angrybird.Model.Pigs
{
  public class BirdDataEventArgs : EventArgs
  {
    public int Id { get; private set; }
    public bool IsDead { get; private set; }

    public BirdDataEventArgs(int id, bool isDead)
    {
      Id = id;
      IsDead = isDead;
    }
    public BirdDataEventArgs(bool isDead)
    {
      IsDead = isDead;
    }
  }
  public class Birds : MonoBehaviour
  {
    private static readonly int IsDeadAnimationHash = Animator.StringToHash("isDead");
    [SerializeField] private float MaxHealth;
    private float m_CurrentHealth;
    private const float k_Threshhold = 0.2f;
    private Animator m_Animator;
    private bool _isActive;
    private bool _isDead; 
    public int Id { get; set; }
    public event EventHandler<BirdDataEventArgs> OnDeath;
    private void Awake()
    {
      m_CurrentHealth = MaxHealth;
      m_Animator = GetComponent<Animator>();
      //OnDeath += OnBirdDeath_PlayAnimation;
      OnDeath += OnBirdDeath_Destroy;
      _isActive = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
      var velocity = other.relativeVelocity.magnitude;
      if (!(velocity > k_Threshhold)) return;
      m_CurrentHealth -= velocity;
      if (m_CurrentHealth <= 0f)
      {
        _isDead = true;
      }
      else
      {
        _isDead = false;
      }
    }

    private void Update()
    {
      if (_isDead && _isActive)
      {
        OnDeath?.Invoke(this, new BirdDataEventArgs(Id, _isDead));
        _isActive = false;
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
    private void OnBirdDeath_Destroy(object sender, BirdDataEventArgs e) => DestroyAfter(0f);
    private void DestroyAfter(float ms) => StartCoroutine(DestroyCoroutine(ms));
    private void OnBirdDeath_PlayAnimation(object sender, BirdDataEventArgs e) => m_Animator.SetBool(IsDeadAnimationHash , true);
  }
}