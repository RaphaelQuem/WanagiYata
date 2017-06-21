using UnityEngine;

namespace Assets.Scripts.StateMachine
{
    public enum PlayerState
    {
        Idle,
        Walking,
        SettingTrap,
        Shooting
    }
    public enum PlayerDirection
    {
        Up,
        Right,
        Down,
        Left
    }
    public class PlayerStateMachine
    {
        private float currentStateDuration;
        private PlayerState currentState;
        private PlayerDirection currentDirection;
        private Vector2 directorVector;
        private bool isSettingTrap;

        private Animator animator;
        public PlayerState CurrentState { get { return currentState; } set{ currentState = value; } }
        public PlayerDirection CurrentDirection { get { return currentDirection; } set { currentDirection = value; } }
        public bool IsSettingTrap { get; set; }

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
                    ChangeCurrentState(PlayerState.Walking);
                    CurrentDirection = GetCurrentDirection();
                }
                else
                {
                    ChangeCurrentState(PlayerState.Idle);
                    if (currentStateDuration > 0.05f)
                        animator.SetBool("isWalking", false);
                }
            }
        }

        private PlayerDirection GetCurrentDirection()
        {
            if(Mathf.Abs(directorVector.x) > Mathf.Abs(directorVector.y))
            {
                return (directorVector.x > 0?PlayerDirection.Right:PlayerDirection.Left);
            }
            else
            {
                return (directorVector.y > 0 ? PlayerDirection.Up : PlayerDirection.Down);
            }
        }

        private void ChangeCurrentState(PlayerState state)
        {
            if (CurrentState != state)
            {
                currentStateDuration = 0;
                CurrentState = state;
            }
        }
    }
}
