using UnityEngine;

namespace View.Bubble
{
    public class BubbleView : MonoBehaviour
    {
        private SpriteRenderer m_Sprite;
        private void Awake()
        {
            m_Sprite = gameObject.GetComponent<SpriteRenderer>();
        }
    }
}