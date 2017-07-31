using UnityEngine;

namespace Assets.Scripts.StateMachine
{
    public enum ObjectState
    {
        Idle,
        Walking,
        SettingTrap,
        Shooting,
        Rolling
    }
    public enum FacingDirection
    {
        Up,
        Right,
        Down,
        Left
    }
    public class PlayerStateMachine
    {
        private float currentStateDuration;
        private ObjectState currentState;
        private FacingDirection currentDirection;
        private Vector2 directorVector;
        private bool isSettingTrap;
        private bool isRolling;

        private Animator animator;
        public ObjectState CurrentState { get { return currentState; } set{ currentState = value; } }
        public FacingDirection CurrentDirection { get { return currentDirection; } set { currentDirection = value; } }
        public bool IsSettingTrap { get; set; }
        public bool IsRolling { get { return isRolling; } set { isRolling = value; animator.SetBool("isRolling", IsRolling); } }

        public PlayerStateMachine(Animator anim)
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

        private FacingDirection GetCurrentDirection()
        {
            if(Mathf.Abs(directorVector.x) > Mathf.Abs(directorVector.y))
            {
                return (directorVector.x > 0?FacingDirection.Right:FacingDirection.Left);
            }
            else
            {
                return (directorVector.y > 0 ? FacingDirection.Up : FacingDirection.Down);
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
