using DCFApixels;
using UnityEngine;

namespace App.WarFog
{
    [RequireComponent(typeof(MeshFilter))]
    public class DynamicFieldOfView : MonoBehaviour
    {
        [SerializeField] private bool drawDebug;

        private LayerMask _layerMask;
        private float _fov;
        private float _viewDistance;
        private int _raysPerAngle;
        
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

            var raysCount =  _raysPerAngle * (int)_fov;
            var angleStep = _fov / raysCount;

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
                if (Physics.Raycast(Origin, direction, out var hit, _viewDistance, _layerMask))
                    vertex = hit.point - Origin;
                else
                    vertex = direction * _viewDistance;

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
            
            _mesh.vertices = vertices;
            _mesh.uv = uv;
            _mesh.triangles = triangles;
            _mesh.bounds = new Bounds(Vector3.zero, Vector3.one * 1000f);
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
            => _layerMask = layerMask;

        public void SetRaysPerAngle(int raysPerAngle) 
            => _raysPerAngle = raysPerAngle;

        public void SetFoV(float fov)
        {
            _fov = fov;
            _startingAngle = _fov / 2;
        }

        public void SetViewDistance(float viewDistance) 
            => _viewDistance = viewDistance;

        private static Vector3 GetVectorFromAngleAroundY(float angle)
        {
            // angle = 0 -> 360
            var angleRad = angle * (Mathf.PI / 180f);
            return new Vector3(Mathf.Sin(angleRad), 0, Mathf.Cos(angleRad));
        }
    }
}