using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Create Weapon")]
public class WeaponSO : ScriptableObject
{
    [Header("Item description")]
    public Sprite icon;
    public string name;
    
    [Header("Default Value")]
    public int defaultMaxAmmo;
    public int defaultAmmo;
    public int defaultDamage;
    public int defaultCadence;
    
    [Header("New Value")]
    public int maxAmmo;
    public int ammo;
    public int damage;
    public int cadence;

    [Header("Last Upgrade Value")] 
    public int lastAmmo;
    public int lastDamage;
    public int lastCadence;

    private void OnEnable()
    {
        DefaultAttributes();
    }
    public void DefaultAttributes()
    {
        maxAmmo = defaultMaxAmmo;
        ammo = defaultAmmo;
        damage = defaultDamage;
        cadence = defaultCadence;
    }
}
