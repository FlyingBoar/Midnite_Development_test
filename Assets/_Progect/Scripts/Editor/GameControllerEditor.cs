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
    SerializedProperty gameSelection;
    bool editorPlaying;


    private void OnEnable()
    {
        randomizeIngredients = serializedObject.FindProperty("RandomizeIngredientQuantity");
        maxQuantity = serializedObject.FindProperty("MaxIngredientQuantity");
        fixedQuantity = serializedObject.FindProperty("FixedIngredientQuantity");
        gameSelection = serializedObject.FindProperty("CurrentGameType");
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
            EditorGUILayout.LabelField("Max quantity");
            maxQuantity.intValue = EditorGUILayout.IntSlider(maxQuantity.intValue, 5, 9);
        }
        else
        {
            EditorGUILayout.LabelField("Fixed quantity of ingredients");
            fixedQuantity.intValue = EditorGUILayout.IntSlider(fixedQuantity.intValue, 4, 9);
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        GUI.enabled = !EditorApplication.isPlaying;

        EditorGUILayout.PropertyField(gameSelection);

        GUI.enabled = true;

        serializedObject.ApplyModifiedProperties();
    }
}
