using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Create Weapon")]
public class WeaponSO : ScriptableObject
{
    [Header("Item description")]
    public Sprite icon;
    public string name;
    
    [Header("Default Value")]
    [SerializeField]private int defaultDamage;
    [SerializeField]private int defaultCadence;
    [SerializeField]private int defaultAmmo;
    
    [Header("New Value")]
    public int damage;
    public int cadence;
    public int ammo;

    [Header("Last Upgrade Value")]
    public int lastCadence;

    private void OnEnable()
    {
        damage = defaultDamage;
        cadence = defaultCadence;
        ammo = defaultAmmo;
    }
}
