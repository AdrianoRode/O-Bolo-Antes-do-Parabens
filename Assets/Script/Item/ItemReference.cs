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

            damageText.text = "Dano: " + weapon.damage.ToString();

            cadenceText.text = "Cadência de tiro: " + weapon.cadence.ToString();

            ammunationText.text = "Capacidade: " + weapon.maxAmmo.ToString();

        }
    
        //Caso o dinheiro da loja n seja decrementado provavelmente é por conta da referência de coins do scriptableObject do gameManager
        public void DamageUpgrade(WeaponSO weapon)
        {
            weapon = weaponSo;
            
            if (game.coins.Value >= 10)
            {
                weapon.damage += 5;
                damageText.text = "Dano: " + weapon.damage.ToString();
                
                if (weapon.damage > weapon.lastDamage)
                {
                    damageText.text = "Dano: " + weapon.lastDamage.ToString();
                    weapon.damage = weapon.lastDamage;
                }
                else
                {
                    game.coins.Value -= 10;
                }
            }
        }

        public void CadenceUpgrade(WeaponSO weapon)
        {
            weapon = weaponSo;

            if (game.coins.Value >= 10)
            {
                weapon.cadence -= 5;
                cadenceText.text = "Cadência de tiro: " + weapon.cadence.ToString();
            
                if (weapon.cadence < weapon.lastCadence)
                {
                    cadenceText.text = "Cadência de tiro: " + weapon.lastCadence.ToString();
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
                weapon.maxAmmo += 5;
                ammunationText.text = "Capacidade: " + weapon.maxAmmo.ToString();

                if (weapon.maxAmmo > weapon.lastAmmo)
                {
                    ammunationText.text = "Capacidade: " + weapon.lastAmmo.ToString();
                    weapon.maxAmmo = weapon.lastAmmo;
                }
                else
                {
                    game.coins.Value -= 10;
                }
            }
        }

    }
}

