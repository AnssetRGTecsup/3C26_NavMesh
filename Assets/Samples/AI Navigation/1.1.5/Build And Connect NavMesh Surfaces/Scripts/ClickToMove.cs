using UnityEngine;
using UnityEngine.AI;

namespace Unity.AI.Navigation.Samples
{
    /// <summary>
    /// Use physics raycast hit from mouse click to set agent destination 
    /// </summary>
    [RequireComponent(typeof(NavMeshAgent))]
    public class ClickToMove : MonoBehaviour
    {
        [SerializeField] private Transform pivot;
        [SerializeField] private LayerMask layers;

        NavMeshAgent m_Agent;
        RaycastHit m_HitInfo = new RaycastHit();

        bool _follow = false;
    
        void Start()
        {
            m_Agent = GetComponent<NavMeshAgent>();
        }
    
        void Update()
        {
            if (Input.GetMouseButton(0))
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray.origin, ray.direction, out m_HitInfo, Mathf.Infinity, layers) && !Input.GetKey(KeyCode.LeftShift))
                {
                    m_Agent.destination = m_HitInfo.point;
                }
            }
            


            /*if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                ChangeFollowCondition();
            }

            MoveObject();

            if (_follow)
            {
               m_Agent.destination = pivot.position;
            }*/
        }

        public void ChangeFollowCondition()
        {
            _follow = !_follow;
        }

        public void MoveObject()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out m_HitInfo, Mathf.Infinity, layers))
            {
                pivot.position = m_HitInfo.point;
            }
        }
    }
}