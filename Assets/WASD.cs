using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))] // Требуем наличие аниматора
public class TopDownMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float acceleration = 20f;
    [SerializeField] private float deceleration = 25f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator; // --- НОВОЕ ---
    private Vector2 currentVelocity;
    private bool isMoving; // --- НОВОЕ ---

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>(); // --- НОВОЕ ---
        rb.gravityScale = 0f;
        rb.freezeRotation = true;
    }

    void Update()
    {
        Vector2 input = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        ).normalized;

        // --- ЛОГИКА ПЕРЕВОРОТА ---
        if (input.x < 0f) spriteRenderer.flipX = false;
        else if (input.x> 0f) spriteRenderer.flipX = true;

        // --- ЛОГИКА АНИМАЦИИ ---
        // Проверяем, есть ли вообще ввод от игрока
        isMoving = input != Vector2.zero;

        // Передаем значение в Аниматор
        animator.SetBool("isMoving", isMoving);

        // Бонус: передаем скорость по осям X и Y (пригодится для анимаций ходьбы вверх/вниз)
        animator.SetFloat("moveX", input.x);
        animator.SetFloat("moveY", input.y);
        // --------------------------

        Vector2 targetVelocity = input * moveSpeed;
        float rate = (input == Vector2.zero) ? deceleration : acceleration;
        currentVelocity = Vector2.MoveTowards(currentVelocity, targetVelocity, rate * Time.deltaTime);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + currentVelocity * Time.fixedDeltaTime);
    }
}