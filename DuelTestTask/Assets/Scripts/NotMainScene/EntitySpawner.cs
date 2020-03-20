using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public enum ObjToSpawn 
{
    enemy,
    axe
}

public class EntitySpawner : MonoBehaviour
{
    public UnityAction<ObjToSpawn> OnObjNeedToBeSpawned;
    [SerializeField] private Enemy enemy;
    [SerializeField] private AxeBehaviour axe;
    [SerializeField] private ThrowMechanic throwMechanic;
    [SerializeField] private Rigidbody2D bodyForJoint;
    [SerializeField] private DragIndicator dragIndicator;
    [SerializeField] private Text scoreText;
    private int score = 0;
    public int Score { set { scoreText.text = $"Score: {score + value}"; score += value;} }
    private void Awake()
    {
        OnObjNeedToBeSpawned += SpawnEnemy;
        SpawnEnemy(ObjToSpawn.axe);
        SpawnEnemy(ObjToSpawn.enemy);
    }
    

    private void SpawnEnemy(ObjToSpawn obj)
    {
        if (obj == ObjToSpawn.enemy)
        {
            var go = Instantiate(enemy, transform.position, Quaternion.identity);
            go.entitySpawner = this;
        }
        else 
        {
            var go = Instantiate(axe, new Vector2(-13.4f, -2.5f), Quaternion.identity);
            go.GetComponent<HingeJoint2D>().connectedBody = bodyForJoint;
            throwMechanic.GetInfoAboutObj(go);
            dragIndicator.ObjectToThrow = go.GetComponent<Rigidbody2D>();
        }
    }

    private void OnDestroy()
    {
        OnObjNeedToBeSpawned = null;
    }
}