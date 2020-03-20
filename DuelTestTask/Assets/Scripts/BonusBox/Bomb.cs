using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private Transform TickCanvas;
    [SerializeField] private Text TickText;
    [SerializeField] private Animator Anim;    
    
    private List<Collider2D> Colliders = new List<Collider2D>();
 
    private bool Exploded = false;
    private Vector2 CanvasOffset;
    private float Timer = 10f;

    private void Awake()
    {        
        CanvasOffset = new Vector2(1.5f, 1.5f);
    }
    private void Update()
    {        
        Ticking();     
    }
    
    private void Ticking() 
    {
        if ((Timer -= Time.deltaTime) >= 1.5f)
        {
            TickText.text = Mathf.Floor(Timer / 2).ToString();
        }
        else if(!Exploded)
                Explode();            
    }
    private void FixedUpdate()
    {
        CanvasPosition();
    }

    private void CanvasPosition() 
    {
        TickCanvas.position = (Vector2)this.transform.position + CanvasOffset;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player") && !Colliders.Contains(collision))
        {
            Colliders.Add(collision);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
            Colliders.Remove(collision);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag.Equals("Player"))
        {
            if (!Exploded) Explode();
        }
    }
    public void Explode()
    {
        Exploded = true;
        Anim.enabled = true;
        if (Colliders.Any())
        {
            Colliders[0].transform.parent.GetComponent<Player>().TakeDamage(-5);
        }
    }
    private void Destroy()
    {
        Destroy(transform.parent.gameObject);
    }
}