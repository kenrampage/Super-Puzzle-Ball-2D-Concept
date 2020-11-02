using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SPB;
using UnityEngine.InputSystem;

namespace SPB
{

    // Handles all movement during the game/play scenes
    public class PlayerController : MonoBehaviour
    {
        private GameInput controls;

        private float mouseRotationZ;
        private Vector2 mouseDifference;
        private Vector2 inputVector;

        public Rigidbody2D playerRb;
        public GameObject wandObject;
        public GameObject environmentObject;
        public GameObject gameManager;
        public GameObject cooldownOverlay;

        public float rotationSpeed;
        public float boostForce = 7;
        public float boostCooldown = .5f;

        private float boostNextFireTime = 0;
        private float boostCooldownLeftPercent;


        private void Awake()
        {
            // Sets controls variable for InputAction asset
            controls = new GameInput();

            // Subscribes to events and directs output
            controls.Player.Move.performed += context => SetVectorInput(context.ReadValue<Vector2>());
            controls.Player.Move.canceled += context => SetVectorInput(new Vector2(0, 0));
            controls.Player.Boost.performed += context => BoostPlayer();
        }

        private void OnEnable()
        {
            // Enables input controls
            controls.Enable();
        }

        private void OnDisable()
        {
            // Disables input controls
            controls.Disable();
        }

        private void FixedUpdate()
        {
            // Set the variables for aiming at the mouse
            SetMouseDirection();

            // Rotates wand to point at mouse
            RotateWand(wandObject);

            // Moves/rotates environment based on Vector2 input
            MoveEnvironment(inputVector);

            // Calculates cooldown % remaining and manages visual effects for it
            BoostCooldownEffect();

        }

        // Rotates pivotObjectName to point at mouse cursor
        public void RotateWand(GameObject pivotObjectName)
        {
            pivotObjectName.transform.rotation = Quaternion.Euler(0f, 0f, mouseRotationZ);
        }

        // Moves/rotates environmentPivotObject based on Vector2 input 
        public void MoveEnvironment(Vector2 input)
        {
            environmentObject.transform.Rotate(Vector3.back, input.x * rotationSpeed);
        }

        // Sets inputVector variable from input action events
        public void SetVectorInput(in Vector2 context)
        {
            inputVector = context;
        }

        // Calculates direction and angle between player and mouse
        public void SetMouseDirection()
        {
            // Calculates the difference between mouse position and player position and normalizes it.
            mouseDifference = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - transform.position;
            mouseDifference.Normalize();

            // Calculates the angle in degrees between two normalized values
            mouseRotationZ = Mathf.Atan2(mouseDifference.y, mouseDifference.x) * Mathf.Rad2Deg;
        }

        // Handles boosting player in the direction of the mouse plus setting and checking the cooldown
        public void BoostPlayer()
        {
            if (!GameManager.gameIsPaused)
            {
                if (boostCooldownLeftPercent == 1)
                {
                    playerRb.AddForce(mouseDifference * boostForce, ForceMode2D.Impulse);
                    boostNextFireTime = Time.time + boostCooldown;
                }
                else
                {
                    print((boostNextFireTime - Time.time) + " Seconds Left on the boost cooldown");
                }
            }

        }

        // Calculates boost cooldown then adjusts sprite opacity and enables/disables the wand
        public void BoostCooldownEffect()
        {
            SpriteRenderer spriteRenderer = cooldownOverlay.GetComponent<SpriteRenderer>();
            
            if (boostNextFireTime > Time.time)
            {
                boostCooldownLeftPercent = (boostNextFireTime - Time.time) / boostCooldown;
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, boostCooldownLeftPercent * .4f);
                GameObject.Find("Wand").GetComponent<SpriteRenderer>().enabled = false;


            }
            else
            {
                boostCooldownLeftPercent = 1;
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0);
                GameObject.Find("Wand").GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }
}






