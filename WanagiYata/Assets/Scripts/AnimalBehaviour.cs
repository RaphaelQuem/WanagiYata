using UnityEngine;

public class AnimalBehaviour : MonoBehaviour
{
    private Vector3 currentObjective;
    private float WaitTime;

    void Start()
    {
        WaitTime = 0;
    }

    void Update()
    {

        Vector3 movVector = CurrentObjective().position - transform.position;
        movVector.Normalize();


        gameObject.transform.position = gameObject.transform.position + movVector * Time.deltaTime;
    }
    private Transform GetClosestTrap(GameObject[] traps)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (GameObject t in traps)
        {
            float dist = Vector3.Distance(t.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = t.transform;
                minDist = dist;
            }
        }
        return tMin;
    }
    private Transform CurrentObjective()
    {
        Transform objective = transform;
        Vector3 runvector = RunFromPlayer();
        if (runvector.Equals(Vector3.zero))
        {
            if (WaitTime > 0f)
            {
                WaitTime -= Mathf.Min(WaitTime, Time.deltaTime);
                if (WaitTime.Equals(0f))
                    currentObjective = NextObjective();

                return objective;
            }
            objective = GetClosestTrap(GameObject.FindGameObjectsWithTag("Trap"));
            if (objective == null)
            {
                if (currentObjective == null || currentObjective.Equals(transform.position))
                {
                    if (currentObjective.Equals(transform.position) && WaitTime.Equals(0f))
                        WaitTime += Random.Range(0.5f, 4f);

                    currentObjective = NextObjective();
                }
                objective.position = currentObjective;
            }
        }
        else
            objective.position = runvector;

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
        if (Vector3.Distance(transform.position, player.transform.position) <= 5)
            v = transform.position - player.transform.position;

        return v;

    }

}
