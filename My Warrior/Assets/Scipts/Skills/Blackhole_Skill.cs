using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackhole_Skill : Skill
{
    [SerializeField] private int amountOfAttacks;
    [SerializeField] private float cloneCooldown;
    [Space]
    [SerializeField] private GameObject blackholePrefab;
    [SerializeField] private float maxSize;
    [SerializeField] private float growSpeed;
    [SerializeField] private float shrinkSpeed;

    public override bool CanUseSkill()
    {
        return base.CanUseSkill();
    }

    public override void UseSkill()
    {
        base.UseSkill();

        GameObject newBlackhole = Instantiate(blackholePrefab);
        Blackhole_Skill_Controller newBlackholeScript = newBlackhole.GetComponent<Blackhole_Skill_Controller>();
        newBlackholeScript.SetupBlackhole(maxSize, growSpeed, shrinkSpeed, amountOfAttacks, cloneCooldown);
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }
}
