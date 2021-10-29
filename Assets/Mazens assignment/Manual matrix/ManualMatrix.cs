using System;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace FG
{
    [Serializable]
    public struct MatrixFourByFour
    {
        [Header("Row 1")]
        public float m00;
        public float m01;
        public float m02;
        public float m03;
        
        [Header("Row 2")]
        public float m10;
        public float m11;
        public float m12;
        public float m13;
        
        [Header("Row 3")]
        public float m20;
        public float m21;
        public float m22;
        public float m23;
        
        [Header("Row 4")]
        public float m30;
        public float m31;
        public float m32;
        public float m33;

        public static MatrixFourByFour Identity
        {
            get => new MatrixFourByFour
            {
                m00 = 1f,
                m01 = 0f,
                m02 = 0f,
                m03 = 0f,
                
                m10 = 0f,
                m11 = 1f,
                m12 = 0f,
                m13 = 0f,
                
                m20 = 0f,
                m21 = 0f,
                m22 = 1f,
                m23 = 0f,
                
                m30 = 0f,
                m31 = 0f,
                m32 = 0f,
                m33 = 1f
            };
        }

        public Vector3 RightVector
        {
            get => new Vector3(m00, m10, m20);

            set
            {
                m00 = value.x;
                m10 = value.y;
                m20 = value.z;
            }
        }
        
        public Vector3 UpVector
        {
            get => new Vector3(m01, m11, m21);

            set
            {
                m01 = value.x;
                m11 = value.y;
                m21 = value.z;
            }
        }
        
        public Vector3 ForwardVector
        {
            get => new Vector3(m02, m12, m22);

            set
            {
                m02 = value.x;
                m12 = value.y;
                m22 = value.z;
            }
        }

        public Vector3 OriginPoint
        {
            get => new Vector3(m03, m13, m23);

            set
            {
                m03 = value.x;
                m13 = value.y;
                m23 = value.z;
            }
        }

        public static MatrixFourByFour operator*(MatrixFourByFour a, MatrixFourByFour b)
        {
            Vector3 bx = b.RightVector;
            Vector3 by = b.UpVector;
            Vector3 bz = b.ForwardVector;
            Vector3 bOrigin = b.OriginPoint;

            MatrixFourByFour matrix = Identity;

            matrix.RightVector = a.MultiplyVector3x4(bx);
            matrix.UpVector = a.MultiplyVector3x4(by);
            matrix.ForwardVector = a.MultiplyVector3x4(bz);
            matrix.OriginPoint = a.MultiplyPoint3x4(bOrigin);

            return matrix;
        }

        public Vector3 MultiplyPoint3x4(Vector3 input)
        {
            return OriginPoint + MultiplyVector3x4(input);
        }

        public Vector3 MultiplyVector3x4(Vector3 input)
        {
            return input.x * RightVector + input.y * UpVector + input.z * ForwardVector;
        }
    }

    public class ManualMatrix : MonoBehaviour
    {
        public float sphereRadius = 0.1f;
        
        public MatrixFourByFour a = MatrixFourByFour.Identity;
        public Vector3 aRotation;
        public Vector3 positionASpace;
        public Color aColor;
        
        public MatrixFourByFour b = MatrixFourByFour.Identity;
        public Vector3 bRotation;
        public Vector3 positionBSpace;
        public Color bColor;

        private void OnDrawGizmos()
        {
            Quaternion test;
            Vector3 originA = a.OriginPoint;
            
            Gizmos.color = aColor;
            Gizmos.DrawSphere(a.MultiplyPoint3x4(positionASpace), sphereRadius);

            Gizmos.color = Color.red;
            Gizmos.DrawLine(originA, originA + a.RightVector);
            
            Gizmos.color = Color.green;
            Gizmos.DrawLine(originA, originA + a.UpVector);
            
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(originA, originA + a.ForwardVector);

            MatrixFourByFour transformedB = a * b;
            Vector3 originB = transformedB.OriginPoint;
            
            Gizmos.color = bColor;
            Gizmos.DrawSphere(transformedB.MultiplyPoint3x4(positionBSpace), sphereRadius);

            Gizmos.color = Color.red;
            Gizmos.DrawLine(originB, originB + transformedB.RightVector);
            
            Gizmos.color = Color.green;
            Gizmos.DrawLine(originB, originB + transformedB.UpVector);
            
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(originB, originB + transformedB.ForwardVector);
        }

        private void OnValidate()
        {
            EulerToBasisVectors(aRotation, out Vector3 right, out Vector3 up, out Vector3 forward);

            a.RightVector = right;
            a.UpVector = up;
            a.ForwardVector = forward;
            
            EulerToBasisVectors(bRotation, out right, out up, out forward);
            
            b.RightVector = right;
            b.UpVector = up;
            b.ForwardVector = forward;
        }

        public static void EulerToBasisVectors(Vector3 eulerAngles, out Vector3 right, out Vector3 up, out Vector3 forward)
        {
            Vector2 xVector = AngToDir(Mathf.Deg2Rad * eulerAngles.x);
            Vector2 yVector = AngToDir(Mathf.Deg2Rad * eulerAngles.y);
            Vector2 zVector = AngToDir(Mathf.Deg2Rad * eulerAngles.z);

            forward = (new Vector3(0f, -xVector.y, xVector.x) + new Vector3(yVector.y, 0f, yVector.x)).normalized;
            up = (new Vector3(0f, xVector.x, xVector.y) + new Vector3(-zVector.y, zVector.x, 0f)).normalized;
            right = Vector3.Cross(up, forward);
        }
        
        public static Vector2 AngToDir( float aRad ) => new Vector2( Mathf.Cos( aRad ), Mathf.Sin( aRad ) );
    }
}