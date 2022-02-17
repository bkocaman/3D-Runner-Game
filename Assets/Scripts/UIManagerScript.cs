using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManagerScript : MonoBehaviour
{
    [SerializeField] private GameObject GameOverUI;
    [SerializeField] private GameObject EndGameUI;
    [SerializeField] private GameObject PrepareUI;
    public GameObject health;
    public GameObject healthParent;
    public GameObject coinText;
    public GameObject levelText;

    public Text score;
    public Text Endscore;
    public Text GameOverScore;

    public bool gameover = false;
    public bool endgame = false;

    void Start()
    {
        GameOverUI.SetActive(false);
        EndGameUI.SetActive(false);
        PrepareUI.SetActive(true);
        UIManager();
    }

    void Update()
    {

        if (!PlayerController.isLevelStart)
            PrepareUI.SetActive(false);

        if (gameover == true)
        {
            GameOverUI.SetActive(true);
            GameOverScore.text = score.text.ToString();
        }

        else
        {
            GameOverUI.SetActive(false);
        }

        if (endgame == true)
        {
            EndGameUI.SetActive(true);
            Endscore.text = score.text.ToString();
        }

        else
        {
            EndGameUI.SetActive(false);
        }




        UIManager();
    }

    private void UIManager()
    {
        coinText.GetComponent<Text>().text = PlayerController.coins + "";
        levelText.GetComponent<Text>().text = "Level " + PlayerController.currentLevel;

        foreach (Transform child in healthParent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        for (int i = 0; i < PlayerController.health; i++)
        {
            Object.Instantiate(health, healthParent.transform);
        }
    }


    public void TryAgainButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        PlayerPrefs.DeleteAll();

    }
    public void NextButton()
    {

        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (SceneManager.sceneCountInBuildSettings > nextSceneIndex)
        {
            SceneManager.LoadScene(nextSceneIndex);
            PlayerPrefs.DeleteAll();
        }
        PlayerController.currentLevel++;



    }


}
