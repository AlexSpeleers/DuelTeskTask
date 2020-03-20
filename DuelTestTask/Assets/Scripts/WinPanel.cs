using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WinPanel : MonoBehaviour
{
    [SerializeField] private Image WinerPanel;
    [SerializeField] private Transform WinText;
    [SerializeField] private Transform ButtonHolder;
    private Color TempColor;
    public void OnWon(Vector2 playerPos) 
    {
        TempColor = new Color(0, 0, 0, 0);
        playerPos.x = -playerPos.x;
        playerPos.y = 0;
        WinerPanel.gameObject.SetActive(true);
        StartCoroutine(AnimateWinPanel(Camera.main.WorldToScreenPoint(playerPos)));
        StartCoroutine(Tint());
        StartCoroutine(ScaleUpButton());
    }

    IEnumerator AnimateWinPanel(Vector2 target)
    {
        target.y += 200;
        while ((Vector2)WinText.position != target)
        {
            WinText.position = Vector2.Lerp(WinText.position, target, 0.9f * Time.deltaTime);
            yield return null;
        }
    }

    public void ShowWinPanel() 
    {
        WinerPanel.gameObject.SetActive(true);
        StartCoroutine(Tint());
        StartCoroutine(ScaleUpButton());
    }
    IEnumerator Tint() 
    {
        while (WinerPanel.color.a != 100) 
        {
            TempColor.a = Mathf.Lerp(WinerPanel.color.a, 0.3f, Time.deltaTime * 0.9f);
            WinerPanel.color = TempColor;
            yield return null;
        }
    }
    IEnumerator ScaleUpButton()
    {
        while (ButtonHolder.localScale != Vector3.one)
        {
            ButtonHolder.localScale = Vector3.Lerp(ButtonHolder.localScale, Vector3.one, Time.deltaTime * 0.5f);
            yield return null;
        } 
    }
}