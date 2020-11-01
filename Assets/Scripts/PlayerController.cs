using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SPB;
using UnityEngine.InputSystem;

namespace SPB
{

    public class PlayerController : MonoBehaviour
    {
        private Rigidbody2D playerRb;
        private float squarePivotRotation;
        private float mouseRotationZ;
        private Vector2 mouseDifference;
        private Vector2 movementInput;
        private float xInput;
        private float yInput;

        public GameObject wandPivotObject;
        public GameObject environmentPivotObject;
        public GameObject gameManager;
        public GameInput controls;
        public SpriteRenderer spriteRenderer;


        public float impulseForce = 5;
        public float rotationSpeed;
        public float boostCooldown = 5;
        private float boostNextFireTime = 0;
        private float boostLastFireTime = 0;

        public float boostCooldownLeftPercent;

        private void Awake()
        {
            controls = new GameInput();
            controls.Player.Move.performed += context => SetVectorInput(context.ReadValue<Vector2>());
            controls.Player.Move.canceled += context => SetVectorInput(new Vector2(0, 0));
            controls.Player.Boost.performed += context => BoostPlayer();
        }

        private void OnEnable()
        {
            controls.Enable();
        }

        private void OnDisable()
        {
            controls.Disable();
        }

        void Start()
        {
            playerRb = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        void Update()
        {



        }

        private void FixedUpdate()
        {

            RotatePlayer(wandPivotObject);
            RotateEnvironment(xInput);
            SetMouseDirection();

            if (boostNextFireTime > Time.time)
            {
                boostCooldownLeftPercent = (boostNextFireTime - Time.time) / boostCooldown;
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1 - boostCooldownLeftPercent);
                GameObject.Find("Wand").GetComponent<SpriteRenderer>().enabled = false;


            }
            else
            {
                boostCooldownLeftPercent = 1;
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1);
                GameObject.Find("Wand").GetComponent<SpriteRenderer>().enabled = true;
            }

        }

        public void RotatePlayer(GameObject pivotObjectName)
        {
            pivotObjectName.transform.rotation = Quaternion.Euler(0f, 0f, mouseRotationZ);

        }

        public void RotateEnvironment(float xInput)
        {
            environmentPivotObject.transform.Rotate(Vector3.back, xInput * rotationSpeed);
        }

        public void SetVectorInput(in Vector2 vectorInput)
        {
            xInput = vectorInput.x;
            yInput = vectorInput.y;
        }

        public void SetMouseDirection()
        {
            mouseDifference = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - transform.position;
            mouseDifference.Normalize();
            mouseRotationZ = Mathf.Atan2(mouseDifference.y, mouseDifference.x) * Mathf.Rad2Deg;
        }

        public void BoostPlayer()
        {
            if (!GameManager.gameIsPaused)
            {
                if (boostCooldownLeftPercent == 1)
                {
                    playerRb.AddForce(mouseDifference * impulseForce, ForceMode2D.Impulse);
                    boostNextFireTime = Time.time + boostCooldown;
                    boostLastFireTime = Time.time;
                    print((boostNextFireTime - Time.time) + " Seconds Left on the boost cooldown");


                }
                else
                {
                    print((boostNextFireTime - Time.time) + " Seconds Left on the boost cooldown");
                }
            }


        }
    }
}






