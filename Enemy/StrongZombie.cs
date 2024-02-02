using Zenject;

public class StrongZombie : Zombie
{
    public override void DethActions()
    {
        gameObject.SetActive(false);
        ResetAllParameters();
    }

    public class Factory : PlaceholderFactory<StrongZombie>
    {

    }
}
