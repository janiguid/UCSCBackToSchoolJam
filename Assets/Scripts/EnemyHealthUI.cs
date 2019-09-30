using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyHealthUI : MonoBehaviour
{

    public Slider healthSlider;
    [SerializeField]
    private EnemyHealth enemyHealth;
    
    


    private void Start()
    {
        healthSlider = gameObject.GetComponent<Slider>();
    }
    // Update is called once per frame
    void Update()
    {
        healthSlider.value = enemyHealth.health;
    }
}
