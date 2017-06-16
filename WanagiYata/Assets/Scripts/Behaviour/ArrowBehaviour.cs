using UnityEngine;


public class ArrowBehaviour : MonoBehaviour
{
    public Vector3 vector;
    void Start()
    {

    }
    void Update()
    {
        Vector3 pos = transform.position;
        if (pos.x > StaticResources.RightCameraLimit || pos.x < StaticResources.LeftCameraLimit || pos.y > StaticResources.TopCameraLimit || pos.y < StaticResources.BotCameraLimit)
            Destroy(gameObject);

        transform.position = pos + vector * Time.deltaTime * 3000;
    }
}
