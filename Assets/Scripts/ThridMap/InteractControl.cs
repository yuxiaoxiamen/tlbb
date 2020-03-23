using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class InteractControl : MonoBehaviour
{
    public GameObject interButtonPrefab;
    public static InteractControl instance;
    public Vector3 defaultScale;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        defaultScale = transform.localScale;
        transform.localScale = new Vector3(defaultScale.x, 0, defaultScale.z);
    }

    public void ShowAndFillPanel(Person person)
    {
        ClearPane();
        ShowInterPanel();
        RectTransform rectTransform = GetComponent<RectTransform>();
        foreach (var interaction in person.BaseData.Interactions)
        {
            GameObject interButtonObject = Instantiate(interButtonPrefab);
            interButtonObject.GetComponent<RectTransform>().SetParent(rectTransform);
            interButtonObject.transform.localScale = Vector3.one;
            interButtonObject.transform.localRotation = Quaternion.identity;
            interButtonObject.transform.localPosition = Vector3.zero;
            interButtonObject.transform.Find("Text").GetComponent<Text>().text = interaction.Name;
            interButtonObject.GetComponent<Button>().onClick.AddListener(()=>
            {
                interaction.InteractionListener(person);
            });
        }
    }

    private void ClearPane()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    public void HideInterPanel()
    {
        transform.DOScale(new Vector3(defaultScale.x, 0, defaultScale.z), 0.2f);
    }

    void ShowInterPanel()
    {
        transform.DOScale(defaultScale, 0.2f);
    }
}
