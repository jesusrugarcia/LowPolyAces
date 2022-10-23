using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public PlaneManager plane;

    public Slider slider;
    public GameObject MissileIcon;
    public GameObject DefenseIcon;
    public Image ammo;

    public void setHealth(){
        slider.gameObject.SetActive(plane.stats.health < plane.stats.maxHealth);
        slider.value = plane.stats.health;
        slider.maxValue = plane.stats.maxHealth;
        
    }

    public void setAmmo(){
        ammo.fillAmount = plane.planeShooter.shootTimer/plane.stats.shootSpeed;
    }


    private void FixedUpdate() {
        Vector3 pos = plane.transform.position;
        pos.z += 0.5f;
        transform.position = pos;
        
        setHealth();
        setAmmo();
        SetMissileIcon();
        SetDefenseIcon();
    }

    public void SetMissileIcon(){
        if(plane.stats.specialAmmo > 0){
            MissileIcon.SetActive(true);
        } else {
            MissileIcon.SetActive(false);
        }
        
    }

    public void SetDefenseIcon(){
        if(plane.stats.defenseAmmo > 0){
            DefenseIcon.SetActive(true);
        } else {
            DefenseIcon.SetActive(false);
        }
    }

}
