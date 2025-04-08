using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;
using Ebac.StateMachine;
public class GameManager : Singleton<GameManager>
{
    public enum GameState
    {
        INTRO,
        GAMEPLAY,
        PAUSE,
        WIN,
        LOSE
    }
    public StateMachine<GameState> stateMachine;

    public void Start()
    {
        Init();
    }

    public void Init()
    {
        stateMachine = new StateMachine<GameState>();

        stateMachine.Init();
        stateMachine.RegisterStates(GameState.INTRO, new GMStateIntro());
        stateMachine.RegisterStates(GameState.GAMEPLAY, new StateBase());
        stateMachine.RegisterStates(GameState.PAUSE, new StateBase());
        stateMachine.RegisterStates(GameState.WIN, new StateBase());
        stateMachine.RegisterStates(GameState.LOSE, new StateBase());

        stateMachine.SwitchState(GameState.INTRO);


    }

   
}
