using CodeMonkey.Utils;
using UnityEngine;

namespace FieldOfView.Scripts
{
    public class FieldOfView : MonoBehaviour
    {
        [SerializeField] private LayerMask layerMask;
        [SerializeField, Min(0)] private int raysCount = 50;
        [SerializeField, Min(0)] private float  fov = 90f;
        [SerializeField, Min(0)] private float  viewDistance = 50f;

        private float _startingAngle;
        private Vector3 _origin;
        private Mesh _mesh;

        private void Start()
        {
            _mesh = new Mesh();
            GetComponent<MeshFilter>().mesh = _mesh;
            _origin = Vector3.zero;
            _startingAngle = fov / 2 - 45;
        }

        private void LateUpdate()
        {
            var angle = _startingAngle;
            var angleIncrease = fov / raysCount;

            var vertices = new Vector3[raysCount + 1 + 1];
            var uv = new Vector2[vertices.Length];
            var triangles = new int[raysCount * 3];

            vertices[0] = _origin;

            var vertexIndex = 1;
            var triangleIndex = 0;
            for (var i = 0; i <= raysCount; i++)
            {
                Vector3 vertex;
                var raycastHit2D = Physics2D.Raycast(_origin, 
                    UtilsClass.GetVectorFromAngle(angle), viewDistance, layerMask);
                if (raycastHit2D.collider == null) // No hit
                    vertex = _origin + UtilsClass.GetVectorFromAngle(angle) * viewDistance;
                else // Hit object
                    vertex = raycastHit2D.point;

                vertices[vertexIndex] = vertex;

                if (i > 0)
                {
                    triangles[triangleIndex + 0] = 0;
                    triangles[triangleIndex + 1] = vertexIndex - 1;
                    triangles[triangleIndex + 2] = vertexIndex;

                    triangleIndex += 3;
                }

                vertexIndex++;
                angle -= angleIncrease;
            }
            
            _mesh.vertices = vertices;
            _mesh.uv = uv;
            _mesh.triangles = triangles;
            _mesh.bounds = new Bounds(_origin, Vector3.one * 1000f);
        }

        public void SetOrigin(Vector3 origin) 
            => _origin = origin;

        public void SetAimDirection(Vector3 aimDirection) 
            => _startingAngle = UtilsClass.GetAngleFromVectorFloat(aimDirection) + fov / 2f;

        public void SetFoV(float newFov) 
            => fov = newFov;

        public void SetViewDistance(float newViewDistance) 
            => viewDistance = newViewDistance;
    }
}