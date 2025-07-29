using UnityEngine;

namespace Project.Scripts.Runtime.Angrybird.MovementProvider
{
    public class HapticPointer : MonoBehaviour, IMovementProvider
    {
        private Color _defaultColor;
        [SerializeField] private Vector2 m_Bias = new (0f, -1.64f); // 0 -1.64
        [SerializeField] private Vector2 m_ScaleFactor = new (-0.055f, 0.033f);
        public Vector3 PointerWorldPosition => GetPointerPosition();

        [SerializeField] private GameObject HapticPluginPrefab;
        private HapticPlugin m_HPlugin;

        public void Initialize()
        {
            GameObject HapticGameObject;
            HapticGameObject = Instantiate(HapticPluginPrefab, transform);
            m_HPlugin = HapticGameObject.GetComponent<HapticPlugin>();
        }

        public void Subscribe()
        {
        }

        public void Unsubscribe()
        {
        }

        public bool MoveEventRaised { get; private set; }

        public Vector3 GetPointerPosition()
        {
            MoveEventRaised = true;
            var position = new Vector3
            {
                x = m_HPlugin.CurrentPosition.x * m_ScaleFactor.x + m_Bias.x,
                y = m_HPlugin.CurrentPosition.y * m_ScaleFactor.y + m_Bias.y,
                z = 5f
            };
            return position;
        }
        
        public bool SelectEventRaised { get; private set; }

        public void Select_performed()
        {
            SelectEventRaised = true;
        }


    }
}