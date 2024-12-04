using UnityEngine;

public class SlideState : IState
{
    PlayerController player;

    public SlideState(PlayerController player)
    {
        this.player = player;
    }
    public void Enter()
    {
        player.GetComponent<Animator>().SetTrigger("Slide");
    }
    public void Exit()
    {

    }
    public void Execute()
    {

    }
}
