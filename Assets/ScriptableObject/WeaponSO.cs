using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Create Weapon")]
public class WeaponSO : ScriptableObject
{
    [Header("Item description")]
    public Sprite icon;
    public string name;
    
    [Header("Default Value")]
    public int defaultAmmo;
    private int defaultDamage;
    private int defaultCadence;
    private int defaultMaxAmmo;
    
    [Header("New Value")]
    public int damage;
    public int cadence;
    public int ammo;
    public int maxAmmo;

    [Header("Last Upgrade Value")]
    public int lastCadence;

    private void OnEnable()
    {
        damage = defaultDamage;
        cadence = defaultCadence;
        ammo = defaultAmmo;
        maxAmmo = defaultMaxAmmo;
    }
}
