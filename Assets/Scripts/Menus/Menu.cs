using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public bool loaded = false;
    public SaveData data;
    public int planesSize = 9;
    public GameOptions gameOptions;
    public RogueliteSave rogueliteSave;

    public AudioManager audioManager;

    public virtual void StartMenu()
    {
        loadOptionsAndData();
    }

    public void loadOptionsAndData(){
        gameOptions = FileManager.loadOptions();
        data = FileManager.loadData(planesSize);
        rogueliteSave = FileManager.loadRoguelite();
        audioManager.updateVolume();
        loaded = true;
    }

    public virtual void saveOptionsAndData(){
        FileManager.saveData(data);
        FileManager.saveOptions(gameOptions);
        FileManager.saveRoguelite(rogueliteSave);
    }

    public void setVolume(){
        audioManager.updateVolume();
    }
}
