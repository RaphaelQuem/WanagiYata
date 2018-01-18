using Assets.Scripts.Extension;
using UnityEngine;

public class AnimalBehaviour : MonoBehaviour
{
    private Vector3 currentObjective;
    private float WaitTime;
    private Animator anim;
    public bool IsColliding = false;
    void Start()
    {
        anim = GetComponent<Animator>();
        WaitTime = 0;
        currentObjective = transform.position;
    }

    void Update()
    {

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Dead") || anim.GetCurrentAnimatorStateInfo(0).IsName("Skinned"))
            return;


        //Vector3 movVector = RunFromPlayer();
        //if (movVector.Equals(Vector3.zero))
        Vector3 movVector = CurrentObjective() - transform.position;
        Debug.DrawLine(transform.position, CurrentObjective(), Color.blue);
        movVector.Normalize();
        gameObject.transform.position = gameObject.transform.position + movVector * Time.deltaTime;
    }
    private Vector3 GetClosestTrap(GameObject[] traps)
    {
        Vector3 tPos = transform.position;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        GameObject player = GameObject.FindGameObjectsWithTag("Player")[0];
        foreach (GameObject t in traps)
        {
            float dist = Vector3.Distance(t.transform.position, currentPos);
            float playdist = Vector3.Distance(t.transform.position, player.transform.position);
            if (playdist >= 3f)
            {
                if (dist < minDist)
                {
                    tPos = t.transform.position;
                    minDist = dist;
                }
            }
        }
        return tPos;
    }
    private Vector3 CurrentObjective()
    {
        if (WaitTime > 0f)
        {
            WaitTime -= Mathf.Min(WaitTime, Time.deltaTime);
            if (WaitTime.Equals(0f))
                currentObjective = NextObjective();

            return transform.position;
        }
        Vector3 objective = GetClosestTrap(GameObject.FindGameObjectsWithTag("Trap"));
        if (Vector3.Distance(objective, transform.position) <0.5f)
        {
            if (Vector3.Distance(currentObjective,transform.position) < 0.5f )
            {
                if (WaitTime.Equals(0f))
                    WaitTime += Random.Range(0.5f, 4f);

                currentObjective = NextObjective();
            }
            objective = currentObjective;
        }

        return objective;
    }
    private Vector3 NextObjective()
    {
        Vector3 nextobjective = new Vector3();
        nextobjective.x = Random.Range(transform.position.x - 5f, transform.position.x + 5f);
        nextobjective.y = Random.Range(transform.position.y - 5f, transform.position.y + 5f);
        nextobjective.z = 0;

        return nextobjective;
    }
    private Vector3 RunFromPlayer()
    {
        GameObject player = GameObject.FindGameObjectsWithTag("Player")[0];
        Vector3 v = Vector3.zero;
        if (Vector3.Distance(transform.position, player.transform.position) <= 2)
        {
            v = transform.position - player.transform.position;
            currentObjective = transform.position;
        }

        return v;

    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Dead"))
            {
                Debug.Log("Skineia garoto");
                other.GetComponent<PlayerBehaviour>().CurrentAction = Assets.Scripts.Resource.PlayerAction.Skin;
                other.GetComponent<PlayerBehaviour>().ActionTarget = gameObject;
            }
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            other.GetComponent<PlayerBehaviour>().CurrentAction = Assets.Scripts.Resource.PlayerAction.None;
        }
    }
    public void Skin()
    {
        anim.SetBool("isSkinned", true);
    }
}
