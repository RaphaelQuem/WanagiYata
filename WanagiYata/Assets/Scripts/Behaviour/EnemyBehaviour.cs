using Assets.Scripts.Extension;
using Assets.Scripts.StateMachine;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private Vector3 currentObjective;
    private float WaitTime;
    private EnemyStateMachine stateMch;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        stateMch = new EnemyStateMachine(anim);
        WaitTime = 0;
        currentObjective = transform.position;
    }

    void Update()
    {


        Vector3 movVector = CurrentObjective() - transform.position;
        Debug.DrawLine(transform.position, CurrentObjective(), Color.blue);
        movVector.Normalize();
        BoxCollider2D box = gameObject.GetComponent<BoxCollider2D>();
        box.enabled = false;
        //movVector = movVector.AvoidCollision(transform.position, 50, gameObject);
        stateMch.Directorvector = movVector;
        box.enabled = true;
        IsSeeingHero();
        gameObject.transform.position = gameObject.transform.position + movVector * Time.deltaTime;
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

        if (Vector3.Distance(currentObjective, transform.position) < 0.5f)
        {
            if (WaitTime.Equals(0f))
                WaitTime += Random.Range(0.5f, 4f);

            currentObjective = NextObjective();
        }

        return currentObjective;
    }
    private Vector3 NextObjective()
    {
        Vector3 nextobjective = new Vector3();
        nextobjective.x = Random.Range(transform.position.x - 5f, transform.position.x + 5f);
        nextobjective.y = Random.Range(transform.position.y - 5f, transform.position.y + 5f);
        nextobjective.z = 0;

        return nextobjective;
    }
    private bool IsSeeingHero()
    {
        for (int i = -30; i < 30; i++)
        {
            Vector3 vector = Quaternion.AngleAxis(i, new Vector3(0, 0, 1)) * stateMch.CurrentDirection.ToVector();
            RaycastHit2D hit = Physics2D.Raycast(transform.position, vector, 10);
            if (hit.collider != null)
            {
                if (hit.collider.tag.Equals("Player"))
                    Debug.DrawRay(transform.position, vector * 10f, Color.red);
                else
                    Debug.DrawRay(transform.position, vector * 10f, Color.blue);
            }
            else
                Debug.DrawRay(transform.position, vector * 10f, Color.blue);

        }
        return true;
    }
}
