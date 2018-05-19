using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
    public GameObject[] ItemPoints;
    public GameObject[] newItems;

    private GameObject newItem;
    private GameObject ItemPoint;
    private Collider[] cols;
    public int ItemNum;
    private float CalmDown = 0;

    private void Update()
    {
        if (ItemNum == 0)
        {
            CalmDown++;
        }

        cols = Physics.OverlapSphere(gameObject.transform.position, 10000, 1 << LayerMask.NameToLayer("Item"));
        ItemNum = cols.Length;

        if (ItemNum < 1 && CalmDown >= 200)
        {
            int ItemPointNumber = Random.Range(0, ItemPoints.Length);
            int ItemNumber = Random.Range(0, newItems.Length);
            ItemPoint = ItemPoints[ItemPointNumber];
            newItem = newItems[ItemNumber];

            Instantiate(newItem, ItemPoint.transform.position, ItemPoint.transform.rotation);

            CalmDown = 0;
        }
    }
}
