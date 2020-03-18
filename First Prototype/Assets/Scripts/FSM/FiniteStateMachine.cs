using Assets.Scripts.FSM.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.FSM
{
    public class FiniteStateMachine : MonoBehaviour
    {
        AbstractFSMState _currentState;

        [SerializeField]
        List<AbstractFSMState> _validStates;
        Dictionary<FSMStateType, AbstractFSMState> _fsmStates;

        public void Awake()
        {
            _currentState = null;

            _fsmStates = new Dictionary<FSMStateType, AbstractFSMState>();

            NavMeshAgent navMeshAgent = this.GetComponent<NavMeshAgent>();

            Assets.Scripts.FSM.Code.NPC npc = this.GetComponent<Assets.Scripts.FSM.Code.NPC>();
            //_fsmStates.Clear();
            var arrayOfAllKeys = _fsmStates.ToArray();
            int count = 0;
            if(!_validStates.Any())
            {
                Debug.Log("EMPTY LIST");
            }
            foreach (AbstractFSMState state in _validStates)
            {
                //Debug.Log("STARTING: " + count);
                //Debug.Log("State: " + state.StateType);
                //Debug.Log("_VALIDSTATES: " + count + ": " + _validStates[count]);
                state.SetExecutingFSM(this);
                state.SetExecutingNPC(npc);
                state.SetNavMeshAgent(navMeshAgent);

                if (state.GetType() == typeof(PatrolState))
                {
                    state.StateType = FSMStateType.PATROL;
                }
                if (state.GetType() == typeof(IdleState))
                {
                    state.StateType = FSMStateType.IDLE;
                }

                if (!_fsmStates.ContainsKey(state.StateType))
                {
                    _fsmStates.Add(state.StateType, state);
                }
                count++;
            }
            
        }

        public void Start()
        {
            EnterState(FSMStateType.IDLE);
        }

        public void Update()
        {
            if(_currentState != null)
            {
                _currentState.UpdateState();
            }
        }

        #region STATE MANAGEMENT

        public void EnterState(AbstractFSMState nextState)
        {
            if (nextState == null)
            {
                return;
            }
            if(_currentState != null)
            {
                _currentState.ExitState();
            }
            _currentState = nextState;
            _currentState.EnterState();
        }

        public void EnterState(FSMStateType stateType)
        {
            
            if (_fsmStates.ContainsKey(stateType))
            {
                AbstractFSMState nextState = _fsmStates[stateType];
                EnterState(nextState);
            }
        }

        #endregion
    }
}
