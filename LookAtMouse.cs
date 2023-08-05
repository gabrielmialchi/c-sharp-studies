using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    /*public Transform myTransform;

    private void AimAtMouse()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - myTransform.position;

        //angle that the weapon must rotate around to face the cursor
        float angle = Mathf.Atan2 (direction.x, direction.y) * Mathf.Rad2Deg;

        //Vector3.forward = z axis
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        myTransform.rotation = rotation;
    }*/

    void Start()
    {
        
    }

    void Update()
    {
        AAMouse();
    }

    private void AAMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);

        transform.up = direction;
    }
}
