using System.Collections.Generic;
using Arcade.Project.Runtime.Games.AngryBird.Hints;
using UnityEngine;

namespace Arcade.Project.Runtime.Games.AngryBird
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