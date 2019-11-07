using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomPropertyDrawer(typeof(Bool3))]
public class Bool3Editor : PropertyDrawer
{
    SerializedProperty X, Y, Z;
    string name;
    bool cache = false;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (!cache)
        {
            //get the name before it's gone
            name = property.displayName;

            //get the X and Y values
            property.Next(true);
            X = property.Copy();
            property.Next(true);
            Y = property.Copy();
            property.Next(true);
            Z = property.Copy();
            cache = true;
        }
        
        Rect contentPosition = EditorGUI.PrefixLabel(position, new GUIContent(name));
        contentPosition.width = 16f;
        float half = 20f;
        GUI.skin.label.padding = new RectOffset(3, 3, 6, 6);
        EditorGUI.LabelField(contentPosition, X.name);
        contentPosition.x += half;
        X.boolValue = EditorGUI.Toggle(contentPosition, X.boolValue);
        contentPosition.x += half;
        EditorGUI.LabelField(contentPosition, Y.name);
        contentPosition.x += half;
        Y.boolValue = EditorGUI.Toggle(contentPosition, Y.boolValue);
        contentPosition.x += half;
        EditorGUI.LabelField(contentPosition, Z.name);
        contentPosition.x += half;
        Z.boolValue = EditorGUI.Toggle(contentPosition, Z.boolValue);



        //contentPosition.x += half;

        //EditorGUI.BeginProperty(contentPosition, label, Z);
        //{
        //    EditorGUI.BeginChangeCheck();
        //    bool newVal = EditorGUI.PropertyField(contentPosition, Z);
        //    if (EditorGUI.EndChangeCheck())
        //       Z.boolValue = newVal;
        //}
        //EditorGUI.EndProperty();
    }
}
