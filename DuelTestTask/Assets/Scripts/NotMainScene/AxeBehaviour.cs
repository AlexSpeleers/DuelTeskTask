using System.Collections;
using UnityEngine;

public class AxeBehaviour : MonoBehaviour
{
    private float LifeTime = 10f;
    [SerializeField] private ParticleSystem Blood;
    [SerializeField] private ParticleSystem Spark;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag.Equals("Player1"))         
        {
            SpawnBleed(collision);
            if (collision.transform.name.Equals("Head")) 
            {
                collision.transform.parent.GetComponent<Enemy>().TakeDamage(-3);
            }
            else
                collision.transform.parent.GetComponent<Enemy>().TakeDamage(-1);
            this.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            this.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        }
        else 
        {
            this.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
            Instantiate(Spark, this.transform.position, Quaternion.identity);
        }
    }

    private void SpawnBleed(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            Instantiate(Blood, contact.point, Quaternion.identity);
        }
    }

    public void OnThrown() 
    {
        StartCoroutine(LifeRoutine());
    }
      
    IEnumerator LifeRoutine ()
    {
        while (!Helper.Timer(LifeTime))
        {
            LifeTime -= Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
}