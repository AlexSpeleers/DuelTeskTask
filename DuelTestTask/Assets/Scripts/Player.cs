using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour, IDamageable
{
    private int health = 5;
    public int Health { get => health; }
    private bool IsShildAura = false;
    private int JumpCount = 0;
    private float ShieldTimer;
    private float JumpForce = 8;
    private const string FloorTag = "Floor";
    [SerializeField] private Shooting CustomShoot;
    [SerializeField] private GameObject ShieldAura;
    [SerializeField] private Animator animator;
    [SerializeField] private HealthUI HealthUI;
    [SerializeField] private WinPanel WinPanel;
    [SerializeField] private Spawner Spawner;
    [HideInInspector] public UnityEvent OnDead;

    private void Update()
    {
        if (IsShildAura)
            ShieldTicking();
    }
    public void Jump(Rigidbody2D player)
    {
        if (JumpCount <= 1)
        {
            player.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
            JumpCount++;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals(FloorTag))
        {
            animator.SetBool("IsGrounded", false);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals(FloorTag))
        {
            JumpCount = 0;
            animator.SetBool("IsGrounded", true);
        }
    }

    private void Die() 
    {
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        if (!IsShildAura || damage > 0)
        {
            health = health + damage > 5 ? 5 : health + damage;
            HealthUI.HealthIconCalculate(damage);
            if (health <= 0)
            {
                Spawner.Disable();
                OnDead.Invoke();                
                WinPanel.OnWon(this.transform.position);
                animator.SetBool("IsDead", true);
            }
        }
    }
    public void ActivateShield()
    {
        ShieldTimer = 5f;
        IsShildAura = true;
        ShieldAura.SetActive(true);
    }
    private void ShieldTicking()
    {
        if (Helper.Timer(ShieldTimer))
        {
            IsShildAura = false;
            ShieldAura.SetActive(false);
        }
        ShieldTimer -= Time.deltaTime;
    }
    public void ActivateJavelin()
    {
        CustomShoot.SwapWeapon(true);
    }
}