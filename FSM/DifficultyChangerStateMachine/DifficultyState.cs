using System;
using UnityEngine;

public class DifficultyState : BaseState<DifficultyChangerStateMachine.DificultyStage>
{
    private ZombieSpawner _zombieSpawner;
    private BonusSpawner _bonusSpawner;
    private ObstacleSpawner _obstacleSpawner;
    private EnemySpawnChanceData _enemyChanceData;
    private BonusSpawnChanceData _bonusChanceData;
    private DifficultyChangerStateMachine.DificultyStage _stateToTransit;
    private int _transititonValue;
    private DifficultyChangerStateMachine _stateMachine;
    protected int GoldToAddWhenWaveDone;
    public DifficultyState(DifficultyChangerStateMachine.DificultyStage key, DifficultyChangerStateMachine stateMachine, DifficultyChangerStateMachine.DificultyStage stateToTransit) : base(key)
    {
        _stateMachine = stateMachine;
        _transititonValue = stateMachine.TransitionValueFromState[(int)key];
        _zombieSpawner = stateMachine.ZombieSpawner;
        _bonusSpawner = stateMachine.BonusSpawner;
        _obstacleSpawner = stateMachine.ObstacleSpawner;
        _enemyChanceData = stateMachine.EnemyChanceData[(int)key];
        _bonusChanceData = stateMachine.BonusChanceDatas[(int)key];
        _stateToTransit = stateToTransit;

    }

    public override void EnterToState()
    {
        _zombieSpawner.ChangeEnemySpawnData(_enemyChanceData);
        _bonusSpawner.ChangeBonusSpawnData(_bonusChanceData);
        _obstacleSpawner.ChangeBonusSpawnData(_bonusChanceData);
    }

    public override void ExitFromState()
    {
        _stateMachine.GoldToAdd = GoldToAddWhenWaveDone;
        _stateMachine.StartWaveCompliteRewarding();
    }

    public override void UpdateState()
    {
        if (GameConstans.DiffcultyValue >= _transititonValue)
            ChangeStateAction.Invoke(_stateToTransit);
    }
}
