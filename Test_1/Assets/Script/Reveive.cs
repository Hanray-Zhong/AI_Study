using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reveive : MonoBehaviour {

    public GameObject[] ReveivePoints;
    public GameObject newRole;

    private GameObject reveivePoint;
    private Collider[] cols;
    public int AINum;
    private float CalmDown = 0;

    private void Start()
    {
        
    }

    private void Update()
    {
        CalmDown++;

        cols = Physics.OverlapSphere(gameObject.transform.position, 10000, 1 << LayerMask.NameToLayer("AI"));
        AINum = cols.Length;

        if (AINum < 4 && CalmDown >= 150)
        {
            int ReveivePointNumber = Random.Range(0, ReveivePoints.Length);
            reveivePoint = ReveivePoints[ReveivePointNumber];

            Instantiate(newRole, reveivePoint.transform.position, reveivePoint.transform.rotation);

            CalmDown = 0;
        }
    }
}
