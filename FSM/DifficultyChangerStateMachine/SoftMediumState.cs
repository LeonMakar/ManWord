public class SoftMediumState : DifficultyState
{
    public SoftMediumState(DifficultyChangerStateMachine.DificultyStage key, DifficultyChangerStateMachine stateMachine, DifficultyChangerStateMachine.DificultyStage stateToTransit) 
        : base(key, stateMachine, stateToTransit)
    {
        GoldToAddWhenWaveDone = 5;

    }
}

