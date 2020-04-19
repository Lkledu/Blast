using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class select_level : MonoBehaviour
{

    int level;
    public Image levelImgScreen;
    public Image loadScreen;
    public List<Sprite> levelImage;

    private void Start()
    {
        level = 1;
        levelImgScreen.sprite = levelImage[0];
}

    public void previousLevel() {
        level = level-1;
        if (level < 1) { level = SceneManager.sceneCountInBuildSettings - 1; }
        levelImgScreen.sprite = levelImage[level-1];
        Debug.Log("previous");
    }


    public void nextLevel() {
        level = level+1;
        if (level > SceneManager.sceneCountInBuildSettings - 1) { level = 1; }
        levelImgScreen.sprite = levelImage[level-1];
        Debug.Log("next");
    }

    public void loadLevel() {
        loadScreen.gameObject.SetActive(true);
        SceneManager.LoadSceneAsync(level, LoadSceneMode.Single);
    }

}
