using UnityEngine;
using UnityEngine.Serialization;

namespace View.Shapes
{
    public class ShapeVisual : MonoBehaviour
    {
        [SerializeField] private LineRenderer lineRenderer;
        private Color _lineColor = Color.cyan;
        private float _lineWidth = .2f;

        private void Awake()
        {
            lineRenderer.startColor = _lineColor;
            lineRenderer.endColor = _lineColor;
            lineRenderer.startWidth = _lineWidth;
            lineRenderer.endWidth = _lineWidth;
            lineRenderer.loop = true;
        }
        public void Draw(Vector3[] vertices)
        {
            lineRenderer.positionCount = vertices.Length;
            for (int i = 0; i < lineRenderer.positionCount; i++)
            {
                vertices[i] = transform.localPosition - vertices[i];
            }
            lineRenderer.SetPositions(vertices);
        }

        public void Log(Vector3[] vertices)
        {
            var count = 1;
            foreach (var vertex in vertices)
            {
               Debug.Log($"Vertex {count} -> [{vertex.x}, {vertex.y}, {vertex.z}].");
               count++;
            }
        }
    }
}