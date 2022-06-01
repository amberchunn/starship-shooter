using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        float currentPlayerX = transform.position.x;
        float currentPlayerY = transform.position.y;
        float xBoundLeft = -11.3f;
        float xBoundRight = 8.7f;
        float yBoundTop = 4.5f;
        float yBoundBottom = -5.0f;
        // transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);
        // transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);

        // Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        // transform.Translate(direction * _speed * Time.deltaTime);

        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) *_speed * Time.deltaTime);
        // Wrap player position whenever they hit a boundary
        // y bound = 4.5 top, -5.0 bottom

        if (currentPlayerY > yBoundTop) {
            transform.position = new Vector3(currentPlayerX, yBoundBottom, 0);
        } else if (currentPlayerY < yBoundBottom) {
            transform.position = new Vector3(currentPlayerX, yBoundTop, 0);
        }

        // x bound = -11.3 left, 8.7 right
        if (currentPlayerX < xBoundLeft) {
            transform.position = new Vector3 (xBoundRight, currentPlayerY, 0);
        } else if (currentPlayerX > xBoundRight) {
            transform.position = new Vector3 (xBoundLeft, currentPlayerY, 0);
        }

    }
}
