public partial class DifficultyChangerStateMachine
{
    public class HardMediumState : DifficultyState
    {
        public HardMediumState(DificultyStage key, DifficultyChangerStateMachine stateMachine, DificultyStage stateToTransit) : base(key, stateMachine, stateToTransit)
        {
            GoldToAddWhenWaveDone = 10;
        }
    }
}

