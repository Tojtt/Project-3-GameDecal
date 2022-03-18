using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class MouseInput : MonoBehaviour
{
    public InventoryManager invM;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            wp.z = 0;

            Collider2D[] allOverlaps = Physics2D.OverlapCircleAll(wp, 1.1f);
            Debug.Log(string.Format("Hit {0} objects", allOverlaps.Length));
            for (int i = 0; i < allOverlaps.Length; i++)
            {
                GameObject target = allOverlaps[i].gameObject;
                if (target.CompareTag("Collectible"))
                {

                    //invM.AddItem(target.GetComponent<Item>().itemID, 1);
                    Destroy(target);
                    Debug.Log("Added");

                }
                else if (target.CompareTag("Trash"))
                {
                    Destroy(target);
                    Debug.Log("Trash has been disposed of");
                }

            }
        }
    }
}