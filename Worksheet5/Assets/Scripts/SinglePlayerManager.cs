using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SinglePlayerManager : MonoBehaviour
{
    [SerializeField]
    Button BackButton;
    private void Start()
    {
        BackButton.onClick.AddListener(
        delegate ()
        {
            OnClick_ReturnMenu();
        });
    }

    public void OnClick_ReturnMenu()
    {
        GameApp.Instance.btnClick();
        SceneManager.LoadScene("Menu");
    }
}
