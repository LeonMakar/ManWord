using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public partial class DifficultyChangerStateMachine : StateManager<DifficultyChangerStateMachine.DificultyStage>
{
    [SerializeField] private TextMeshProUGUI _wavePassedText;

    [Space(10), Header("EnemySpawnChanes"), Tooltip("Register Order SuperEasy,Easy,SoftMedium,Medium,HardMedium,SoftHard,Hard,VeryHard,Epic,Unreal")]
    public List<EnemySpawnChanceData> EnemyChanceData = new List<EnemySpawnChanceData>(10);

    [Space(20), Header("BonusSpawnChanes"), Tooltip("Register Order SuperEasy,Easy,SoftMedium,Medium,HardMedium,SoftHard,Hard,VeryHard,Epic,Unreal")]
    public List<BonusSpawnChanceData> BonusChanceDatas = new List<BonusSpawnChanceData>(10);


    [field: SerializeField, Space(20), Header("Spawners")] public ZombieSpawner ZombieSpawner { get; private set; }
    [field: SerializeField] public BonusSpawner BonusSpawner { get; private set; }
    [field: SerializeField] public ObstacleSpawner ObstacleSpawner { get; private set; }

    [Space(20), Header("TransitionsValues"), Tooltip("Register Order SuperEasy,Easy,SoftMedium,Medium,HardMedium,SoftHard,Hard,VeryHard,Epic,Unreal")]
    public List<int> TransitionValueFromState = new List<int>(10);


    private UIMoneyShower _money;
    [HideInInspector] public int GoldToAdd = 5;

    public void StartWaveCompliteRewarding()
    {
        _wavePassedText.gameObject.SetActive(true);
        _money.AddGold(GoldToAdd);
        StartCoroutine(StartVanish());
    }


    [Inject]
    private void Construct(UIMoneyShower money)
    {
        SuperEasyState superEasyState = new SuperEasyState(DificultyStage.SuperEasy, this, DificultyStage.Easy);
        EasyState easyState = new EasyState(DificultyStage.Easy, this, DificultyStage.SoftMedium);
        SoftMediumState softMediumState = new SoftMediumState(DificultyStage.SoftMedium, this, DificultyStage.Medium);
        MediumState mediumState = new MediumState(DificultyStage.Medium, this, DificultyStage.HardMedium);
        HardMediumState hardMediumState = new HardMediumState(DificultyStage.HardMedium, this, DificultyStage.SoftHard);
        SoftHardState softHardState = new SoftHardState(DificultyStage.SoftHard, this, DificultyStage.Hard);
        HardState hardState = new HardState(DificultyStage.Hard, this, DificultyStage.VeryHard);
        VeryHardState veryHardState = new VeryHardState(DificultyStage.VeryHard, this, DificultyStage.Epic);
        EpicState epicState = new EpicState(DificultyStage.Epic, this, DificultyStage.Unreal);
        UnrealState unrealState = new UnrealState(DificultyStage.Unreal, this, DificultyStage.Unreal);

        States.Add(DificultyStage.SuperEasy, superEasyState);
        States.Add(DificultyStage.Easy, easyState);
        States.Add(DificultyStage.SoftMedium, softMediumState);
        States.Add(DificultyStage.Medium, mediumState);
        States.Add(DificultyStage.HardMedium, hardMediumState);
        States.Add(DificultyStage.SoftHard, softHardState);
        States.Add(DificultyStage.Hard, hardState);
        States.Add(DificultyStage.VeryHard, veryHardState);
        States.Add(DificultyStage.Epic, epicState);
        States.Add(DificultyStage.Unreal, unrealState);

        _money = money;

        StartStateMachine(DificultyStage.SuperEasy);
    }

    private IEnumerator StartVanish()
    {
        Color textColor = _wavePassedText.color;
        textColor.a = 0;
        _wavePassedText.color = textColor;
        while (textColor.a < 1)
        {
            textColor.a += Time.deltaTime;
            _wavePassedText.color = textColor;
            yield return null;
        }
        yield return new WaitForSeconds(3);
        while (textColor.a > 0)
        {
            textColor.a -= Time.deltaTime;
            _wavePassedText.color = textColor;
            yield return null;
        }
    }

    public enum DificultyStage
    {
        SuperEasy = 0,
        Easy = 1,
        SoftMedium = 2,
        Medium = 3,
        HardMedium = 4,
        SoftHard = 5,
        Hard = 6,
        VeryHard = 7,
        Epic = 8,
        Unreal = 9,
    }
}
