using UnityEngine;
using UnityEngine.InputSystem; // 1. Importar o novo Input System

[RequireComponent(typeof(Rigidbody2D))]
public class Jogador : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Transform aimTranform;

    private Rigidbody2D rb;
    private Camera cam;

    // Variáveis para guardar o input lido
    private Vector2 moveInput;
    private Vector2 mouseWorldPos;

    // 2. Variáveis para referenciar os dispositivos
    private Keyboard keyboard;
    private Mouse mouse;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;

        // 3. Pega os dispositivos atuais
        // (É bom checar se não são nulos, caso não haja teclado/mouse)
        keyboard = Keyboard.current;
        mouse = Mouse.current;
    }

    private void Update()
    {
        // --- 4. LEITURA MANUAL DO INPUT ---

        // Reseta o input de movimento
        moveInput = Vector2.zero;

        // Verifica o Teclado (Keyboard)
        if (keyboard != null)
        {
            if (keyboard.wKey.isPressed)
            {
                moveInput.y += 1;
            }
            if (keyboard.sKey.isPressed)
            {
                moveInput.y -= 1;
            }
            if (keyboard.aKey.isPressed)
            {
                moveInput.x -= 1;
            }
            if (keyboard.dKey.isPressed)
            {
                moveInput.x += 1;
            }
        }

        // Verifica o Mouse
        if (mouse != null)
        {
            // Lê a posição do mouse na tela (em pixels)
            Vector2 screenPos = mouse.position.ReadValue();
            
            // Converte para a posição no "mundo" do jogo
            mouseWorldPos = cam.ScreenToWorldPoint(screenPos);
        }

        // --- LÓGICA DA MIRA (VISUAL) ---
        Vector2 lookDirection = mouseWorldPos - (Vector2)transform.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

        // Descomente a linha abaixo se seu sprite de mira aponta para CIMA
        // angle -= 90f; 

        aimTranform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void FixedUpdate()
    {
        // Aplica o movimento na física
        rb.linearVelocity = moveInput.normalized * moveSpeed;
    }
}