using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private static readonly string SavePath = Application.persistentDataPath + "/Save/";

    public static void Save(List<IngredientsController.IngredientDisposition> _objects)
    {
        string json = string.Empty;

        foreach (var item in _objects)
        {
            json += JsonUtility.ToJson(item) + " ";
        }
        if (!Directory.Exists(SavePath))
            Directory.CreateDirectory(SavePath);
        File.WriteAllText(SavePath + "LastSave.json", json);

    }

    public static List<IngredientsController.IngredientDisposition> LoadLastSavedData()
    {
        List<IngredientsController.IngredientDisposition> returnList = new List<IngredientsController.IngredientDisposition>();
        
        if (File.Exists(SavePath + "LastSave.json"))
        {
            string json = File.ReadAllText(SavePath + "LastSave.json");
            if(json != string.Empty)
            {
                string[] strings = json.Split(' ');

                foreach (string s in strings)
                    if (s != "")
                        returnList.Add(JsonUtility.FromJson<IngredientsController.IngredientDisposition>(s));
            }
        }
        
        return returnList;
    }
}
