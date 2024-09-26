using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class UI : MonoBehaviour, ISaveManager
{
    [Header("End Screen")]
    [SerializeField] private UI_FadeScreen fadeScreen;
    [SerializeField] private GameObject endText;
    [SerializeField] private GameObject restartButton;
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

    [SerializeField] private UI_VolumeSlider[] volumeSettings;

    private void Awake()
    {
        SwitchTo(skillTreeUI);
        fadeScreen.gameObject.SetActive(true);
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
        {
            _menu.SetActive(true);
        }

        if(GameManager.instance != null)
        {
            if (_menu == inGameUI)
                GameManager.instance.PauseGame(false);
            else
                GameManager.instance.PauseGame(true);
        }
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
        SFXWhenSwitchTab();
    }

    private void CheckForInGameUI()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf && transform.GetChild(i).GetComponent<UI_FadeScreen>() == null)
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

        //Volume Part

        foreach (KeyValuePair<string, float> pair in _data.volumeSettings)
        {
            foreach (UI_VolumeSlider item in volumeSettings)
            {
                if (item.parameter == pair.Key)
                    item.LoadSlider(pair.Value);
            }
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

        // Volume Part
        _data.volumeSettings.Clear();

        foreach (UI_VolumeSlider item in volumeSettings)
        {
            _data.volumeSettings.Add(item.parameter, item.slider.value);
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

        yield return new WaitForSeconds(1.5f);
        restartButton.SetActive(true);
    }

    public void RestartGameButton() => GameManager.instance.RestartScene();

    public void SFXWhenSwitchTab()
    {
        AudioManager.instance.PlaySFX(7, null);
    }
}
