using UnityEngine;
public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] BonusItems = new GameObject[4];
    private float Timer = 6f;

    public void Disable()
    {
        this.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (Helper.Timer(Timer))
            Spawn();
        Timer -= Time.deltaTime;
    }
    private void Spawn() 
    {
        Timer = 6f;
        var obj = Instantiate(BonusItems[Random.Range(0, 4)], transform.position, Quaternion.identity);
        if (obj.tag.Equals("Bomb"))
            Timer = 10f;
    }
}