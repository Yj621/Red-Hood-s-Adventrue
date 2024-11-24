using UnityEngine;

public interface IState
{
    //state 진입
    public void Enter();
    //state 취소
    public void Exit();
    //현재 state 수행
    public void Execute();
}
