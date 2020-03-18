using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.FSM.States
{
    [CreateAssetMenu(fileName ="IdleState", menuName ="Unity-FSM/States/Idle", order =1)]
    public class IdleState : AbstractFSMState
    {
        [SerializeField]
        float _idleDuration = 3f;

        float _totalDuration;
        public override void onEnable()
        {
            base.onEnable();
            StateType = FSMStateType.IDLE;
        }

        public override bool EnterState()
        {
            EnteredState = base.EnterState();
            if (EnteredState)
            {
                Debug.Log("Entered Idle State");
                _totalDuration = 0f;
            }
            return EnteredState;
        }
        public override void UpdateState()
        {
            if (EnteredState)
            {
                //Increment _totalDuration while the NPC is waiting
                _totalDuration += Time.deltaTime;

                Debug.Log("Updating Idle State: " + _totalDuration + " seconds.");

                //Change to Patrol state once the NPC has waited long enough
                if(_totalDuration >= _idleDuration)
                {
                    _fsm.EnterState(FSMStateType.PATROL);
                }
            }
        }

        public override bool ExitState()
        {
            base.ExitState();
            Debug.Log("Exiting Idle State");
            return true;
        }
    }
}
