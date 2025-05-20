using DCFApixels;
using UnityEngine;

namespace App.WarFog
{
    [RequireComponent(typeof(MeshFilter))]
    public class DynamicFieldOfView : MonoBehaviour
    {
        [SerializeField] private bool drawDebug;

        public LayerMask LayerMask { get; private set; }
        public float FOV { get; private set; }
        public float ViewDistance { get; private set; }
        public int RaysPerAngle { get; private set; }

        private Vector3 Origin => transform.position;
        private float _startingAngle;
        private Mesh _mesh;

        private void Awake()
        {
            _mesh = new Mesh();
            GetComponent<MeshFilter>().mesh = _mesh;
        }

        private void LateUpdate()
        {
            var angle = 0f;
            if (transform.forward.x >= 0) //TODO swap it to the Vector3.SignedAngle(...)
                angle = -_startingAngle + Vector3.Angle(Vector3.forward, transform.forward);
            else
                angle = -_startingAngle - Vector3.Angle(Vector3.forward, transform.forward);

            var raysCount =  RaysPerAngle * (int)FOV;
            var angleStep = FOV / raysCount;

            //TODO hashed it
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
                if (Physics.Raycast(Origin, direction, out var hit, ViewDistance, LayerMask))
                    vertex = hit.point - Origin;
                else
                    vertex = direction * ViewDistance;

                if (drawDebug)
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
            
            _mesh.Clear();
            _mesh.SetVertices(vertices);
            _mesh.uv = uv;
            _mesh.SetTriangles(triangles, 0);
            _mesh.bounds = new Bounds(Vector3.zero, Vector3.one * 100f);
        }

        public void SetData(DynamicFieldOfViewConfig config)
            => SetData(config.LayerMask, config.FOV, config.ViewDistance, config.RaysPerAngle);

        public void SetData(LayerMask layerMask, float fov, float viewDistance, int raysPerAngle)
        {
            SetLayers(layerMask);
            SetFoV(fov);
            SetViewDistance(viewDistance);
            SetRaysPerAngle(raysPerAngle);
        }
        
        public void SetLayers(LayerMask layerMask) 
            => LayerMask = layerMask;

        public void SetRaysPerAngle(int raysPerAngle) 
            => RaysPerAngle = raysPerAngle;

        public void SetFoV(float fov)
        {
            FOV = fov;
            _startingAngle = FOV / 2;
        }

        public void SetViewDistance(float viewDistance) 
            => ViewDistance = viewDistance;

        private static Vector3 GetVectorFromAngleAroundY(float angle)
        {
            // angle = 0 -> 360
            var angleRad = angle * (Mathf.PI / 180f);
            return new Vector3(Mathf.Sin(angleRad), 0, Mathf.Cos(angleRad));
        }
    }
}