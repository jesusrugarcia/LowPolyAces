using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public PlaneManager plane;

    public Slider slider;

    public void setHealth(){
        slider.gameObject.SetActive(plane.stats.health < plane.stats.maxHealth);
        slider.value = plane.stats.health;
        slider.maxValue = plane.stats.maxHealth;
        
    }

    private void FixedUpdate() {
        Vector3 pos = plane.transform.position;
        pos.z += 0.5f;
        transform.position = pos;
        
        setHealth();
    }

}
