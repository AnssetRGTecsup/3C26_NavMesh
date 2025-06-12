using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.AI.Navigation.Samples
{
    /// <summary>
    /// NavMeshSurface that updates only once per frame upon request
    /// </summary>
    [RequireComponent(typeof(NavMeshSurface))]
    public class GloballyUpdatedNavMeshSurface : MonoBehaviour
    {
        static bool s_NeedsNavMeshUpdate;

        public static event Action OnUpdate;
        public static Action onUpdate;

        NavMeshSurface m_Surface;

        public static void RequestNavMeshUpdate()
        {
            OnUpdate?.Invoke();
        }

        void Start()
        {
            m_Surface = GetComponent<NavMeshSurface>();
            m_Surface.BuildNavMesh();
        }

        private void OnEnable()
        {
            OnUpdate += UpdateNavMeshSurface;

            onUpdate = UpdateNavMeshSurface;
        }

        private void OnDisable()
        {
            OnUpdate -= UpdateNavMeshSurface;

            onUpdate = null;
        }

        private void UpdateNavMeshSurface()
        {
            m_Surface.UpdateNavMesh(m_Surface.navMeshData);
        }
    }
}
