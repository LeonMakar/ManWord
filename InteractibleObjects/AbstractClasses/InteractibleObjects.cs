using UnityEngine;

public abstract class InteractibleObjects : MonoBehaviour
{
    [SerializeField] protected GameObject ParentObject;
    [SerializeField] protected int Speed;

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
