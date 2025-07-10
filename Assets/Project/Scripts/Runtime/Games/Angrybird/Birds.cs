using System;
using UnityEngine;

namespace Arcade.Project.Runtime.Games.AngryBird
{
  public class Birds : MonoBehaviour
  {
    [SerializeField] private float MaxHealth;
    private float m_CurrentHealth;
    private const float k_Threshhold = 0.2f;
    private Animation m_Animation;
    private string m_ClipName;

    private void Awake()
    {
      m_CurrentHealth = MaxHealth;
      m_Animation = GetComponent<Animation>();
      m_ClipName = m_Animation.clip.name;
    }

    private void Update()
    {
      if (Input.GetKeyDown(KeyCode.S))
      {
        m_Animation.Play(m_ClipName);
      }
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

    private void OnDistroyed()
    {
      Destroy(gameObject);
    }
  }
}