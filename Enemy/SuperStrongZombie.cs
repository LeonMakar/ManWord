using Zenject;

public class SuperStrongZombie : Zombie
{
    public override void DethActions()
    {
        gameObject.SetActive(false);
        ResetAllParameters();
    }

    public class Factory : PlaceholderFactory<SuperStrongZombie>
    {

    }
}
