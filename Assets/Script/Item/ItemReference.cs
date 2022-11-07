using Script.GameManager;
using ScriptableObjectArchitecture;
using Syrinj;
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
        [SerializeField]private GameManagerSO game;
        public WeaponSO weaponSo { get; set; }

        public void SetValues(WeaponSO weapon)
        {
            icon.sprite = weapon.icon;
    
            weaponSo = weapon;

            nameText.text = weapon.name;

            damageText.text = weapon.damage.ToString();

            cadenceText.text = weapon.cadence.ToString();

            ammunationText.text = weapon.ammo.ToString();

        }
    
        //Caso o dinheiro da loja n seja decrementado provavelmente é por conta da referência de coins do scriptableObject do gameManager
        public void DamageUpgrade(WeaponSO weapon)
        {
            weapon = weaponSo;
            if (game.coins.Value >= 10)
            {
                weapon.damage += 5;
                damageText.text = weapon.damage.ToString();
                game.coins.Value -= 10;
            }
        }

        public void CadenceUpgrade(WeaponSO weapon)
        {
            weapon = weaponSo;

            if (game.coins.Value >= 10)
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
                    game.coins.Value -= 10;
                }
            }
        }

        public void AmmunationUpgrade(WeaponSO weapon)
        {
            weapon = weaponSo;

            if (game.coins.Value >= 10)
            {
                weapon.ammo += 5;
                ammunationText.text = weapon.ammo.ToString();
                
            }
        }

    }
}

