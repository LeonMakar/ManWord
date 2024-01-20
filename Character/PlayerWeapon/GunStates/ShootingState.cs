﻿using System;
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using TMPro;


public class ShootingState : BaseState<GunStateMachine.GunStates>
{
    private readonly CharacterActions _inputActions;
    private bool _canShoot = true;
    private bool _isOnShootingState = false;
    private Gun _gun; // also used for MonoBehevior methods


    public ShootingState(GunStateMachine.GunStates stateKey, Gun gun, CharacterActions inputActions) : base(stateKey)
    {
        _gun = gun;
        _inputActions = inputActions;
    }

    public override void EnterToState()
    {
        _isOnShootingState = true;
        _gun.GunParticles.StartCoroutine(Shooting(_gun.GunData.RateOfFire));
        _gun.AudioSource.clip = _gun.GunData.ShootSound;
    }

    public override void ExitFromState()
    {
        _isOnShootingState = false;
        _canShoot = false;
        //_gun.GunParticles.StopAllCoroutines();
        IsTransitionStart = false;
    }

    public override void UpdateState()
    {
        if (_inputActions.Default.Move.WasPressedThisFrame() && _gun.IsGameStarted)
        {
            _canShoot = false;
            _gun.LineRenderer.enabled = false;
        }
        else if (_inputActions.Default.Move.WasReleasedThisFrame() || !_gun.IsGameStarted)
        {
            _canShoot = true;
            _gun.LineRenderer.enabled = true;
        }
        if (_canShoot && _gun.TargetIsFinded && _isOnShootingState && !_inputActions.Default.Move.IsPressed())
            Shoot();
    }

    private void Shoot()
    {
        Ray ray = CreateRay();
        if (Physics.Raycast(ray, out RaycastHit hit, 100, _gun.HitableLayer))
        {
            hit.transform.TryGetComponent(out IShootable shootableObject);
            shootableObject?.Attacked(_gun.GunDamage);
            _gun.GunParticles.ActivateHitParticles(hit);
            _gun.GunParticles.ActivateTrailParticles(hit);
        }

        // ---- Visual and Audio effects ---- //
        _gun.MainPlayerController.AnimateShoot();
        _gun.GunParticles.ActivateParticlesAfterShoot();
        //_gun.GunTrail.SetShootPoint(GetSprededPoint(_gun.BulletAimPoint.position));
        PlayShootAudio();


        if (_gun.BulletAmmount == 0 && !IsTransitionStart)
        {
            IsTransitionStart = true;
            ChangeStateAction.Invoke(GunStateMachine.GunStates.Reloading);
        }

        _gun.BulletAmmount--;
        _canShoot = false;
    }

    private IEnumerator Shooting(float rateOfFire)
    {
        while (true)
        {
            if (!_canShoot)
            {
                yield return new WaitForSeconds(rateOfFire);
                _canShoot = true;
            }
            else
                yield return null;

        }
    }

    private Ray CreateRay()
    {
        var direction = GetSprededPoint(_gun.RawEndPoint.position - _gun.RawSpawnPoint.position).normalized;
        Ray ray = new Ray(_gun.RawSpawnPoint.position, direction);
        Debug.DrawRay(_gun.RawSpawnPoint.position, direction * 150, Color.red, 2);
        return ray;
    }
    private Vector3 GetSprededPoint(Vector3 direction)
    {
        direction.x = UnityEngine.Random.Range(direction.x - _gun.GunData.GunSpred.x, direction.x + _gun.GunData.GunSpred.x);
        direction.y = UnityEngine.Random.Range(direction.y - _gun.GunData.GunSpred.y, direction.y + _gun.GunData.GunSpred.y);
        direction.z = UnityEngine.Random.Range(direction.z - _gun.GunData.GunSpred.z, direction.z + _gun.GunData.GunSpred.z);

        return direction;
    }
    private void PlayShootAudio()
    {
        if (Time.timeScale == 1)
            _gun.AudioSource.pitch = UnityEngine.Random.Range(0.7f, 1);
        else
            _gun.AudioSource.pitch = UnityEngine.Random.Range(0.2f, 0.4f);
        _gun.AudioSource.Play();
    }
}
