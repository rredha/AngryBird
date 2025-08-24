using UnityEngine;

namespace Model.Shape
{
    [CreateAssetMenu(fileName = "Generic Shape", menuName = "Shapes")]
    public class ShapeSO : ScriptableObject
    {
        public float Size;
        public Vector3 Center;
        public float Offset;
        public float Radius;
        public int VerticesCount;
    }
}