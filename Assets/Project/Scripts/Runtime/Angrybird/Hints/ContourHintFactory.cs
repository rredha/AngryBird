using UnityEngine;

namespace Project.Scripts.Runtime.Angrybird.Hints
{
  public class ContourHintFactory : MonoBehaviour, IVisualHintFactory
  {
    public IVisualHint CreateVisualHint()
    {
      return new Contour();
    }
  }
}
