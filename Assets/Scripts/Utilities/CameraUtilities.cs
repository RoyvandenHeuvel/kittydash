using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CameraUtilities
{
    public static bool IsVisible(GameObject @object)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        return GeometryUtility.TestPlanesAABB(planes, @object.GetComponent<Collider2D>().bounds);
    }

    public static bool IsHigherThanCamera(this GameObject @object)
    {
        float cameraMaxVisibleY = Camera.main.transform.position.y + Camera.main.orthographicSize;
        var spriteRenderer = @object.GetComponent<SpriteRenderer>();
        float objectY;
        if (spriteRenderer != null)
        {

            objectY = @object.transform.position.y - spriteRenderer.size.y / 2;
        }
        else
        {
            objectY = @object.transform.position.y;
        }
        return cameraMaxVisibleY < objectY;
    }

    public static bool IsLowerThanCamera(this GameObject @object)
    {
        float cameraMinVisibleY = Camera.main.transform.position.y - Camera.main.orthographicSize;
        var spriteRenderer = @object.GetComponent<SpriteRenderer>();
        float objectY;
        if (spriteRenderer != null)
        {

            objectY = @object.transform.position.y + spriteRenderer.size.y / 2;
        }
        else
        {
            objectY = @object.transform.position.y;
        }

        return cameraMinVisibleY > objectY;
    }
}
