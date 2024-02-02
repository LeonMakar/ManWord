using Zenject;

public class FastZombie : Zombie
{
    public override void DethActions()
    {
        gameObject.SetActive(false);
        ResetAllParameters();
    }

    public class Factory : PlaceholderFactory<FastZombie>
    {

    }
}
