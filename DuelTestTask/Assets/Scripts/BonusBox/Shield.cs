using UnityEngine;

public class Shield : BonusBox
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag.Equals(BulletTag))
            OnDamagedWithBullet(collision);
    }

    public override void OnDamagedWithBullet(Collision2D collision)
    {
        collision.gameObject.GetComponent<BulletBehaviour>().Player.ActivateShield();
        Destroy(gameObject);
        Destroy(collision.gameObject);
    }
}
