using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class StartShoot : MonoBehaviour, IInjectable
{
    private MainPlayerController controller;

    [Inject]
    public void Construct(MainPlayerController controller)
    {
        this.controller = controller;
    }

    public void CanStartShoot() => controller.IdleanimationStarted();
}
