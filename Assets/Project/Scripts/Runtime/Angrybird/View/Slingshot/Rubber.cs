using DG.Tweening;
using Project.Scripts.Runtime.Angrybird.Utils.Configurations;
using UnityEngine;

namespace Project.Scripts.Runtime.Angrybird.View.Slingshot
{
  public partial class Rubber : MonoBehaviour
  {
    [SerializeField] private SlingshotConfiguration m_Config;

    private LineRenderer m_LeftLineRenderer;
    private LineRenderer m_RightLineRenderer;
    [SerializeField] private Transform m_LeftRubber;
    [SerializeField] private Transform m_RightRubber;
    [SerializeField] private Transform m_Center;
    [SerializeField] private Transform m_Holder;
    [SerializeField] private Vector3 m_HolderInitialPosition;
    //
    // set to property, with public getter.
    public Transform Holder => m_Holder;
    public Vector3 HolderInitialPosition => m_HolderInitialPosition;
    public Transform Center => m_Center;
    public SlingshotConfiguration Config => m_Config;

    private void Awake()
    {
      m_LeftLineRenderer = m_LeftRubber.GetComponent<LineRenderer>();
      m_RightLineRenderer = m_RightRubber.GetComponent<LineRenderer>();
      m_HolderInitialPosition = m_Holder.position;
    }

    public void Init()
    {
      m_LeftLineRenderer.positionCount = 2;
      m_RightLineRenderer.positionCount = 2;

      m_LeftLineRenderer.SetPosition(0, m_LeftRubber.position);
      m_RightLineRenderer.SetPosition(0, m_RightRubber.position);
    }

    public void Set(Vector3 position)
    {
      position = m_Center.position + Vector3.ClampMagnitude(position - m_Center.position, m_Config.maxLength);
      position.y = Mathf.Clamp(position.y, m_Config.bottomBoundary, 1000);

      m_LeftLineRenderer.SetPosition(1, position);
      m_RightLineRenderer.SetPosition(1, position);

      // fix later, projectile position is the one that needs updating.
      m_Holder.transform.position = position;
    }

    public void Reset(Vector3 position)
    {
      m_LeftLineRenderer.SetPosition(1, position);
      m_RightLineRenderer.SetPosition(1, position);

      m_Holder.transform.position = position;
    }
  }

  public partial class Rubber
  {
    // Animation
    [SerializeField] private Transform m_Elastic;
    public void Animation()
    {
      m_Elastic.position = m_LeftLineRenderer.GetPosition(0);
      var distance = Vector2.Distance(m_Elastic.position, m_Center.position);
      var tho = distance / 1.2f;
      m_Elastic.DOMove(m_Center.position, tho);
      AnimateLineRenderer(m_Elastic, tho);
    }
    private void AnimateLineRenderer(Transform transform, float tho)
    {
      var elapsedTime = 0f;
      while (elapsedTime < tho)
      {
        elapsedTime += Time.deltaTime;
        Set(transform.position);
      }
    }
  }
  
}
