using UnityEngine;
using UnityEngine.UI;

public class SelectAccIcon : MonoBehaviour
{
    [SerializeField] private GameObject blockedPanel;
    public string iconName;
    public Image button;

    void Start()
    {
        iconName = GetComponent<Image>().sprite.name;

        CheckLock();

        if (iconName == JsonStorage.instance.data.userData.userIcon)
        {
            Change();
        }
    }

    public void CheckLock()
    {
        bool isBought = JsonStorage.instance.data.icons.icons.Contains(iconName);

        if (isBought == true)
        {
            blockedPanel.SetActive(false);
        }
    }

    public void Change()
    {
        EquipAccIcon.instance.Change(iconName, button);
    }
}
