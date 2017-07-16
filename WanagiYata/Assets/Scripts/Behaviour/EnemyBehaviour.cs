using Assets.Scripts.Extension;
using Assets.Scripts.StateMachine;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private Vector3 currentObjective;
    private float waitTime;
    private float reloadTime;
    private EnemyStateMachine stateMch;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        stateMch = new EnemyStateMachine(anim);
        waitTime = 0;
        currentObjective = transform.position;
    }

    void Update()
    {

        Vector3 movVector = CurrentObjective() - transform.position;
        Debug.DrawLine(transform.position, CurrentObjective(), Color.blue);
        movVector.Normalize();      
        stateMch.Directorvector = movVector;

        if(IsSeeingHero())
        {
            ShootTarget(GameObject.FindGameObjectsWithTag("Player")[0]);
        }
        gameObject.transform.position = gameObject.transform.position + movVector * Time.deltaTime;
    }
    private Vector3 CurrentObjective()
    {
        if (waitTime > 0f)
        {
            waitTime -= Mathf.Min(waitTime, Time.deltaTime);
            if (waitTime.Equals(0f))
                currentObjective = NextObjective();

            return transform.position;
        }

        if (Vector3.Distance(currentObjective, transform.position) < 0.5f)
        {
            if (waitTime.Equals(0f))
                waitTime += Random.Range(0.5f, 4f);

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
                {
                    Debug.DrawRay(transform.position, vector * 10f, Color.red);
                    return true;
                }
                else
                    Debug.DrawRay(transform.position, vector * 10f, Color.blue);
                    
            }
            else
                Debug.DrawRay(transform.position, vector * 10f, Color.blue);

        }
        return false;
    }
    private void ShootTarget(GameObject target)
    {
        reloadTime -= Mathf.Min(reloadTime, Time.deltaTime);
        if (reloadTime.Equals(0))
        {
            GameObject spawnedobj = (GameObject)Resources.Load("Bullet");
            spawnedobj.transform.position = transform.position + stateMch.CurrentDirection.ToVector();
            ProjectileBehaviour behaviour = spawnedobj.GetComponent<ProjectileBehaviour>();
            behaviour.vector = (target.transform.position - transform.position);
            behaviour.shooter = gameObject;
            GameObject.Instantiate(spawnedobj);
            reloadTime += 1.5f;
        }
    }
}
