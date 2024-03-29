﻿using UnityEngine;

namespace FG
{
    public class WorldToLocal : MonoBehaviour
    {
        public Vector2 worldPosition;
        public Transform obj;
        private Vector2 localPosition;

        private void OnDrawGizmos()
        {
            localPosition = SpaceTransform.WorldToLocal(worldPosition, transform);
            obj.localPosition = localPosition;
        }
    }
}
