using UnityEngine;

public class ShieldAura : MonoBehaviour
{
    private Player player;
    private void Awake()
    {
        player = this.transform.parent.GetComponent<Player>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Equals("Bullet") && player != collision.GetComponent<BulletBehaviour>().Player)
        {
            Destroy(collision.gameObject);
        }
        else if (collision.name.Equals("Bomb"))
        {
            ApplyForce(collision.GetComponent<Rigidbody2D>());
        }
    }
    private void ApplyForce(Rigidbody2D target) 
    {
        target.AddForce(-target.velocity*8);
    }
}
