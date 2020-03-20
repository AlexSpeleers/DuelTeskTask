using UnityEngine;
using UnityEngine.UI;

public class MedKit : BonusBox, IDamageable
{
    [HideInInspector] public Player PlayerToInfluence;
    [SerializeField] private Image HealthSlider;
    private int Health = 10;
    private int HealAmount = 3;

    public void TakeDamage(int damage)
    {
        Health += damage;
        if (Health <= 0)
        {
            HealthSlider.fillAmount = 0;
            PlayerToInfluence.TakeDamage(HealAmount);
            Destroy(gameObject);
            return;
        }
        HealthSlider.fillAmount = Health / 10f;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag.Equals(BulletTag))
            OnDamagedWithBullet(collision);
    }

    public override void OnDamagedWithBullet(Collision2D collision)
    {
        PlayerToInfluence = collision.transform.GetComponent<BulletBehaviour>().Player;
        TakeDamage(-3);
        Destroy(collision.gameObject);
    }
}
