﻿using System.Collections;
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
            person.SelectedInnerGong = person.BaseData.InnerGongs[int.Parse(name)];
            FightGUI.HideScrollPane();
            FightGUI.ShowBattlePane(FightPersonClick.currentPerson);
        });
    }
}
