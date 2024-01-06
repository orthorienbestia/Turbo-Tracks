using System;
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
        public Transform target;
        public Transform[] milestoneGroups;
        
        public Transform[][] _milestonesByGroup;
        
        public Queue<Transform> _currentGamePath = new Queue<Transform>();
        
        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            _milestonesByGroup = milestoneGroups.Select(milestoneGroup => milestoneGroup.GetComponentsInChildren<Transform>().Where(t=> t!=milestoneGroup).ToArray()).ToArray();
        }

        private void Start()
        {
            int lapCount = PlayerPrefs.GetInt(AppConstants.LapCountPrefKey, 1);
            
            for (int i = 0; i < lapCount; i++)
            {
                foreach (var milestoneGroup in _milestonesByGroup)
                {
                    _currentGamePath.Enqueue(milestoneGroup.ToList().GetRandomItem());
                }
            }
            
            _currentGamePath.Enqueue(_milestonesByGroup[0].ToList().GetRandomItem());
        }

        private void Update()
        {
            if (_currentGamePath.Count == 0)
            {
                return;
            }
            target = _currentGamePath.Peek();
            if (Vector3.Distance(transform.position, target.position) < 4f)
            {
                _currentGamePath.Dequeue();
                if (_currentGamePath.Count == 0)
                {
                    return;
                }
                target = _currentGamePath.Peek();
            }
            if(target != null)
                    agent.SetDestination(target.position);
        }
    }
}
