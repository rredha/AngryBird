namespace Project.Scripts.Runtime.Angrybird.Hints
{
  public class ColorChangeWithDelayFactory : IVisualHintFactory
  {
    public IVisualHint CreateVisualHint()
    {
      return new ColorChangeWithDelay();
    }
  }
}
