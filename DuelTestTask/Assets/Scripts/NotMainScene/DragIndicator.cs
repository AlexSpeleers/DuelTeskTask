using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragIndicator : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler 
{
    private Vector2 Force;
    private Vector2 MaxPower;
    private Vector2 MinPower;    
    private Vector2 StartPoint;
    private Vector3 EndPoint;
    private Camera camera;
    private float Power = 10f;    
    [SerializeField] private LineRenderer ForceLine;
    [HideInInspector] public Rigidbody2D ObjectToThrow;
    [SerializeField] private ThrowMechanic throwMechanic;

    [Header("Predicted Line variables")]
    [SerializeField] private LineRenderer PredictedPathLine;
    private int Resolution = 10;
    void Awake()
    {
        camera = Camera.main;
        MinPower = new Vector2(0.0f, 0.0f);
        MaxPower = new Vector2(2.0f, 2.0f);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        StartPoint = camera.ScreenToWorldPoint(eventData.position); 
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        StopAllCoroutines();
        EndPoint = camera.ScreenToWorldPoint(eventData.position);
        Force = new Vector2(Mathf.Clamp(StartPoint.x - EndPoint.x, MinPower.x, MaxPower.x),
            Mathf.Clamp(StartPoint.y - EndPoint.y, MinPower.y, MaxPower.y));
        if (Force.x > 0 && Force.y > 0)
        {
            Vector2 force = Force * Power;
            throwMechanic.ThrowAxe(force);
            ObjectToThrow = null;
        }
        ForceLine.positionCount = 0;
        PredictedPathLine.positionCount = 0;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (ObjectToThrow != null)
        {
            ForceLine.positionCount = 2;
            Vector3[] points = { StartPoint, camera.ScreenToWorldPoint(eventData.position) };
            ForceLine.SetPositions(points);
            Force = new Vector2(Mathf.Clamp(StartPoint.x - camera.ScreenToWorldPoint(eventData.position).x, MinPower.x, MaxPower.x),
                Mathf.Clamp(StartPoint.y - camera.ScreenToWorldPoint(eventData.position).y, MinPower.y, MaxPower.y));
            StartCoroutine(ShowTrajectory(ObjectToThrow.position, Force * Power));
        }
    }
    IEnumerator ShowTrajectory(Vector3 origin, Vector3 Speed) 
    {
        Vector3[] point = new Vector3[Resolution];
        PredictedPathLine.positionCount = point.Length;
        for (int i = 0; i < point.Length; i++)
        {
            float time = i * 0.1f;
            point[i] = origin + Speed * time + Physics.gravity * time * time / 2f;
        }
        PredictedPathLine.SetPositions(point);
        yield return null;
    }
}