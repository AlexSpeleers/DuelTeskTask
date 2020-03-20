using System.Collections;
using UnityEngine;

public class ThrowMechanic : MonoBehaviour
{
    [SerializeField] private EntitySpawner entitySpawner;
    private HingeJoint2D AxeHJ;
    private Rigidbody2D ObjectRB;
    private AxeBehaviour AxeBehaviour;
    private PolygonCollider2D AxeCollider;

    public void GetInfoAboutObj(AxeBehaviour obj) 
    {
        AxeHJ = obj.GetComponent<HingeJoint2D>();
        ObjectRB = obj.GetComponent<Rigidbody2D>();
        AxeCollider = obj.GetComponent<PolygonCollider2D>();
        AxeBehaviour = obj;

    }

    public void ThrowAxe(Vector2 force) 
    {
        if (ObjectRB != null)
        {
            AxeCollider.enabled = true;
            AxeHJ.breakForce = 0;
            AxeBehaviour.OnThrown();
            ObjectRB.AddForce(force, ForceMode2D.Impulse);
            StartCoroutine(GetNewAxeRoutine());
            ObjectRB = null;
        }
    }

    IEnumerator GetNewAxeRoutine() 
    {
        yield return new WaitForSeconds(0.4f);
        entitySpawner.OnObjNeedToBeSpawned(ObjToSpawn.axe);
    }
}
