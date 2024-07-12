using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Parry_Skill : Skill
{
    [Header("Parry")]
    [SerializeField] private UI_SkillTreeSlot parryUnlockButton;
    public bool parryUnlocked { get; private set; }

    [Header("Parry Restore Health")]
    [SerializeField] private UI_SkillTreeSlot restoreHealthUnlockButton;
    public bool restoreHealthUnlocked { get; private set; }
    [Range(0f, 1f)]
    [SerializeField] private float restoreHealthPercentage;

    [Header("Parry with Mirage")]
    [SerializeField] private UI_SkillTreeSlot parryWithMirageUnlockButton;
    public bool parryWithMirageUnlocked { get; private set; }

    public override void UseSkill()
    {
        base.UseSkill();

        if (restoreHealthUnlocked)
        {
            int restoreAmount = Mathf.RoundToInt(player.stats.GetMaxHealthValue() * restoreHealthPercentage);
            player.stats.IncreaseHealthBy(restoreAmount);
        }
    }

    protected override void Start()
    {
        base.Start();

        parryUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockParry);
        restoreHealthUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockParryRestoreHealth);
        parryWithMirageUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockParryWithMirage);
    }

    protected override void CheckUnlock()
    {
        UnlockParry();
        UnlockParryRestoreHealth();
        UnlockParryWithMirage();
    }

    #region Unlock

    private void UnlockParry()
    {
        if (parryUnlockButton.unlocked)
            parryUnlocked = true;
    }

    private void UnlockParryRestoreHealth()
    {
        if(restoreHealthUnlockButton.unlocked)
            restoreHealthUnlocked = true;
    }

    private void UnlockParryWithMirage()
    {
        if (parryWithMirageUnlockButton.unlocked)
            parryWithMirageUnlocked = true;
    }

    #endregion

    public void MakeMirageOnParry(Transform _respawnTransform)
    {
        if (parryWithMirageUnlocked)
            SkillManager.instance.clone.CreateCloneWithDelay(_respawnTransform);
    }
}
