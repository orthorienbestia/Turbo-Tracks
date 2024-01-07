using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Utility;
using UnityEngine;
using UnityEngine.AI;

namespace _Project.Scripts
{
    public class AIKartHandler : MonoBehaviour
    {
        private NavMeshAgent agent;
        private Transform target;
        public Transform[] milestoneGroups;

        private Transform[][] _milestonesByGroup;

        private Queue<Transform> _currentGamePath = new();

        public static bool hasReachedTarget;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            _milestonesByGroup = milestoneGroups.Select(milestoneGroup =>
                    milestoneGroup.GetComponentsInChildren<Transform>().Where(t => t != milestoneGroup).ToArray())
                .ToArray();
        }

        private void Start()
        {
            hasReachedTarget = false;
            int lapCount = PlayerPrefs.GetInt(AppConstants.LapCountPrefKey, 1);

            for (int i = 0; i < lapCount; i++)
            {
                foreach (var milestoneGroup in _milestonesByGroup)
                {
                    _currentGamePath.Enqueue(milestoneGroup.ToList().GetRandomItem());
                }
            }
        }

        private void Update()
        {
            if (_currentGamePath.Count == 0)
            {
                hasReachedTarget = true;
                return;
            }

            target = _currentGamePath.Peek();
            if (Vector3.Distance(transform.position, target.position) < 4f)
            {
                _currentGamePath.Dequeue();
                if (_currentGamePath.Count == 0)
                {
                    hasReachedTarget = true;
                    return;
                }

                target = _currentGamePath.Peek();
            }

            if (target != null)
                agent.SetDestination(target.position);
        }
    }
}