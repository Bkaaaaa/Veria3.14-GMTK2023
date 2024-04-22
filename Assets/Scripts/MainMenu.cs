using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public RectTransform rtBehindStart;
    public RectTransform rtStart;

    private Vector3 rtBehindStartPos;
    private Vector3 rtStartPos;

    public float timerAnimation = 0.5f;

    private void Start()
    {
        rtBehindStartPos = rtBehindStart.position;
        rtStartPos = rtStart.position + (rtBehindStartPos - rtStart.position) * 0.4f;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void approach()
    {
        StartCoroutine(corApproach());
    }

    public void pullBack()
    {
        StopAllCoroutines();
        rtBehindStart.position = rtBehindStartPos;
    }

    IEnumerator corApproach()
    {
        float time = 0f;

        while (time < timerAnimation)
        {
            rtBehindStart.position = new Vector3(
                (rtBehindStart.position.x + rtStartPos.x) / 2f,
                (rtBehindStart.position.y + rtStartPos.y) / 2f,
                0f);

            yield return new WaitForSeconds(0.025f);
            time += 0.025f;
        }
    }
}
