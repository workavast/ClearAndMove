using DCFApixels;
using UnityEngine;

namespace App
{
    [RequireComponent(typeof(MeshFilter))]
    public class FieldOfView : MonoBehaviour
    {
        [SerializeField] private LayerMask layerMask;
        [SerializeField, Min(0)] private int raysPerAngle = 4;
        [SerializeField, Range(0, 360)] private float fov = 90f;
        [SerializeField, Min(0)] private float viewDistance = 50f;
        [SerializeField] private bool debug;

        private Vector3 Origin => transform.position;
        private float _startingAngle;
        private Mesh _mesh;

        private void Awake()
        {
            _mesh = new Mesh();
            GetComponent<MeshFilter>().mesh = _mesh;
            _startingAngle = fov / 2;
        }

        private void LateUpdate()
        {
            var angle = 0f;
            if (transform.forward.x >= 0)
                angle = -_startingAngle + Vector3.Angle(Vector3.forward, transform.forward);
            else
                angle = -_startingAngle - Vector3.Angle(Vector3.forward, transform.forward);

            var raysCount =  raysPerAngle * (int)fov;
            var angleStep = fov / raysCount;

            var vertices = new Vector3[raysCount + 1 + 1];
            var uv = new Vector2[vertices.Length];
            var triangles = new int[raysCount * 3];

            vertices[0] = Vector3.zero;

            var vertexIndex = 1;
            var triangleIndex = 0;
            for (var i = 0; i <= raysCount; i++)
            {
                Vector3 vertex;

                var direction = GetVectorFromAngleAroundY(angle);
                if (Physics.Raycast(Origin, direction, out var hit, viewDistance, layerMask))
                    vertex = hit.point - Origin;
                else
                    vertex = direction * viewDistance;

                if (debug)
                {
                    if (i == 0)
                        DebugX.Draw(Color.magenta).Line(Origin, (Origin + vertex));
                    else
                        DebugX.Draw(Color.magenta).Line(Origin, (Origin + vertex));
                }

                vertices[vertexIndex] =  transform.InverseTransformPoint(Origin + vertex);

                if (i > 0)
                {
                    triangles[triangleIndex + 0] = 0;
                    triangles[triangleIndex + 1] = vertexIndex - 1;
                    triangles[triangleIndex + 2] = vertexIndex;

                    triangleIndex += 3;
                }

                vertexIndex++;
                angle += angleStep;
            }
            
            _mesh.vertices = vertices;
            _mesh.uv = uv;
            _mesh.triangles = triangles;
            _mesh.bounds = new Bounds(Vector3.zero, Vector3.one * 1000f);
        }

        public void SetFoV(float newFov) 
            => fov = newFov;

        public void SetViewDistance(float newViewDistance) 
            => viewDistance = newViewDistance;

        private static Vector3 GetVectorFromAngleAroundY(float angle)
        {
            // angle = 0 -> 360
            var angleRad = angle * (Mathf.PI / 180f);
            return new Vector3(Mathf.Sin(angleRad), 0, Mathf.Cos(angleRad));
        }
    }
}