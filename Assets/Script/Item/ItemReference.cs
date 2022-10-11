using Script.GameManager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Item
{
    public class ItemReference : MonoBehaviour
    {
        [SerializeField]private TextMeshProUGUI nameText;
        [SerializeField]private TextMeshProUGUI damageText;
        [SerializeField]private TextMeshProUGUI cadenceText;
        [SerializeField]private TextMeshProUGUI ammunationText;
        [SerializeField]private Image icon;
        public WeaponSO weaponSo { get; set; }
        private WeaponSO test;
    
        public void SetValues(WeaponSO weapon)
        {
            icon.sprite = weapon.icon;
    
            weaponSo = weapon;

            nameText.text = weapon.name;

            damageText.text = weapon.damage.ToString();

            cadenceText.text = weapon.cadence.ToString();

        }
    
        public void damageUpgrade(WeaponSO weapon)
        {
            weapon = weaponSo;
            if (Game.coins >= 10)
            {
                weapon.damage += 5;
                damageText.text = weapon.damage.ToString();
                Game.coins -= 10;
            }
        }

        public void cadenceUpgrade(WeaponSO weapon)
        {
            weapon = weaponSo;

            if (Game.coins >= 10)
            {
                weapon.cadence -= 5;
                cadenceText.text = weapon.cadence.ToString();
            
                if (weapon.cadence < weapon.lastCadence)
                {
                    cadenceText.text = weapon.lastCadence.ToString();
                    weapon.cadence = weapon.lastCadence;
                }
                else
                {
                    Game.coins -= 10;
                }
            }
        }

    }
}

