using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject _PauseMenu;
    private bool isMenuActive = false;
    public AudioSource _BGM;
    public Slider _BGMSlider;
    // Start is called before the first frame update
    void Start()
    {
        CloseWindow();
        _BGM.volume = _BGMSlider.value;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isMenuActive)
            {
                isMenuActive = false;
                _PauseMenu.SetActive(false);
                Time.timeScale = 1f;
            }
            else 
            {
                isMenuActive = true;
                _PauseMenu.SetActive(true);
                Time.timeScale = 0f;
            }

        }
    }

    public void CloseWindow()
    {


        isMenuActive = false;
        _PauseMenu.SetActive(false);
        Time.timeScale = 1f;

    }
    public void QuitGame() 
    {
        Application.Quit();
    
    
    
    }
    public void ChangeBGMVolume() 
    {

        _BGM.volume = _BGMSlider.value;



    }
    
}
