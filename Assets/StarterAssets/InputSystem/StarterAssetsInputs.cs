using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
		public bool squat;
		public bool openDoor;
		public bool collect;
		public bool attack;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}

        public void OnSquat(InputValue value)
        {
            SquatInput(value.isPressed);
        }

        public void OnAttack(InputValue value)
        {
            AttackInput(value.isPressed);
        }

        public void OnOpenDoor(InputValue value)
        {
            OpenDoorInput(value.isPressed);
        }

        public void OnCollect(InputValue value)
        {
            CollectInput(value.isPressed);
        }
#endif


        public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}

        public void SquatInput(bool newSquatState)
        {
            squat = newSquatState;
        }

        public void AttackInput(bool newAttackState)
        {
            attack = newAttackState;
        }
        public void OpenDoorInput(bool newOpenDoorState)
        {
            openDoor = newOpenDoorState;
        }

        public void CollectInput(bool newCollectState)
        {
            collect = newCollectState;
        }

        private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
	
}