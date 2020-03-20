using UnityEngine;

public class Javelin : BonusBox
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals(BulletTag))
            OnDamagedWithBullet(collision);
    }
    public override void OnDamagedWithBullet(Collision2D collision)
    {
        collision.gameObject.GetComponent<BulletBehaviour>().Player.ActivateJavelin();
        Destroy(collision.gameObject);
        Destroy(gameObject);
    }
}
