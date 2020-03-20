using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BonusBox : MonoBehaviour
{
    [SerializeField] protected Text TickText;
    protected const string BulletTag = "Bullet";
    private float Timer = 5f;

    public abstract void OnDamagedWithBullet(Collision2D collision);

    private void Update()
    {
        Ticking();
    }
    private void Ticking()
    {
        if (Helper.Timer(Timer))
            Destroy(gameObject);
        TickText.text = Mathf.Floor(Timer -= Time.deltaTime).ToString();                   
    }
}