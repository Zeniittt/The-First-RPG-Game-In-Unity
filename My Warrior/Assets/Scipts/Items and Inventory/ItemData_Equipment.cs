using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType
{
    Weapon,
    Armor,
    Amulet,
    Flask
}

[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Equipment")]
public class ItemData_Equipment : ItemData
{
    public EquipmentType equipmentType;

    [Header("Unique Effect")]
    public float itemCooldown;
    public ItemEffect[] itemEffects;


    [Header("Major Stats")]
    public int strength;       // 1 point increase damage by 1 and crit power 1%
    public int agility;        // 1 point increase evasion by 1% and crit chance by 1%
    public int intelligence;   // 1 point increase magic damage by 1 and magic resistance by 3
    public int vitality;       // 1 point increase health by 3 or 5

    [Header("Offensive Stats")]
    public int damage;
    public int critChance;
    public int critPower;

    [Header("Defensive Stats")]
    public int maxHealth;
    public int armor;
    public int evasion;
    public int magicResistance;

    [Header("Magic Stats")]
    public int fireDamage;
    public int iceDamage;
    public int lightingDamage;

    [Header("Craft requirements")]
    public List<InventoryItem> craftingMaterials;

    public void Effect(Transform _enemyPosition)
    {
        foreach (var item in itemEffects)
        {
            item.ExecuteEffect(_enemyPosition);
        }
    }

    public void AddModifiers()
    {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        playerStats.strength.AddModifier(strength);
        playerStats.agility.AddModifier(agility);
        playerStats.intelligence.AddModifier(intelligence);
        playerStats.vitality.AddModifier(vitality);

        playerStats.damage.AddModifier(damage);
        playerStats.critChance.AddModifier(critChance);
        playerStats.critPower.AddModifier(critPower);

        playerStats.maxHealth.AddModifier(maxHealth);
        playerStats.armor.AddModifier(armor);
        playerStats.evasion.AddModifier(evasion);
        playerStats.magicResistance.AddModifier(magicResistance);

        playerStats.fireDamage.AddModifier(fireDamage);
        playerStats.iceDamage.AddModifier(iceDamage);
        playerStats.lightingDamage.AddModifier(lightingDamage);
    }

    public void RemoveModifiers()
    {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        playerStats.strength.RemoveModifier(strength);
        playerStats.agility.RemoveModifier(agility);
        playerStats.intelligence.RemoveModifier(intelligence);
        playerStats.vitality.RemoveModifier(vitality);

        playerStats.damage.RemoveModifier(damage);
        playerStats.critChance.RemoveModifier(critChance);
        playerStats.critPower.RemoveModifier(critPower);

        playerStats.maxHealth.RemoveModifier(maxHealth);
        playerStats.armor.RemoveModifier(armor);
        playerStats.evasion.RemoveModifier(evasion);
        playerStats.magicResistance.RemoveModifier(magicResistance);

        playerStats.fireDamage.RemoveModifier(fireDamage);
        playerStats.iceDamage.RemoveModifier(iceDamage);
        playerStats.lightingDamage.RemoveModifier(lightingDamage);
    }

    public override string GetDescription()
    {
        sb.Length = 0;

        AddItemDescription("Strength", strength);
        AddItemDescription("Agility", agility);
        AddItemDescription("Intelligence", intelligence);
        AddItemDescription("Vitality", vitality);

        AddItemDescription("Damage", damage);
        AddItemDescription("Crit Chance", critChance);
        AddItemDescription("Crit Power", critPower);

        AddItemDescription("Health", maxHealth);
        AddItemDescription("Armor", armor);
        AddItemDescription("Evasion", evasion);
        AddItemDescription("Magic Resist.", magicResistance);

        AddItemDescription("Fire DMG", fireDamage);
        AddItemDescription("Ice DMG", iceDamage);
        AddItemDescription("Lighting DMG", lightingDamage);

        if(itemEffects.Length > 0)
        {
            sb.AppendLine();
            sb.AppendLine();
            for (int i = 0; i < itemEffects.Length; i++)
            {
                if (itemEffects[i].effectDescription.Length > 0)
                {
                    sb.AppendLine("Passive: " + itemEffects[i].effectDescription);
                }
            }
        }

        return sb.ToString();
    }

    private void AddItemDescription(string _name, int _value)
    {
        if(_value != 0)
        {
            if(sb.Length > 0)
            {
                sb.AppendLine();
            }

            if(_value > 0)
            {
                sb.Append("+ " + _value + " " + _name);
            }
        }
    }
}
