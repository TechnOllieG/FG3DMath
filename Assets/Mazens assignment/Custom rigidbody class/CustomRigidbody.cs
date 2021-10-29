using UnityEngine;

namespace FG
{
    public class CustomRigidbody : MonoBehaviour
    {
        public Vector3 Position
        {
            get => Tf.position;
            set => Tf.position = value;
        }

        private Transform Tf
        {
            get
            {
                if (_transform == null)
                    _transform = transform;

                return _transform;
            }
        }

        public float mass = 1f;
        public bool useGravity = true;
        public float friction = 0.05f;
        public Vector3 constantAcceleration = Vector3.zero;
        public bool initialForce = false;
        public bool initialForceIsInLocalSpace = false;
        public Vector3 initialForceValue;

        private Transform _transform;
        private Vector3 _velocity;
        private Vector3 _acceleration;
        private Vector3 _forces = Vector3.zero;
        private Transform _tf;

        private void Start()
        {
            _tf = transform;
            
            if (initialForce)
            {
                Vector3 force;
                if (initialForceIsInLocalSpace)
                    force = initialForceValue.x * _tf.right + initialForceValue.y * _tf.up + initialForceValue.z * _tf.forward;
                else
                    force = initialForceValue;
                
                AddForce(force);
            }
        }

        private void Update()
        {
            _velocity += constantAcceleration + _forces * Time.deltaTime;
            
            if (useGravity)
                _velocity += Physics.gravity * Time.deltaTime;
            
            _velocity -= friction * Time.deltaTime * mass * _velocity;

            _forces = Vector3.zero;

            Tf.position += _velocity * Time.deltaTime;
        }
        
        public void AddForce(Vector3 force)
        {
            _forces += force / mass;
        }
        
        // Reflect vector code (see the scene in the folder "4. LaserReflect" for demo)
        private Vector3 Reflect(Vector3 direction, Vector3 normal)
        {
            float projectedVectorOnNormal = Vector3.Dot(direction, normal);
            direction -= (projectedVectorOnNormal * normal).normalized;
            return direction;
        }
    }
}