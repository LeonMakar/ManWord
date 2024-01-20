using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunStateMachine : StateManager<GunStateMachine.GunStates>
{
    public enum GunStates
    {
        Shooting,
        Reloading
    }

    public void InitStateMachine(Gun gun, CharacterActions inputActions)
    {
        ShootingState shootingstate = new ShootingState(GunStateMachine.GunStates.Shooting, gun, inputActions);
        ReloadingState reloading = new ReloadingState(GunStates.Reloading, gun);

        States.Add(GunStates.Shooting, shootingstate);
        States.Add(GunStates.Reloading, reloading);
        

        StartStateMachine(GunStates.Shooting);
    }
}
