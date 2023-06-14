using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBattleState : EnemyState
{
    private Transform player;
    private Enemy_Skeleton enemy;
    private int moveDirection;

    public SkeletonBattleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Skeleton _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        player = GameObject.Find("Player").transform;
    }

    public override void Update()
    {
        base.Update();

        if(enemy.IsPlayerDetected())
        {
            if(enemy.IsPlayerDetected().distance < enemy.attackDistance)
            {
                Debug.Log("dap chet moe may`");
                enemy.ZeroVelocity();
                return;
            }
        }

        if (player.position.x > enemy.transform.position.x)
            moveDirection = 1;
        else if (player.position.x < enemy.transform.position.x)
            moveDirection = -1;

        enemy.SetVelocity(enemy.moveSpeed * moveDirection, rb.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();
    }

}