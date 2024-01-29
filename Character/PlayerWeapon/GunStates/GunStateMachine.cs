using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunStateMachine : StateManager<GunStateMachine.GunStates>
{
    private Gun _gun;

    public enum GunStates
    {
        Shooting,
        Reloading
    }

    public void InitStateMachine(Gun gun, CharacterActions inputActions)
    {
        ShootingState shootingState = new ShootingState(GunStateMachine.GunStates.Shooting, gun, inputActions);
        ReloadingState reloading = new ReloadingState(GunStates.Reloading, gun);
        _gun = gun;

        States.Add(GunStates.Shooting, shootingState);
        States.Add(GunStates.Reloading, reloading);
    }

    public void StartStateMachine()
    {
        _gun.StopAllCoroutines();
        StartStateMachine(GunStates.Shooting);
    }



}
