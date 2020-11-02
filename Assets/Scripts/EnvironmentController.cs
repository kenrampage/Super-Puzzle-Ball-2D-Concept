using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SPB
{

    public class EnvironmentController : MonoBehaviour
    {
        private GameInput controls;

        private Vector2 inputVector;

        public GameObject levelMovableObject;
        public GameManager gameManager;

        public float rotationSpeed;

        private void Awake()
        {
            // Sets controls variable for InputAction asset
            controls = new GameInput();

            // Subscribes to events and directs output
            controls.Player.Move.performed += context => SetVectorInput(context.ReadValue<Vector2>());
            controls.Player.Move.canceled += context => SetVectorInput(new Vector2(0, 0));
        }

        private void Start()
        {
            levelMovableObject = transform.Find(GameManager.level.ToString()).transform.Find("Moveable").gameObject;
            
            if (GameManager.level > 0)
            {
                transform.Find(GameManager.level.ToString()).gameObject.SetActive(true);
            }

            Time.timeScale = 1f;
            gameManager.gameIsPaused = false;


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
            // Moves/rotates environment based on Vector2 input
            MoveEnvironment(inputVector);
        }

        // Moves/rotates environmentPivotObject based on Vector2 input 
        public void MoveEnvironment(Vector2 input)
        {
            levelMovableObject.transform.Rotate(Vector3.back, input.x * rotationSpeed);
        }

        // Sets inputVector variable from input action events
        public void SetVectorInput(in Vector2 context)
        {
            inputVector = context;
        }


    }
}
