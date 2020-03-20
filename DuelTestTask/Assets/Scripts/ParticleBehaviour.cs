using UnityEngine;

public sealed class ParticleBehaviour : MonoBehaviour
{
    private float lifeDuration = 4f;
    void Update()
    {
        if (lifeDuration <= 0)
            Destroy(gameObject);
        else
            lifeDuration -= Time.deltaTime;
    }
}
