using System.Collections;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(BoardData))]
public class CustomArrayEditor : PropertyDrawer
{
    private int rowOffset = 70;
    private int columnOffset = 20;
    private int rowIdentifierOffset = 20;
    private int columnIdentifierOffset = 40;

    private int initialColumnIdentifierOffset = 40;

    private int numOfRows = 8;
    private int numOfColumns = 8;
    
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.PrefixLabel(position, label);

        Rect newPosition = position;
        newPosition.y += 18f;

        //Retrieving the row list from BoardData.cs
        SerializedProperty rows = property.FindPropertyRelative("rows");

        int xIndex = 1;
        char yIndex = 'A';

        //Set this to whatever you want so that the column identifier lines up properly with your columns
        newPosition.x += initialColumnIdentifierOffset;

        //Creating Columns Labels
        for (int i = 0; i < numOfColumns; i++)
        {
            EditorGUI.LabelField(newPosition, xIndex.ToString());
            newPosition.x += rowOffset;
            xIndex++;
        }

        newPosition.y += columnOffset;

        //Reset the x position now that you're done drawing all the column identifiers;
        newPosition.x = position.x;

        //Creating the Row Labels
        for (int i = 0; i < numOfRows; i++)
        {
            EditorGUI.LabelField(newPosition, yIndex.ToString());
            yIndex++;
            newPosition.y += columnOffset;
        }

        //Reset the y position now that you're done drawing all the row identifiers
        //Add identifierOffsets to both the x and y so that your array doesn't overlap with your identifiers;
        newPosition.y = position.y + columnIdentifierOffset;
        newPosition.x += rowIdentifierOffset;


        //Creation of the 2D Array in the Unity Editor
        for (int i = 0; i < numOfRows; i++)
        {
            SerializedProperty row = rows.GetArrayElementAtIndex(i).FindPropertyRelative("row");
            newPosition.height = 20;

            if (row.arraySize != numOfRows)
                row.arraySize = numOfRows;

            newPosition.width = 70;

            for (int j = 0; j < numOfRows; j++)
            {
                //Set up in a way where the columns need to be identical to the rows in the matrix. Should be fixable but I'm tired
                EditorGUI.PropertyField(newPosition, row.GetArrayElementAtIndex(j), GUIContent.none);
                newPosition.x += newPosition.width;
            }

            newPosition.x = position.x + 20;
            newPosition.y += 20;
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return 20 * 12;
    }
    
}
