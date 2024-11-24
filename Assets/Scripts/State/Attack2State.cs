using UnityEngine;

public class Attack2State: IState
{
    PlayerController player;

    public Attack2State(PlayerController player){
        this.player = player;
    }
    public void Enter()
    {
        player.GetComponent<Animator>().SetTrigger("Attack2");
    }
    public void Exit()
    {

    }
    public void Execute()
    {

    }
}
