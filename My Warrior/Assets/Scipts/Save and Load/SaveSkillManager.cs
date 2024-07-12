using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveSkillManager : MonoBehaviour
{
    public static SaveSkillManager instance;

    public Dictionary<string, bool> skillTree;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;

        skillTree = GetAllSkillInSkillTree();
    }

    private void Update()
    {
        skillTree = GetAllSkillInSkillTree();
    }

    private Dictionary<string, bool> GetAllSkillInSkillTree()
    {
        Dictionary<string, bool> dictionary = new Dictionary<string, bool>();

        for (int i = 0; i < this.transform.childCount; i++)
        {
            UI_SkillTreeSlot skill = this.transform.GetChild(i).gameObject.GetComponent<UI_SkillTreeSlot>();
            dictionary.Add(skill.skillName, skill.unlocked);
        }

        return dictionary;
    }

    public void SetSkillFromDataBase(string nameSkill, bool unlock)
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            UI_SkillTreeSlot skill = this.transform.GetChild(i).gameObject.GetComponent<UI_SkillTreeSlot>();
            if(nameSkill == skill.skillName && unlock == true)
            {
                skill.unlocked = true;
            }
        }
    } 
}
