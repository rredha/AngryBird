using System.Collections.Generic;
using Project.Scripts.Runtime.Angrybird.Hints;
using UnityEngine;

namespace Project.Scripts.Runtime.Angrybird.View.Pointer
{
    public class PointerVisual : MonoBehaviour
    {
        public List<IVisualHint> ColorChangeHints { get; private set; }
        private Color _defaultColor;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            ColorChangeHints = new List<IVisualHint>();
            ColorChangeHints.Add(new ColorChangeHintFactory().CreateVisualHint());
            ColorChangeHints.Add(new ColorChangeWithDelayFactory().CreateVisualHint());
            ColorChangeHints[0].Initialize(_spriteRenderer, Color.blue);
            ColorChangeHints[1].Initialize(_spriteRenderer, Color.red);
        }
    }
}