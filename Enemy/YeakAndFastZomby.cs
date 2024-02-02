using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class YeakAndFastZomby : Zombie
{
    public override void DethActions()
    {
        gameObject.SetActive(false);
        ResetAllParameters();
    }

    public class Factory : PlaceholderFactory<YeakAndFastZomby>
    {

    }

}
