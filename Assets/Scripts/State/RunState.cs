using UnityEngine;

public class RunState: IState
{
    PlayerController player;

    public RunState(PlayerController player){
        this.player = player;
    }
    public void Enter()
    {
        player.GetComponent<Animator>().SetTrigger("Run");
    }
    public void Exit()
    {

    }
    public void Execute()
    {

    }
}
