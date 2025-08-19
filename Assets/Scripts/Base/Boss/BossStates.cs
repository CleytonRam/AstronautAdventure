using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.StateMachine;


namespace Boss { 

    public class BossStateBase : StateBase
    {
        protected BossBase boss;
        public override void OnStateEnter(params object[] objs)
        {
            base.OnStateEnter(objs);
            boss = (BossBase)objs[0];
        }
    }
    public class BossStateInit : BossStateBase
    {
      public override void OnStateEnter(params object[] objs)
        {
            base.OnStateEnter(objs);
            boss.StartInitAnimation();
            Debug.Log("Boss: " + boss);
        }
    }
    public class BossStateWalk : BossStateBase
    {
        public override void OnStateEnter(params object[] objs)
        {
            base.OnStateEnter(objs);
            boss.GoToRandomPoint(OnArrive);
            Debug.Log("Boss walking");
        }

        private void OnArrive() 
        { 
            boss.SwitchState(BossAction.ATTACK);
        }
        public override void OnStateExit()
        {
            Debug.Log("Exit walk");

            base.OnStateExit();
            boss.StopAllCoroutines();
        }
    }
    public class BossStateAttack : BossStateBase
    {
        public override void OnStateEnter(params object[] objs)
        {
            base.OnStateEnter(objs);
            boss.StartAttack(EndAttacks);

        }
        private void EndAttacks()
        {
            boss.SwitchState(BossAction.WALK);
        }
        public override void OnStateExit()
        {
            Debug.Log("Exit atack");
            base.OnStateExit();
            boss.StopAllCoroutines();
        }

    }


    public class BossStateDeath : BossStateBase
    {
        public override void OnStateEnter(params object[] objs)
        {
            base.OnStateEnter(objs);
            Debug.Log("Enter death");

            boss.transform.localScale = Vector3.one * .2f; // Example of death effect
        }
    }

}
