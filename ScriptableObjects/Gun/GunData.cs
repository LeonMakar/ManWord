using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GunData", menuName = "Gun/GunData")]
public class GunData : ScriptableObject
{
    [field: SerializeField] public string GunName { get; private set; }
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public float RateOfFire { get; private set; }
    [field: SerializeField] public Vector3 GunSpred { get; private set; }
    [field: SerializeField] public int BulletAmmount { get; private set; }
    [field: SerializeField] public float ReloadingTime { get; private set; }
    [field: SerializeField] public int GunCoast { get; private set; }
    [field: SerializeField] public AudioClip ShootSound { get; private set; }
    [field: SerializeField] public AudioClip ReloadingSound { get; private set; }
    [field: SerializeField] public GameObject GunPrefab { get; private set; }

}
