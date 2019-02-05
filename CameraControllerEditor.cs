using UnityEngine;
using UnityEditor;

using UnityEditor.SceneManagement;


[CustomEditor(typeof(CameraController))]
public class CameraControllerEditor : Editor {

    CameraController cam;

    public GameObject obj = null;


    SerializedProperty distanceProperty;
    SerializedProperty horizontalProperty;
    SerializedProperty verticalProperty;
    SerializedProperty zoomMinimalProperty;
    SerializedProperty zoomMaxProperty;
    SerializedProperty verticalTextProperty;
    SerializedProperty horizontalTextProperty;
    SerializedProperty distanceTextProperty;
    SerializedProperty smoothProperty;
    SerializedProperty focusProperty;
    SerializedProperty heigthDetectProperty;
    SerializedProperty maskProperty;
    SerializedProperty minHeightProperty;
    SerializedProperty blockByEnvProperty;
    SerializedProperty focusYAddProperty;
    SerializedProperty rangeYProperty;


    private void OnEnable()
    {
        cam = (CameraController)target;
        


        if (!Application.isPlaying)
        {
            //cam.moveCam();
        }
        distanceProperty = serializedObject.FindProperty("distance");
        horizontalProperty = serializedObject.FindProperty("h");
        verticalProperty = serializedObject.FindProperty("v");
        zoomMinimalProperty = serializedObject.FindProperty("minDistance");
        zoomMaxProperty = serializedObject.FindProperty("maxDistance");
        verticalTextProperty = serializedObject.FindProperty("verticalText");
        horizontalTextProperty = serializedObject.FindProperty("horizontalText");
        distanceTextProperty = serializedObject.FindProperty("DistanceText");
        smoothProperty = serializedObject.FindProperty("smooth");
        focusProperty = serializedObject.FindProperty("focus");
        heigthDetectProperty = serializedObject.FindProperty("heigthDetect");
        maskProperty = serializedObject.FindProperty("mask");
        minHeightProperty = serializedObject.FindProperty("minHeight");
        blockByEnvProperty = serializedObject.FindProperty("blocByEnv");
        focusYAddProperty = serializedObject.FindProperty("focusYAdd");
        rangeYProperty = serializedObject.FindProperty("rangeY");

    }

    public override void OnInspectorGUI()
    {

        serializedObject.Update();

        distanceProperty.floatValue = EditorGUILayout.Slider("Distance", cam.Distance, cam.minDistance, cam.maxDistance);
        ////cam.H = EditorGUILayout.Slider("Horizontal angle", cam.H, 0, 360);
        verticalProperty.floatValue = EditorGUILayout.Slider("Vertical angle", cam.V, cam.minY, cam.maxY);
        horizontalProperty.floatValue = EditorGUILayout.Slider("Horizontal angle", cam.H, cam.minX, cam.maxX);

        EditorGUILayout.PropertyField(focusProperty);
        EditorGUILayout.PropertyField(zoomMinimalProperty);
        EditorGUILayout.PropertyField(zoomMaxProperty);
        EditorGUILayout.PropertyField(smoothProperty);
        EditorGUILayout.PropertyField(verticalTextProperty);
        EditorGUILayout.PropertyField(horizontalTextProperty);
        EditorGUILayout.PropertyField(distanceTextProperty);
        EditorGUILayout.PropertyField(heigthDetectProperty);
        EditorGUILayout.PropertyField(maskProperty);
        EditorGUILayout.PropertyField(minHeightProperty);
        EditorGUILayout.PropertyField(blockByEnvProperty);
        EditorGUILayout.PropertyField(focusYAddProperty);

        if (!Application.isPlaying && cam.focus != null )
        {
            cam.MoveSmoothlyCam();
            cam.RotateSmoothlyCam();
        }

        serializedObject.ApplyModifiedProperties();
    }

}
