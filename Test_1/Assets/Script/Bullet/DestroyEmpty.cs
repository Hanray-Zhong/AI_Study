using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEmpty : MonoBehaviour {
    private void Update()
    {
        Destroy(gameObject, 2);
    }
}
