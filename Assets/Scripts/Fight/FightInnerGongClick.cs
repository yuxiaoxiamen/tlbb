using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightInnerGongClick : MonoBehaviour
{
    private Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            var person = FightPersonClick.currentPerson;
            GongBuffTool.instance.ResumeGongBuff(person);
            person.SelectedInnerGong = person.BaseData.InnerGongs[int.Parse(name)];
            GongBuffTool.instance.EffectValueBuff(person);
            GongBuffTool.instance.CreateHalo(person, FightMain.instance.friendQueue, FightMain.instance.enemyQueue);
            FightGUI.HideScrollPane();
            FightGUI.ShowBattlePane(FightPersonClick.currentPerson);
        });
    }
}
