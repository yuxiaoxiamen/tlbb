using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightStyleClick : MonoBehaviour
{
    private Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            var person = FightPersonClick.currentPerson;
            person.SelectedAttackStyle = person.BaseData.AttackStyles[int.Parse(name)];
            FightGUI.HideScrollPane();
            FightGUI.ShowBattlePane(FightPersonClick.currentPerson);
        });
    }
}
