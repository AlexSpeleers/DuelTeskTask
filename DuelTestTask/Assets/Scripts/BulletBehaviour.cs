using System.Collections;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private ParticleSystem Blood;
    [SerializeField] private ParticleSystem Spark;
    [SerializeField] private Rigidbody2D BulletRB;
    [HideInInspector] public bool IsJavelin;
    private Player player;
    public Player Player { get => player; set => player = value; }
    private float LifeDuration = 8f;
    private Vector2 BulletDirection;
    private void Start()
    {
        player.OnDead.AddListener(() => Destroy(gameObject));
        gameObject.name = "Bullet";
        BulletDirection.y = -12f;
        StartCoroutine(LifeTimer());
    }

    IEnumerator LifeTimer() 
    {
        while (!Helper.Timer(LifeDuration)) 
        {
            LifeDuration -= Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }

    private void SpawnBleed(Collision2D collision) 
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            Instantiate(Blood, contact.point, Quaternion.identity);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var name = collision.gameObject.name;
        var tag = collision.gameObject.tag;
        if (tag.Equals("Player"))
        {
            SpawnBleed(collision);
            if (!IsJavelin)
            {
                if (name.Equals("Head"))
                {
                    collision.transform.parent.GetComponent<Player>().TakeDamage(-3);
                }
                else if (name.Equals("Torso"))
                {
                    collision.transform.parent.GetComponent<Player>().TakeDamage(-2);
                }
                else
                {
                    collision.transform.parent.GetComponent<Player>().TakeDamage(-1);
                }
            }
            else
                collision.transform.parent.GetComponent<Player>().TakeDamage(-5);
            Destroy(gameObject);
        }
        else
        {
            if (tag.Equals("Bomb"))
            {
                collision.transform.GetComponent<Rigidbody2D>().AddForce(BulletRB.velocity / 2, ForceMode2D.Force);
            }
            Instantiate(Spark, this.transform.position, Quaternion.identity);
            BulletRB.velocity = -BulletRB.velocity.normalized * 5 + BulletDirection;
            this.GetComponent<PolygonCollider2D>().enabled = false;
        }
    }
    private void OnDestroy()
    {
        player.OnDead.RemoveListener(() => Destroy(gameObject));
    }
}