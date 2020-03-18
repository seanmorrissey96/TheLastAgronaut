using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.FSM.Code
{
    [RequireComponent(typeof(NavMeshAgent), typeof(FiniteStateMachine))]
    public class NPC : MonoBehaviour
    {

        [SerializeField]
        Waypoint[] _patrolPoints;

        NavMeshAgent _navMeshAgent;
        FiniteStateMachine _finiteStateMachine;


        public void Awake()
        {
            _navMeshAgent = this.GetComponent<NavMeshAgent>();
            _finiteStateMachine = this.GetComponent<FiniteStateMachine>();
        }

        public void Start()
        {
            
        }

        public void Update()
        {
            
        }
        public Waypoint[] PatrolPoints
        {
            get
            {
                return _patrolPoints;
            }
        }
    }
}
