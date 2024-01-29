using UnityEngine;
using System.Collections;

public class ReloadingState : BaseState<GunStateMachine.GunStates>
{
    private Gun _gun;
    public ReloadingState(GunStateMachine.GunStates key, Gun gun) : base(key)
    {
        _gun = gun;
    }

    public override void EnterToState()
    {
        _gun.StartCoroutine(Reloading());
        //StartReloadingAnimation;
    }

    public override void ExitFromState()
    {

    }

    public override void UpdateState()
    {

    }

    private IEnumerator Reloading()
    {
        _gun.AudioSource.PlayOneShot(_gun.GunData.ReloadingSound);
        _gun.MainPlayerController.AnimateReload();
        yield return new WaitForSeconds(_gun.ReloadingTime);
        _gun.BulletAmmount = _gun.MaxBulletAmmount;
        ChangeStateAction.Invoke(GunStateMachine.GunStates.Shooting);
    }
}
