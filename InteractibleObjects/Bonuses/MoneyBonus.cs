using UnityEngine;

public class MoneyBonus : InteractibleObjects
{
    [SerializeField] private GameObject _meshedObject;
    [SerializeField] private int _moneyCount;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == GameConstans.PlayerTag)
        {
            other.transform.TryGetComponent(out MainPlayerController controller);
            if (controller != null)
                controller.Money.AddMoney(_moneyCount);
            gameObject.SetActive(false);
        }
    }
    public override void Update()
    {
        base.Update();
        _meshedObject.transform.Rotate(Vector3.up, 100 * Time.deltaTime);
    }
}
