using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField] 
    private Transform aimTranform;

    private Rigidbody2D rigidbody;
    private Vector2 movementInput;
    private Vector2 smoothedMovementInput;
    private Vector2 movementInputSmoothVelocity;
    private Vector2 mouseWorldPos;

    private Mouse mouse;
    private Camera cam;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        mouse = Mouse.current;
        cam = Camera.main;
    }

    private void Update()
    {
        if (mouse != null)
        {
            Vector2 screenPos = mouse.position.ReadValue();
            mouseWorldPos = cam.ScreenToWorldPoint(screenPos);

        }

        Vector2 lookDirection = mouseWorldPos - (Vector2)transform.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

        aimTranform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void FixedUpdate()
    {
        smoothedMovementInput = Vector2.SmoothDamp(smoothedMovementInput, movementInput, ref movementInputSmoothVelocity, 0.1f);
        rigidbody.linearVelocity = smoothedMovementInput * speed;
    }

    private void OnMove(InputValue inputValue)
    {
        movementInput = inputValue.Get<Vector2>();
    }

    public void AddSpeed(float amount)
    {
        speed += amount;
    }

}
