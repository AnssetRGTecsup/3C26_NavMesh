using DG.Tweening;
using System.Collections;
using Unity.AI.Navigation.Samples;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlatformerModifier : MonoBehaviour
    {
        private Transform objectTransform;
        Sequence sequence;

        private void Awake()
        {
            objectTransform = transform;
        }

        private void Start()
        {
            sequence = DOTween.Sequence();

            sequence.Append(objectTransform.DOMove(transform.position + transform.forward * 5f, 1f)).OnComplete(UpdateNavMesh);
            sequence.Append(objectTransform.DOMove(transform.position - transform.forward * 5f, 1f)).onComplete += UpdateNavMesh;

            sequence.SetLoops(-1);
        }

        private void OnDisable()
        {
            sequence.Kill();
        }

        void FixedUpdate()
        {
            UpdateNavMesh();
        }

        private void UpdateNavMesh()
        {
            GloballyUpdatedNavMeshSurface.RequestNavMeshUpdate();
        }
    }
}