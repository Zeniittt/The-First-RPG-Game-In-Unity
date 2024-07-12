using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class UI : MonoBehaviour, ISaveManager
{
    [Header("End Screen")]
    [SerializeField] private UI_FadeScreen fadeScreen;
    [SerializeField] private GameObject endText;
    [Space]

    [SerializeField] private GameObject characterUI;
    [SerializeField] private GameObject skillTreeUI;
    [SerializeField] private GameObject craftUI;
    [SerializeField] private GameObject optionsUI;
    [SerializeField] private GameObject inGameUI;

    public UI_ItemToolTip itemToolTip;
    public UI_StatToolTip statToolTip;
    public UI_CraftWindow craftWindow;
    public UI_SkillToolTip skillToolTip;

    private Dictionary<string, bool> skillTreeDictionary;

    private void Awake()
    {
        SwitchTo(skillTreeUI);
    }

    void Start()
    {
        SwitchTo(inGameUI);

        itemToolTip.gameObject.SetActive(false);
        statToolTip.gameObject.SetActive(false);

        skillTreeDictionary = SaveSkillManager.instance.skillTree;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Tab))
            SwitchWithKeyTo(characterUI);

        if (Input.GetKeyUp(KeyCode.K))
            SwitchWithKeyTo(skillTreeUI);

        if (Input.GetKeyUp(KeyCode.P))
            SwitchWithKeyTo(craftUI);

        if (Input.GetKeyUp(KeyCode.O))
            SwitchWithKeyTo(optionsUI);

        skillTreeDictionary = SaveSkillManager.instance.skillTree;
    }

    public void SwitchTo(GameObject _menu)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            bool fadeScreen = transform.GetChild(i).GetComponent<UI_FadeScreen>() != null;  //we need this to keep fade screen gameObejct active
            if(fadeScreen == false)
                transform.GetChild(i).gameObject.SetActive(false);
        }

        if (_menu != null)
            _menu.SetActive(true);
    }

    public void SwitchWithKeyTo(GameObject _menu)
    {
        if (_menu != null && _menu.activeSelf)
        {
            _menu.SetActive(false);
            CheckForInGameUI();
            return;
        }

        SwitchTo(_menu);
    }

    private void CheckForInGameUI()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf)
                return;
        }

        SwitchTo(inGameUI);
    }

    public void LoadData(GameData _data)
    {
        foreach (KeyValuePair<string, bool> item in _data.skillTree)
        {
            SaveSkillManager.instance.SetSkillFromDataBase(item.Key, item.Value);
        }
    }

    public void SaveData(ref GameData _data)
    {

        Dictionary<string, bool> newSkillTreeDictionary = SaveSkillManager.instance.skillTree;

        foreach (KeyValuePair<string, bool> skill in skillTreeDictionary)
        {
            if (_data.skillTree.TryGetValue(skill.Key, out bool value))
            {
                _data.skillTree.Remove(skill.Key);
                _data.skillTree.Add(skill.Key, skill.Value);
            }
            else
            {
                _data.skillTree.Add(skill.Key, skill.Value);
            }
        }

    }

    public void SwitchOnEndScreen()
    {
        fadeScreen.FadeOut();
        StartCoroutine(EndScreenCoroutine());
    }

    IEnumerator EndScreenCoroutine()
    {
        yield return new WaitForSeconds(1);

        endText.SetActive(true);
    }
}
