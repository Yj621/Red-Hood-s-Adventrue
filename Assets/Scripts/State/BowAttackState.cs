using UnityEngine;

public class BowAttackState: IState
{
    PlayerController player;

    public BowAttackState(PlayerController player){
        this.player = player;
    }
    public void Enter()
    {
        player.GetComponent<Animator>().SetTrigger("BowAttack");
    }
    public void Exit()
    {

    }
    public void Execute()
    {

    }
}
