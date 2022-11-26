using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public GameController controller;
    public MainMenu menu;
    public ShopMenu shopMenu;
    public WorldMapManager mapManager;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        if (audioSource == null){
            audioSource = GetComponent<AudioSource>();
        }
        updateVolume();
    }

    // Update is called once per frame
    void Update()
    {
        updateVolume();
    }

    public void updateVolume(){
        if(controller != null){
            audioSource.volume = controller.gameOptions.musicVolume;
        } else if (menu != null){
            audioSource.volume = menu.gameOptions.musicVolume;
        } else if(shopMenu != null) {
            audioSource.volume = shopMenu.gameOptions.musicVolume;
        } else if(mapManager != null){
            audioSource.volume = mapManager.gameOptions.musicVolume;
        }
    }
}
