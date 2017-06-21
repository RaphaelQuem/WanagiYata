using Assets.Scripts.Extension;
using UnityEngine;


public class ArrowBehaviour : MonoBehaviour
{
    public Vector3 vector;
    public GameObject player;
    void Start()
    {

    }
    void Update()
    {
        Vector3 pos = transform.position;
        
        if (Vector3.Distance(pos,player.transform.position) > 10f)
            Destroy(gameObject);

        transform.position = pos + vector * Time.deltaTime * 10;
        transform.rotation = Quaternion.AngleAxis(vector.ToAngleAxis(),new Vector3(0, 0, 1));
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Animal"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
