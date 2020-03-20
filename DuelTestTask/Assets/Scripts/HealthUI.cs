using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Image HealthIcon;
    [SerializeField] private Player Player;

    private void Awake()
    {
        HealthBarInit();
    }
    private void HealthBarInit()
    {
        for (int i = 0; i < Player.Health; i++)
            Instantiate(HealthIcon, this.transform);
    }

    public void HealthIconCalculate(int damage)
    {
        if (damage <= 0)
        {
            if (transform.childCount != 0)
            {
                var amount = Mathf.Abs(damage) < transform.childCount ? Mathf.Abs(damage) : transform.childCount;
                for (int i = 0; i < amount; i++)
                {
                    Destroy(transform.GetChild(i).gameObject);
                }                
            }
        }
        else
        {
            if (transform.childCount != 5)
            {                
                if (transform.childCount + damage > 5)
                {
                    for (int i = 0; i < 5 - damage; i++)
                        Instantiate(HealthIcon, this.transform);
                }
                else
                {
                    for (int i = 0; i < damage; i++)
                    {
                        Instantiate(HealthIcon, this.transform);
                    }
                }
            }
        } 
    }
}
