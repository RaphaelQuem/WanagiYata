using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rbody;
    private Animator anim;
    public GameObject trap;
    public float speed;
    void Start()
    {
        StaticResources.MapColumn = 3;
        StaticResources.MapRow = 3;
        StaticResources.CurrentDay = 1;
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        anim.SetBool("isSettingTrap", false);
        if (InputManager.BButton())
            speed = 5;

        if(InputManager.XButton())
        {
            GameObject spawnedobj = (GameObject)Resources.Load("Arrow");
            spawnedobj.transform.position = transform.position;
            ArrowBehaviour behaviour = spawnedobj.GetComponent<ArrowBehaviour>();
            behaviour.vector = InputManager.ControllerVector();
            GameObject.Instantiate(spawnedobj);
        }

        if (!InputManager.AButton())
        {
            Vector2 movVector = InputManager.ControllerVector();
            if (!movVector.Equals(Vector2.zero))
            {
                anim.SetBool("isWalking", true);
                anim.SetFloat("InputX", movVector.x);
                anim.SetFloat("InputY", movVector.y);
            }
            else
                anim.SetBool("isWalking", false);

            rbody.MovePosition(rbody.position + movVector * Time.deltaTime * speed);
            
        }
        else
        {
            anim.SetBool("isSettingTrap", true);
            Instantiate(trap, new Vector3(rbody.position.x, rbody.position.y,0),Quaternion.identity);
        }

        
    }
}
