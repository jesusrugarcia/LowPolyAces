using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FileManager 
{
    public static string saveName = "saveData";
    public static string optionName = "gameOptions";
    public static string mapName = "mapGraph";
    public static string rogueliteName = "rogueliteSave";

    public static void saveData(SaveData data){
        var jsonData = JsonUtility.ToJson(data);

        System.IO.File.WriteAllText(Application.dataPath + "/" + saveName + ".json", jsonData);
    }

    public static SaveData loadData(int size){
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

    public static void saveOptions(GameOptions data){
        var jsonData = JsonUtility.ToJson(data);
        System.IO.File.WriteAllText(Application.dataPath + "/" + optionName + ".json", jsonData);
    }

    public static GameOptions loadOptions(){
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

    public static void saveMap(WorldMapGraph data){
        var jsonData = JsonUtility.ToJson(data);
        System.IO.File.WriteAllText(Application.dataPath + "/" + mapName + ".json", jsonData);
    }

    public static WorldMapGraph loadMap(){
        try{
            var jsonData = System.IO.File.ReadAllText(Application.dataPath + "/" + mapName + ".json");
            var data = JsonUtility.FromJson<WorldMapGraph>(jsonData);
            return data;
        }
        catch (System.Exception){
            return null;
        }
        
    }

    public static void saveRoguelite(RogueliteSave data){
        var jsonData = JsonUtility.ToJson(data);
        System.IO.File.WriteAllText(Application.dataPath + "/" + rogueliteName + ".json", jsonData);
    }

    public static RogueliteSave loadRoguelite(){
        try{
            var jsonData = System.IO.File.ReadAllText(Application.dataPath + "/" + rogueliteName + ".json");
            var data = JsonUtility.FromJson<RogueliteSave>(jsonData);
            return data;
        }
        catch (System.Exception){
            return null;
        }
        
    }
}
