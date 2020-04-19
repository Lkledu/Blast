using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class main_menu_cam : MonoBehaviour {

    public EventSystem evSystem;
    public GameObject mainFirstObj;
    public GameObject optionFirstObj;
    public GameObject selectFightFirstObj;

    private void Start()
    {
        evSystem.SetSelectedGameObject(mainFirstObj);
    }

    public void CreateLevel()
    {
        StartCoroutine(Rotate(Vector3.up, 90f, 0.5f));
        evSystem.SetSelectedGameObject(selectFightFirstObj);
    }

    public void OptionScreen()
    {
        StartCoroutine(Rotate(Vector3.up, -90f, 0.5f));
        evSystem.SetSelectedGameObject(optionFirstObj);
    }

    public void BackToMenu()
    {
        if (transform.rotation.eulerAngles.y == 270f) {
            StartCoroutine(Rotate(Vector3.up, 90f, 0.5f));
        } else {
            StartCoroutine(Rotate(Vector3.up, -90f, 0.5f));
        }
        evSystem.SetSelectedGameObject(mainFirstObj);
    }

    IEnumerator Rotate(Vector3 axis, float angle, float duration)
    {
        Quaternion from = transform.rotation;
        Quaternion to = transform.rotation;
        to *= Quaternion.Euler(axis * angle);

        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            transform.rotation = Quaternion.Slerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.rotation = to;
    }


}
