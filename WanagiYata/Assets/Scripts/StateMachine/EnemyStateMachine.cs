using UnityEngine;
using Assets.Scripts.Resource;

namespace Assets.Scripts.StateMachine
{

    public class EnemyStateMachine
    {
        private float currentStateDuration;
        private ObjectState currentState;
        private Direction currentDirection;
        private Vector2 directorVector;
        
        private Animator animator;
        public ObjectState CurrentState { get { return currentState; } set{ currentState = value; } }
        public Direction CurrentDirection { get { return currentDirection; } set { currentDirection = value; } }
        public bool IsSettingTrap { get; set; }

        public EnemyStateMachine(Animator anim)
        {
            animator = anim;
        }

        public Vector2 Directorvector
        {
            get
            {
                return directorVector;
            }
            set
            {
                directorVector = value;
                currentStateDuration += Time.deltaTime;
                if (directorVector != Vector2.zero)
                {
                    animator.SetFloat("InputX", directorVector.x);
                    animator.SetFloat("InputY", directorVector.y);
                    animator.SetBool("isWalking", true);
                    ChangeCurrentState(ObjectState.Walking);
                    CurrentDirection = GetCurrentDirection();
                }
                else
                {
                    ChangeCurrentState(ObjectState.Idle);
                    if (currentStateDuration > 0.05f)
                        animator.SetBool("isWalking", false);
                }
            }
        }

        private Direction GetCurrentDirection()
        {
            if(Mathf.Abs(directorVector.x) > Mathf.Abs(directorVector.y))
            {
                return (directorVector.x > 0?Direction.Right:Direction.Left);
            }
            else
            {
                return (directorVector.y > 0 ? Direction.Up : Direction.Down);
            }
        }

        private void ChangeCurrentState(ObjectState state)
        {
            if (CurrentState != state)
            {
                currentStateDuration = 0;
                CurrentState = state;
            }
        }
    }
}
