using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemState : MonoBehaviour {

    private Transform trans;

    private float RotateSpeed = 120;

    private void Start()
    {
        
        trans = gameObject.GetComponent<Transform>();
    }

    private void Update()
    {
        trans.Rotate(Vector3.up, RotateSpeed * Time.deltaTime);
    }

}
