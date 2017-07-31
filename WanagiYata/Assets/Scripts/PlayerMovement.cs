using Assets.Scripts.Extension;
using Assets.Scripts.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rbody;
    private Animator anim;
    private PlayerStateMachine stateMch;
    public GameObject trap;
    public float speed;
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
        anim.SetBool("isSettingTrap", false);
        if (InputManager.BPressed())
            speed = 2.5f;
        else
            speed = 1f;

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
            SetTrap();
        }

        if (InputManager.YButton())
            Roll();

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
    private void SetTrap()
    {
        anim.SetBool("isSettingTrap", true);
        Instantiate(trap, new Vector3(rbody.position.x, rbody.position.y, 0), Quaternion.identity);
    }
    private void Roll()
    {
        anim.SetBool("isRolling", true);
        Vector2 movVector = InputManager.ControllerVector();
        stateMch.Directorvector = movVector;
        gameObject.transform.position = gameObject.transform.position + (Vector3)movVector * Time.deltaTime * speed * 10;
        anim.SetBool("isRolling", false);
    }

}

