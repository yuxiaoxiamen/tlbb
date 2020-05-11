using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonAnimationControl : MonoBehaviour
{
    private GameObject move;
    private GameObject action;
    private GameObject stand;
    private GameObject knife;
    private GameObject sword;
    private GameObject rod;
    private GameObject personObject;
    private Person person;
    // Start is called before the first frame update
    void Start()
    {
        person = FightMain.instance.persons[int.Parse(name)];
        int modelId = person.BaseData.ModelId;
        if(transform.Find("person") != null)
        {
            personObject = transform.Find("person").gameObject;
        }
        else
        {
            personObject = null;
        }
        if (personObject != null)
        {
            var animator = personObject.GetComponent<Animator>();
            AnimatorOverrideController overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
            overrideController["move"] = Resources.Load("animation/" + modelId + "move") as AnimationClip;
            overrideController["action"] = Resources.Load("animation/" + modelId + "action") as AnimationClip;
            overrideController["stand"] = Resources.Load("animation/" + modelId + "stand") as AnimationClip;
            animator.runtimeAnimatorController = overrideController;
        }
        else
        {
            move = transform.Find("move").gameObject;
            action = transform.Find("action").gameObject;
            stand = transform.Find("stand").gameObject;
            if (modelId == 0)
            {
                knife = transform.Find("knife").gameObject;
                sword = transform.Find("sword").gameObject;
                rod = transform.Find("rod").gameObject;
                knife.SetActive(false);
                sword.SetActive(false);
                rod.SetActive(false);
            }
            var animator = move.GetComponent<Animator>();
            AnimatorOverrideController overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
            overrideController["move"] = Resources.Load("animation/" + modelId + "move") as AnimationClip;
            animator.runtimeAnimatorController = overrideController;
            animator = action.GetComponent<Animator>();
            overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
            overrideController["action"] = Resources.Load("animation/" + modelId + "action") as AnimationClip;
            animator.runtimeAnimatorController = overrideController;
            action.SetActive(false);
            move.SetActive(false);
        }
    }

    public void Move()
    {
        if(personObject != null)
        {
            personObject.GetComponent<Animator>().SetBool("IsMove", true);
        }
        else
        {
            stand.SetActive(false);
            move.SetActive(true);
        }
    }

    public void Stand()
    {
        if(personObject != null)
        {
            var animator = personObject.GetComponent<Animator>();
            animator.SetBool("IsMove", false);
            animator.SetBool("IsAction", false);
        }
        else
        {
            if (move.activeSelf)
            {
                move.SetActive(false);
            }
            if (action.activeSelf)
            {
                action.SetActive(false);
            }
            if (person.BaseData.ModelId == 0)
            {
                if (knife.activeSelf)
                {
                    knife.SetActive(false);
                }
                if (sword.activeSelf)
                {
                    sword.SetActive(false);
                }
                if (rod.activeSelf)
                {
                    rod.SetActive(false);
                }
            }
            stand.SetActive(true);
        }
    }

    public IEnumerator Action()
    {
        if (personObject != null)
        {
            personObject.GetComponent<Animator>().SetBool("IsAction", true);
        }
        else
        {
            stand.SetActive(false);
            if (person.BaseData.ModelId == 0)
            {
                if (person.EquippedWeapon == null)
                {
                    action.SetActive(true);
                }
                else
                {
                    switch (person.EquippedWeapon.Type)
                    {
                        case ItemKind.Knife:
                            knife.SetActive(true);
                            break;
                        case ItemKind.Sword:
                            knife.SetActive(true);
                            break;
                        case ItemKind.Rod:
                            knife.SetActive(true);
                            break;
                    }
                }
            }
            else
            {
                action.SetActive(true);
            }
        }
        yield return new WaitForSeconds(0.6f);
        Stand();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
