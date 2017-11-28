
using UnityEngine;

public class DecorationGroupCleaner : MonoBehaviour
{
    void Update()
    {
        if(this.transform.childCount == 0)
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
