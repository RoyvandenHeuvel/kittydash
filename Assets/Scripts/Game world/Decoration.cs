using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decoration : MonoBehaviour
{
    void Update()
    {
        if (gameObject.IsLowerThanCamera())
        {
            DestroyObject(this.gameObject);
        }
    }
}
