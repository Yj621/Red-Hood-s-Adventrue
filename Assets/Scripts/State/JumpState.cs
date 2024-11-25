using UnityEngine;

public class JumpState: IState
{
    PlayerController player;

    public JumpState(PlayerController player)
    {
        this.player = player;
    }
    public void Enter()
    {
        player.GetComponent<Animator>().SetTrigger("Jump");
    }
    public void Exit()
    {

    }
    public void Execute()
    {

    }
}
