using Ez;
using UnityEngine;

namespace Script.Item
{
    public class WeaponItem : MonoBehaviour
    {
        [SerializeField]private GameObject weapon;
        [SerializeField]private WeaponSO weaponSo;
        private Merchant merchant;

        void Start()
        {
            merchant = FindObjectOfType<Merchant>();
        }

        void Update()
        {
            transform.Rotate (Vector3.up * 50 * Time.deltaTime, Space.Self);
        }
        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.CompareTag("Player"))
            {
                other.gameObject.Send<IInventory>(_ => _.PickingUpItem(weapon));
                merchant.gameObject.Send<IInventory>(_=>_.PickingUpWeapon(weaponSo));

                Destroy(gameObject);
            }  
        }
    }
}
