namespace Project.Scripts.Runtime.Angrybird.Hints
{
  public class ColorChangeHintFactory : IVisualHintFactory
  {
    public IVisualHint CreateVisualHint()
    {
      return new ColorChangeWithDelay();
    }
  }
}
