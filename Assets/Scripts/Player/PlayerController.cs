using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private int maxHealth = 100;
    [SerializeField]
    private int currentHealth;

    private Rigidbody2D rb2D;
    private Vector2 targetPosition;
    [SerializeField]
    private HealthBar healthBar;

    private void Awake() {
        rb2D = GetComponent<Rigidbody2D>();
        targetPosition = rb2D.position;
        currentHealth = maxHealth;
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        // This may be unnecessary because it seems that touches are registered as clicks.
        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);
            targetPosition = Camera.main.ScreenToWorldPoint(touch.position);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            Heal(20);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
        }
    }

    private void FixedUpdate() {
        Vector2 newPosition = Vector2.MoveTowards(rb2D.position, targetPosition, movementSpeed);
        rb2D.MovePosition(newPosition);

        // TODO: Maybe set targetPosition to currentPosition when the player is stopped. Then, the player
        // won't continually try to reach an unreachable targetPosition when blocked by a collider.
    }
    
    public void TakeDamage(int damage)
    {
       
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
    }
    
    public void Heal(int heal)
    {
       
        currentHealth += heal;
        healthBar.SetHealth(currentHealth);

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

    }
}
