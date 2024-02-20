using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Optimization : MonoBehaviour
{
    [SerializeField] private List<MeshRenderer> _meshRendere;
    [SerializeField] private SkinnedMeshRenderer _skinnedMeshRendere;
    void Start()
    {
        MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();
        if (_meshRendere.Count == 0)
            _skinnedMeshRendere.SetPropertyBlock(propertyBlock);
        else
            foreach (var mesh in _meshRendere)
                mesh.SetPropertyBlock(propertyBlock);



    }


}
