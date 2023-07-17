using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour
{
    [System.Serializable]

    public struct ObjectID
    {
        public int idOb, amountOb;

        public ObjectID(int idOb, int amountOb)
        {
            this.idOb = idOb;
            this.amountOb = amountOb;
        }
    }

    // Modify ------------------------------------------------- \\
    [SerializeField] dataItems data;
    [SerializeField] GraphicRaycaster graphRay;
    PointerEventData pointerData;
    List<RaycastResult> rayResult;
    public static Transform canvas;
    public GameObject selecObject;
    public Transform firstParent;

    // Text ---------------------------------------------------- \\
    public static GameObject description;

    // Auto Fill ------------------------------------------------ \\
    public Transform content;
    public Item item;
    public List<ObjectID> inventory = new List<ObjectID>();
    List<Item> pool = new List<Item>();

    // Start is called before the first frame update
    void Start()
    {
        InventoryUpdate();

        // Modify ------------------------------------------------- \\
        pointerData = new PointerEventData(null);
        rayResult = new List<RaycastResult>();
        canvas = transform.parent.transform;

        // Text ---------------------------------------------------- \\
        description = GameObject.Find("Description");
    }

    // Update is called once per frame
    void Update()
    {
        Drag();
    }

    void Drag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            pointerData.position = Input.mousePosition;
            graphRay.Raycast(pointerData, rayResult);

            if (rayResult.Count > 0)
            {
                if (rayResult[0].gameObject.GetComponent<Item>())
                {
                    selecObject = rayResult[0].gameObject;
                    firstParent = selecObject.transform.parent.transform;
                    firstParent.GetComponent<Image>().fillCenter = false;
                    selecObject.transform.SetParent(canvas);
                }
            }
        } // End Input Down 0

        if (selecObject != null)
        {
            selecObject.GetComponent<RectTransform>().localPosition = canvasScreen(Input.mousePosition);

            if (Input.GetMouseButtonUp(0))
            {
                pointerData.position = Input.mousePosition;
                rayResult.Clear();
                graphRay.Raycast(pointerData, rayResult);
                selecObject.transform.SetParent(firstParent);

                if (rayResult.Count > 0)
                {
                    foreach (var result in rayResult)
                    {
                        if (result.gameObject == selecObject) continue;
                        {
                            if (result.gameObject.CompareTag("slot"))
                            {
                                if (result.gameObject.GetComponentInChildren<Item>() == null)
                                {
                                    selecObject.transform.SetParent(result.gameObject.transform);
                                }
                            }
                            else if (result.gameObject.CompareTag("item"))
                            {
                                if (result.gameObject.GetComponentInChildren<Item>().ID == selecObject.GetComponent<Item>().ID)
                                {
                                    Destroy(selecObject.gameObject);
                                }
                                else
                                {
                                    selecObject.transform.SetParent(result.gameObject.transform.parent);
                                    result.gameObject.transform.SetParent(firstParent);
                                    result.gameObject.transform.localPosition = Vector2.zero;
                                }
                            }

                        }
                    } // End foreach
                }
                selecObject.transform.localPosition = Vector2.zero;
                selecObject = null;

            } // End Input Up 0

        } // End != null

        rayResult.Clear();

    } // End Drag()

    public Vector2 canvasScreen(Vector2 screenPos)
    {
        Vector2 viewportPoint = Camera.main.ScreenToViewportPoint(screenPos);
        Vector2 canvasSize = canvas.GetComponent<RectTransform>().sizeDelta;

        return (new Vector2(viewportPoint.x * canvasSize.x, viewportPoint.y * canvasSize.y) - (canvasSize / 2));
    }

    public void InventoryUpdate()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (i < inventory.Count)
            {
                ObjectID au = inventory[i];
                pool[i].ID = au.idOb;
                pool[i].GetComponent<Image>().sprite = data.dataBase[au.idOb].icono;
                pool[i].GetComponent<RectTransform>().localPosition = Vector2.zero; // Recolocar
                pool[i].gameObject.SetActive(true);
            }
            else
            {
                pool[i].gameObject.SetActive(false);
                pool[i].gameObject.transform.parent.GetComponent<Image>().fillCenter = false;
            }

        } // End For

        if (inventory.Count > pool.Count)
        {
            for (int i = pool.Count; i < inventory.Count; i++)
            {
                Item it = Instantiate(item, content.GetChild(i));
                pool.Add(it);

                if (content.GetChild(0).childCount >= 2)
                {
                    for (int j = 0; j < content.childCount; j++)
                    {
                        if (content.GetChild(j).childCount == 0)
                        {
                            it.transform.SetParent(content.GetChild(j));
                            break;
                        }
                    }
                }
                it.transform.position = Vector2.zero;
                it.transform.localScale = Vector2.one;

                ObjectID au = inventory[i];
                pool[i].ID = au.idOb;
                pool[i].GetComponent<RectTransform>().localPosition = Vector2.zero;
                pool[i].GetComponent<Image>().sprite = data.dataBase[au.idOb].icono;
                pool[i].gameObject.SetActive(true);

            } // End For

        }

    } // End InventoryUpdate

    public void AddItem(int idAd, int amountAd)
    {
        inventory.Add(new ObjectID(idAd, amountAd));

        InventoryUpdate();

    } // End AddItem

    public void DeleteItem(int idDl, int amountDl)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].idOb == idDl)
            {
                inventory[i] = new ObjectID(inventory[i].idOb, inventory[i].amountOb - amountDl);
                if (inventory[i].amountOb <= 0)
                {
                    inventory.Remove(inventory[i]);
                    InventoryUpdate();
                    break;
                }
            }
            InventoryUpdate();

        } // End For

    } // End DeleteItem
}
