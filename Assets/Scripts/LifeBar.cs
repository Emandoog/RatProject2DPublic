using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    public Slider _LifeBar;
    public SOPlayerStats _PlayerStats;
    
    public Image _Fill;
    // Start is called before the first frame update
    void Start()
    {
        _LifeBar = GetComponent<Slider>();
        UpdateLifeBar();
    }

    // Update is called once per frame
    
    public void UpdateLifeBar() 
    {
        _LifeBar.value = _PlayerStats.health /_PlayerStats.maxHealth;
    }
    public void StartPoison()
    {

        _Fill.color = Color.green;

    
    
    }
    public void StopPoison()
    {

        _Fill.color = Color.white;
    }

}
