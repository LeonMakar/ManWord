using System.Collections.Generic;
using UnityEngine;

public abstract class InteractibleObjects : MonoBehaviour
{
    [SerializeField] protected GameObject ParentObject;
    [SerializeField] protected int Speed;

    [Header("Optimization")]
    [SerializeField] private List<SkinnedMeshRenderer> _scinnedMeshRendere;
    [SerializeField] private List<MeshRenderer> _meshRendere;



    private void Awake()
    {
        var propertyBlock = new MaterialPropertyBlock();
        if (_scinnedMeshRendere.Count > 0)
        {
            foreach (var scinMesh in _scinnedMeshRendere)
            {
                scinMesh.SetPropertyBlock(propertyBlock);
            }
        }
        else
        {
            foreach (var mesh in _meshRendere)
            {
                mesh.SetPropertyBlock(propertyBlock);
            }
        }


    }
    /// <summary>
    /// Moove Forward current Interactible object
    /// </summary>
    public virtual void Update()
    {
        ParentObject.transform.Translate(Vector3.forward * Speed * Time.deltaTime);
        if (ParentObject.gameObject.transform.position.z < -40)
            ParentObject.gameObject.SetActive(false);
    }
}
