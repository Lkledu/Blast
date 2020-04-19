using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class reset_scene : MonoBehaviour {

    public status player1;
    public status player2;

    public void ResetScene() {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        
        Time.timeScale = 1;
        player1.life = 300;
        player2.life = 300;
}
}
