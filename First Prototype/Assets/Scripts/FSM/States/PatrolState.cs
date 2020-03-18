using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.FSM.States
{
    [CreateAssetMenu(fileName ="PatrolState", menuName ="Unity-FSM/States/Patrol", order =2)]
    public class PatrolState : AbstractFSMState
    {
        Waypoint[] _patrolPoints;
        int _patrolPointIndex;

        public override void onEnable()
        {
            base.onEnable();
            StateType = FSMStateType.PATROL;
            _patrolPointIndex = -1; //33:43 - starting state on fsm?
        }

        public override bool EnterState()
        {
            EnteredState = false;
            if (base.EnterState())
            {
                _patrolPoints = _npc.PatrolPoints;

                if (_patrolPoints == null || _patrolPoints.Length == 0)
                {
                    Debug.LogError("PatrolState: Failed to grab patrol points from NPC");
                }
                else
                {
                    if (_patrolPointIndex < 0)
                    {
                        _patrolPointIndex = UnityEngine.Random.Range(0, _patrolPoints.Length);
                    }
                    else
                    {
                        _patrolPointIndex = (_patrolPointIndex + 1) % _patrolPoints.Length;
                    }

                    SetDestination(_patrolPoints[_patrolPointIndex]);
                    EnteredState = true;
                    Debug.Log("Entered Patrol State");
                }
            }
            return EnteredState;
        }
        public override void UpdateState()
        {
            if(EnteredState)
            {
                
                if (Vector3.Distance(_navMeshAgent.transform.position, _patrolPoints[_patrolPointIndex].transform.position) <= 1f)
                {
                    
                    _fsm.EnterState(FSMStateType.IDLE);
                }
            }
        }

        private void SetDestination(Waypoint destination)
        {
            if (_navMeshAgent != null && destination != null)
            {
                _navMeshAgent.SetDestination(destination.transform.position);
            }
        }
    }
}
