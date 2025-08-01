using System.Collections;
using Project.Scripts.Runtime.Angrybird.Utils.Configurations;
using UnityEngine;

namespace Project.Scripts.Runtime.Angrybird.Hints
{
  public class ColorChangeWithDelay : IVisualHint
  {
    private ColorChangeCueConfiguration _config;
    private SpriteRenderer _spriteRenderer;
    public Color _cueColor { get; set; }
    public Color _defaultColor { get; set; }

    public Color _currentColor { get; set; }


    public void Initialize(SpriteRenderer sp, Color hintColor)
    {
      _spriteRenderer = sp;
      _cueColor = hintColor;
      _currentColor = sp.color;
      _defaultColor = _currentColor;
    }
    private IEnumerator Delay(float delay)
    {

      yield return new WaitForSeconds(delay);
    }

    public void OnHintEnabled()
    {
      Delay(10f);
      _currentColor = _cueColor;
      ChangeColor(_currentColor);
    }

    public void OnHintDisabled()
    {
      _currentColor = _defaultColor;
      ResetColor();
    }

    public void HintToggle()
    {
      if (_currentColor == _defaultColor)
      {
        _currentColor = _cueColor;
        ChangeColor(_currentColor);
      } else
      {
        ResetColor();
      }

    }

    private void ResetColor()
    {
        _spriteRenderer.color = _defaultColor;
    }

    private void ChangeColor(Color color)
    {
        _spriteRenderer.color = color;
    }
  }
}
