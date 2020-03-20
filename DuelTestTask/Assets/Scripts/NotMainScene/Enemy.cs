using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private List<Rigidbody2D> enemyBody;
    [SerializeField] private List<HingeJoint2D> hinjJoint;
    [HideInInspector] public EntitySpawner entitySpawner;
    private int Health = 2;
    private float Timer = 3f;
    private bool IsDead = false;

    public void TakeDamage(int damage)
    {
        if (!IsDead)
        {
            Health += damage;
            if (Health <= 0)
            {
                entitySpawner.Score = 1;
                StartCoroutine(Destroy());
                foreach (var item in hinjJoint)
                {
                    item.breakForce = 0;
                    item.breakTorque = 0;
                }
                foreach (var item in enemyBody)
                {
                    item.AddForce(new Vector2(Random.Range(0.3f, 1) * 5, Random.Range(1, -1) * 4), ForceMode2D.Force);
                }
                IsDead = true;
            }
        }
    }

    IEnumerator Destroy()
    {
        while (!Helper.Timer(Timer))
        {
            Timer -= Time.deltaTime;
            yield return null;
        }
        entitySpawner.OnObjNeedToBeSpawned(ObjToSpawn.enemy);
        Destroy(gameObject);
    }
}