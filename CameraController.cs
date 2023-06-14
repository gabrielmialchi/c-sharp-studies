using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public class CameraController : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}*/

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float followSpeed = 10f; // Valor para controlar a velocidade de acompanhamento da câmera
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    // LateUpdate is called once per frame, after all Update functions
    void LateUpdate()
    {
        Vector3 desiredPosition = player.transform.position + offset;
        Vector3 newPosition = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
        transform.position = newPosition;
    }
}


