using UnityEngine;

public class DeadState: IState
{
    PlayerController player;

    public DeadState(PlayerController player){
        this.player = player;
    }
    public void Enter()
    {
        player.GetComponent<Animator>().SetTrigger("Dead");
    }
    public void Exit()
    {

    }
    public void Execute()
    {

    }
}
