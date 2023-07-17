using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class Item : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler 
{
    public int ID;
    public dataItems dataItems;
    public Vector3 offset;

    // Information box ------------------------------- \\
    [HideInInspector] public GameObject descripItem;
    [HideInInspector] public Text infoTxt;

    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector3(-100, 0, 0);

        // Information box ------------------------------- \\
        descripItem = Inventory.description;
        //infoTxt = descripItem.transform.GetChild(1).GetComponent<Text>();
        descripItem.SetActive(false);
        //if (!descripItem.GetComponent<Image>().enabled)
        //{
        //    descripItem.GetComponent<Image>().enabled = true;
        //    infoTxt.enabled = true;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent.GetComponent<Image>() != null)
        {
            transform.parent.GetComponent<Image>().fillCenter = true;
        }

        // Information box ------------------------------- \\
        if (transform.parent == Inventory.canvas)
        {
            descripItem.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        descripItem.SetActive(true);
        //infoTxt.text = dataItems.dataBase[ID].description;
        descripItem.transform.position = transform.position + offset;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        descripItem.SetActive(false);
    }
}
