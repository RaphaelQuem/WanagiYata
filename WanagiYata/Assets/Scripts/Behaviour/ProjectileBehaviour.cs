using Assets.Scripts.Extension;
using UnityEngine;


public class ProjectileBehaviour : MonoBehaviour
{
    public Vector3 vector;
    public GameObject shooter;
    public GameObject bloodParticle;
    private float? particleTimer = null;
    void Start()
    {

    }
    void Update()
    {
        Vector3 pos = transform.position;

        if (Vector3.Distance(pos, shooter.transform.position) > 30f)
            Destroy(gameObject);

        transform.position = pos + vector * Time.deltaTime * 5;
        transform.rotation = Quaternion.AngleAxis(vector.ToAngleAxis(), new Vector3(0, 0, 1));
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.tag.Equals(shooter.tag))
        {
            if (other.tag.Equals("Enemy") || other.tag.Equals("Animal") || other.tag.Equals("Player"))
            {
                other.gameObject.GetComponent<Animator>().SetTrigger("isDying");
                Instantiate(bloodParticle,transform);
                if (particleTimer == null)
                    particleTimer = 1f;
                else
                    particleTimer -= Time.deltaTime;

                if(particleTimer <= 0f)
                    Destroy(gameObject);
            }
        }
    }
}
