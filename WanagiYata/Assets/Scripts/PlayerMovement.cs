using Assets.Scripts.Extension;
using Assets.Scripts.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rbody;
    private Animator anim;
    private PlayerStateMachine stateMch;
    public GameObject trap;
    public float speed;
    public bool IsHidden { get; set; }
    public bool CanHide { get; set; }
    void Start()
    {
        StaticResources.MapColumn = 3;
        StaticResources.MapRow = 3;
        StaticResources.CurrentDay = 1;
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        stateMch = new PlayerStateMachine(anim);

    }

    void Update()
    {
        if (InputManager.BPressed())
        {
            speed = 2.5f;
        }
        else
        {
            speed = 1f;
        }

        if (InputManager.StealthButtonPressed())
        {
            if (CanHide)
                IsHidden = true;
        }
        else
        {
            IsHidden = false;
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Rolling"))
        {
            speed = 3.5f;
            if (InputManager.ControllerVector().Equals(Vector2.zero))
                Move(stateMch.CurrentDirection.ToVector());
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("StealthKill"))
        {
            speed = 3.5f;
            if (InputManager.ControllerVector().Equals(Vector2.zero))
                KillSingle();
        }
        else
        {



            if (InputManager.XButton())
            {
                ShootArrow();
            }

            if (!InputManager.AButton())
            {
                Move();
            }
            else
            {
                List<GameObject> withinRange = WithinRange();
                if (withinRange.Count.Equals(0))
                    SetTrap();
                else
                    Stealthkill(withinRange);
            }

            if (InputManager.YButton())
            {
                Roll();
            }
        }
    }

    private void Stealthkill(List<GameObject> withinRange)
    {
        if (withinRange.Count.Equals(1))
        {
            stateMch.Kill();
            KillSingle();
        }
        else
            KillMultiple(withinRange);
    }

    private void KillMultiple(List<GameObject> withinRange)
    {
        throw new NotImplementedException();
    }

    private void KillSingle()
    {

        GameObject enemy = gameObject.GetClosestObject(GameObject.FindGameObjectsWithTag("Enemy"));
        Vector2 direction = enemy.transform.position - transform.position;
        if (direction.magnitude > 0.5)
        {

            stateMch.Directorvector = direction.normalized;
            Move(stateMch.Directorvector);
        }
        else
        {
            stateMch.Directorvector = Vector2.zero;
            stateMch.CurrentState = ObjectState.Idle;
            Destroy(enemy);
            anim.SetBool("isKilling", false);
        }
        Debug.Log(anim.GetCurrentAnimatorStateInfo(0).fullPathHash.ToString());
    }
    private Vector3 GetClosestEnemy(GameObject[] enemies)
    {
        Vector3 tPos = transform.position;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (GameObject e in enemies)
        {
            float dist = Vector3.Distance(e.transform.position, currentPos);

            if (dist < minDist)
            {
                tPos = e.transform.position;
                minDist = dist;
            }

        }
        return tPos;
    }

    private void ShootArrow()
    {
        GameObject spawnedobj = (GameObject)Resources.Load("Arrow");
        spawnedobj.transform.position = transform.position;
        ProjectileBehaviour behaviour = spawnedobj.GetComponent<ProjectileBehaviour>();
        behaviour.vector = stateMch.CurrentDirection.ToVector();
        behaviour.shooter = gameObject;
        GameObject.Instantiate(spawnedobj);
    }
    private void Move()
    {
        Vector2 movVector = InputManager.ControllerVector();
        stateMch.Directorvector = movVector;
        gameObject.transform.position = gameObject.transform.position + (Vector3)movVector * Time.deltaTime * speed;
    }
    private void Move(Vector2 vec)
    {

        stateMch.Directorvector = vec;
        gameObject.transform.position = gameObject.transform.position + (Vector3)vec * Time.deltaTime * speed;

    }
    private void SetTrap()
    {
        if (stateMch.CanSetTrap)
        {
            stateMch.IsSettingTrap = true;
            stateMch.Directorvector = InputManager.ControllerVector(); ;
            Instantiate(trap, new Vector3(rbody.position.x, rbody.position.y, 0), Quaternion.identity);
        }
    }
    private void Roll()
    {
        if (stateMch.CanRoll)
        {

            stateMch.IsRolling = true;

        }
    }
    public List<GameObject> WithinRange()
    {
        List<GameObject> objects = new List<GameObject>();
        for (int i = 0; i < 360; i += 5)
        {
            Vector3 vector = Quaternion.AngleAxis(i, new Vector3(0, 0, 1)) * stateMch.CurrentDirection.ToVector();
            RaycastHit2D hit = Physics2D.Raycast(transform.position, vector, 0.75f);
            if (hit.collider != null)
            {
                Debug.DrawRay(transform.position, vector * 0.75f, Color.red);
                if (!objects.Contains(hit.collider.gameObject))
                    objects.Add(hit.collider.gameObject);

            }
            else
                Debug.DrawRay(transform.position, vector * 0.75f, Color.blue);

        }
        return objects;
    }

}

