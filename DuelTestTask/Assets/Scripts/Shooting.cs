using UnityEngine;
using UnityEngine.UI;
public sealed class Shooting : MonoBehaviour
{
    [SerializeField] private Transform StartPoint;
    [SerializeField] private BulletBehaviour BulletPrefab;
    [SerializeField] private BulletBehaviour RocketPrefab;
    [SerializeField] private Image TintImage;
    [SerializeField] private Button ShootButton;
    [SerializeField] private Sprite GunImage;
    [SerializeField] private Sprite JavelinImage;
    private BulletBehaviour AmoToSpawn;
    private float JavelinTimer = 5f;
    private float coolDown = 2f;
    private bool pressed = false;
    private bool isJavelin = false;

    private void Awake()
    {
        AmoToSpawn = BulletPrefab;
    }
    private void Update()
    {
        if(pressed)
            CoolDown();
        if (isJavelin)
            JavelinDischarge();
    }

    public void SwapWeapon(bool isRocket)
    {
        if (isRocket)
        {
            AmoToSpawn = RocketPrefab;
            this.GetComponent<SpriteRenderer>().sprite = JavelinImage;
            isJavelin = true;
        }
        else
        {
            AmoToSpawn = BulletPrefab;
            this.GetComponent<SpriteRenderer>().sprite = GunImage;
            isJavelin = false;
            JavelinTimer = 5f;
        }
    }
    private void JavelinDischarge() 
    {
        if (Helper.Timer(JavelinTimer))
            SwapWeapon(false);
            
        JavelinTimer -= Time.deltaTime;
    }
    private void CoolDown()
    {
        if (TintImage.fillAmount <= 0)
        {
            pressed = false;
            ShootButton.interactable = true;
        }
        TintImage.fillAmount -= 1 / coolDown * Time.deltaTime;

    }
    public void Shoot(int direction)
    {     
        ShootButton.interactable = false;
        TintImage.fillAmount = 1;
        pressed = true;
        var amo = Instantiate(AmoToSpawn, StartPoint.position, Quaternion.identity);
        amo.IsJavelin = isJavelin;
        amo.Player = transform.parent.GetComponent<Player>();
        if (direction == -1) 
        {
            amo.transform.RotateAround(amo.transform.position, transform.up, 180);
        }
        amo.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 6 * direction, ForceMode2D.Impulse);
    }
}