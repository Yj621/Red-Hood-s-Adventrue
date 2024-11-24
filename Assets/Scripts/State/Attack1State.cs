using UnityEngine;

public class Attack1State : IState
{
    PlayerController player;

    public Attack1State(PlayerController player)
    {
        this.player = player;
    }
    public void Enter()
    {
        player.GetComponent<Animator>().SetTrigger("Attack1");
    }
    public void Exit()
    {

    }
    public void Execute()
    {

    }
}
