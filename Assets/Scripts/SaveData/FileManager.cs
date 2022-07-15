using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileManager 
{
    public string saveName = "saveData";
    public string optionName = "gameOptions";

    public void saveData(SaveData data){
        var jsonData = JsonUtility.ToJson(data);

        System.IO.File.WriteAllText(Application.dataPath + "/" + saveName + ".json", jsonData);
    }

    public SaveData loadData(int size){
        try
        {
            var jsonData = System.IO.File.ReadAllText(Application.dataPath + "/" + saveName + ".json");
            var data = JsonUtility.FromJson<SaveData>(jsonData);
            return data;
        }
        catch (System.Exception)
        {
            var data = new SaveData(0,0,0,size);
            data.unlockedPlanes[0] = true;
            return data;
        }
        
    }

    public void saveOptions(GameOptions data){
        var jsonData = JsonUtility.ToJson(data);
        System.IO.File.WriteAllText(Application.dataPath + "/" + optionName + ".json", jsonData);
    }

    public GameOptions loadOptions(){
        try{
            var jsonData = System.IO.File.ReadAllText(Application.dataPath + "/" + optionName + ".json");
            var data = JsonUtility.FromJson<GameOptions>(jsonData);
            return data;
        }
        catch (System.Exception){
            var data = new GameOptions();
            saveOptions(data);
            return data;
        }
        
    }
}
