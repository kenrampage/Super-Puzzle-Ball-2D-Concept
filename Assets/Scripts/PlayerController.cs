using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRb;
    [SerializeField] GameObject wandPivotObject;
    public float impulseForce = 5;
    public Vector2 mouseDifference;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            playerRb.AddForce(new Vector2(mouseDifference.x, mouseDifference.y) * impulseForce, ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {
        mouseDifference = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;

        RotateOnPivot();

    }

    public void RotateOnPivot()
    {
      float rotationZ = Mathf.Atan2(mouseDifference.y, mouseDifference.x) * Mathf.Rad2Deg;
      wandPivotObject.transform.rotation = Quaternion.Euler(0f,0f,rotationZ);
       
    }
}






