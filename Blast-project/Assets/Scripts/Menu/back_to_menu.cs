using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class back_to_menu : MonoBehaviour {

    public status player1;
    public status player2;

    public void backToMenu() {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
        player1.life = 300;
        player2.life = 300;

    }
}
