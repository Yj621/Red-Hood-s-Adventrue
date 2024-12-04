using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public IState CurrentState { get; private set; }
    PlayerController player;

    public IdleState idleState;
    public DeadState deadState;
    public Attack1State attack1State;
    public Attack2State attack2State;
    public HurtState hurtState;
    public BowAttackState bowAttackState;
    public JumpState jumpState;
    public RunState runState;
    public SlideState slideState;

    public StateMachine(PlayerController player)
    {
        this.player = player;
        idleState = new IdleState(player);
        deadState = new DeadState(player);
        attack1State = new Attack1State(player);
        attack2State = new Attack2State(player);
        hurtState = new HurtState(player);
        bowAttackState = new BowAttackState(player);
        jumpState = new JumpState(player);
        runState = new RunState(player);
        slideState = new SlideState(player);
    }

    //초기 State를 받아 CurrentState에 넣고 Enter(state 진입)
    public void Initialize(IState state)
    {
        CurrentState = state;
        state.Enter();
    }

    //바뀔 state를 받아 현재 state는 Exit, CurrentState를 바꾸며 바뀔 state에는 Enter수행
    public void TransitionTo(IState nextState)
    {
        CurrentState.Exit();
        CurrentState = nextState;
        CurrentState.Enter();
    }
    public void Execute()
    {
        CurrentState.Execute();
    }
}
