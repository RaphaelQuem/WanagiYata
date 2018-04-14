using Assets.Scripts.Extension;
using Assets.Scripts.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using Assets.Scripts.Resource;
using Assets.Scripts.StateMachine.Player;
using Assets.Scripts.Model;
using Assets.Scripts.Managers.Interactions;

public class PlayerBehaviour : MonoBehaviour
{

    private Rigidbody2D rbody;
    private Animator anim;
    private Animator bowanim;
    public PlayerStateMachine stateMch;
    public GameObject trap;
    public GameObject ActionTarget { get; set; }
    public float speed;
    public bool IsHidden { get; set; }
    public bool CanHide { get; set; }
    public int Scalps { get; set; }
    public int Skins { get; set; }
    public List<string> Dialogues { get; set; }
    public Direction Colliding { get; set; }
    public PlayerAction CurrentAction { get; set; }

    public IState CurrentState;
    

    void Start()
    {
        StaticResources.MapColumn = 3;
        StaticResources.MapRow = 3;
        StaticResources.CurrentDay = 1;
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Colliding = Direction.None;
        stateMch = new PlayerStateMachine(anim);
        CurrentState = new PlayerWalkingState(this);
        Dialogues = new List<string>();
        EventManager.StartListening("teste", EventResponse);
    }

    void Update()
    {
        CurrentState.Update();
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Dead") || anim.GetCurrentAnimatorStateInfo(0).IsName("Dying"))
            return;

        if (InputManager.StealthButtonPressed())
        {
            IsHidden = true;
            EventModel model = new EventModel();
            model.DestinationX = 500;
            model.DestinationY = 500;
            List<EventModel> list = new List<EventModel>();
            list.Add(model);
            EventManager.TriggerEvent("teste",list);








            EventManager.GetText();
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
                //Move();

            }
            else
            {


                List<GameObject> withinRange = WithinRange();
                if (withinRange.Count.Equals(0))
                    SetTrap();
                else
                {
                    switch (CurrentAction)
                    {
                        case PlayerAction.Scalp:
                            ActionTarget.GetComponent<EnemyBehaviour>().Scalp();
                            CurrentAction = PlayerAction.None;
                            Scalps++;
                            break;
                        case PlayerAction.Skin:
                            ActionTarget.GetComponent<AnimalBehaviour>().Skin();
                            CurrentAction = PlayerAction.None;
                            Skins++;
                            break;
                        default:
                            Stealthkill(withinRange);
                            break;
                    }



                }
            }
        }

        if (InputManager.YButton())
        {

            Roll();
        }
    }
    

    private void Stealthkill(List<GameObject> withinRange)
    {
        withinRange = withinRange.Where(x => x.tag.Equals("Enemy") && !x.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Dead") && !x.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Scalped")).ToList();
        if (withinRange.Count.Equals(0))
            return;
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
        stateMch.CurrentState = ObjectState.Shooting;
        anim.SetTrigger("Shooting");
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

        // Dont allow the play to move towards in others objects direction when colliding
        if (movVector.ToDirection().Equals(Colliding))
        {
            movVector = Vector2.zero;
            stateMch.Directorvector = movVector;
            return;
        }

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
                {
                    if (hit.collider.tag.Equals("Enemy") || hit.collider.tag.Equals("Animal"))
                    {
                        objects.Add(hit.collider.gameObject);
                    }
                }

            }
            else
                Debug.DrawRay(transform.position, vector * 0.75f, Color.blue);

        }
        return objects;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("StealthKill"))
        {
            // mudar pra anim de one hit ko
            collision.gameObject.GetComponent<Animator>().SetTrigger("isDying");
            anim.SetBool("isKilling", false);
        }

        Vector2 collisionVector = collision.gameObject.transform.position - gameObject.transform.position;

        Colliding = collisionVector.ToDirection();
        Debug.Log(Colliding.ToString());
    }
    public void OnCollisionExit2D(Collision2D collision)
    {
        Colliding = Direction.None;
    }
    public void EventResponse(List<EventModel> list)
    {
        this.CurrentState = new PlayerEventState(this, list);
    }
}

