using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public Stat damage;
    public Stat maxHealth;

    [SerializeField] private int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth.GetValue();

        damage.AddModifier(4);
    }

    public void TakeDamage(int _damage)
    {
        currentHealth -= _damage;
    }
}
