using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameController))]
public class GameControllerEditor : Editor
{
    GameController _target;
    
    SerializedProperty randomizeIngredients;
    SerializedProperty maxQuantity;
    SerializedProperty fixedQuantity;

    private void OnEnable()
    {
        randomizeIngredients = serializedObject.FindProperty("RandomizeIngredientQuantity");
        maxQuantity = serializedObject.FindProperty("MaxIngredientQuantity");
        fixedQuantity = serializedObject.FindProperty("FixedIngredientQuantity");
    }

    public override void OnInspectorGUI()
    {
        _target = (GameController)target;

        EditorGUILayout.ObjectField("Script", MonoScript.FromMonoBehaviour((GameController)target), typeof(GameController), false);

        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("Randomize ingredients");
        randomizeIngredients.boolValue = EditorGUILayout.Toggle(randomizeIngredients.boolValue);
        
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        if (_target.RandomizeIngredientQuantity)
        {
            EditorGUILayout.LabelField("Max quantity to take");
            maxQuantity.intValue = EditorGUILayout.IntSlider(maxQuantity.intValue, 5, 8);
        }
        else
        {
            EditorGUILayout.LabelField("Fixed quantity of ingredients");
            fixedQuantity.intValue = EditorGUILayout.IntSlider(fixedQuantity.intValue, 4, 8);
        }
        EditorGUILayout.EndHorizontal();

        serializedObject.ApplyModifiedProperties();
    }
}
