using TDS.HealthSystem;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(HealthComponent))]
public class HealthComponentInspector : Editor
{
    private SerializedProperty _maxHealthProp;
    private SerializedProperty _currentHealthProp;

    private HealthComponent _targetScript;

    private void OnEnable()
    {
        _targetScript = target as HealthComponent;

        _maxHealthProp = serializedObject.FindProperty("_maxHealth");
        _currentHealthProp = serializedObject.FindProperty("_currentHealth");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawHealthFields();

        EditorGUILayout.Space(15);

        DrawHelpButtons();

        serializedObject.ApplyModifiedProperties();
    }

    private void DrawHelpButtons()
    {
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);

        EditorGUILayout.LabelField("Debug Buttons");
        EditorGUILayout.Space(5);

        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("ADD: ");

        if (GUILayout.Button("25%"))
            AddHPinPercent(25);

        if (GUILayout.Button("50%"))
            AddHPinPercent(50);

        if (GUILayout.Button("75%"))
            AddHPinPercent(50);

        if (GUILayout.Button("100%"))
            AddHPinPercent(100);

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("REMOVE: ");

        if (GUILayout.Button("25%"))
            AddHPinPercent(25);

        if (GUILayout.Button("50%"))
            AddHPinPercent(50);

        if (GUILayout.Button("75%"))
            AddHPinPercent(50);

        if (GUILayout.Button("100%"))
            AddHPinPercent(100);

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.EndVertical();
    }

    private void DrawHealthFields()
    {
        float maxHealth = _maxHealthProp.floatValue;
        float currentHealth = _currentHealthProp.floatValue;

        // Ensure that CurrentHealth is within the valid range.
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        EditorGUIUtility.labelWidth = 90.0f;
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(_currentHealthProp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(_maxHealthProp);

        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.FloatField(0, new GUILayoutOption[] { GUILayout.MaxWidth(EditorGUIUtility.singleLineHeight), GUILayout.MinWidth(EditorGUIUtility.singleLineHeight) });
        EditorGUI.EndDisabledGroup();

        Rect sliderPosition = EditorGUILayout.GetControlRect();

        EditorGUI.BeginChangeCheck();
        currentHealth = GUI.HorizontalSlider(sliderPosition, currentHealth, 0f, maxHealth);
        if (EditorGUI.EndChangeCheck())
        {
            _currentHealthProp.floatValue = currentHealth;
        }
        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.PropertyField(_maxHealthProp, GUIContent.none, new GUILayoutOption[] { GUILayout.MaxWidth(50.0f), GUILayout.MinWidth(40.0f) });
        EditorGUI.EndDisabledGroup();
        EditorGUILayout.EndHorizontal();
        var color = GUI.color;
        GUI.color = Color.yellow;
        EditorGUILayout.LabelField("!!! Change in inspector dont invoke events !!!");
        GUI.color = color;

        EditorGUILayout.EndVertical();
    }

    private void AddHPinPercent(int percent) => _targetScript.AddHealth((percent/100)*_targetScript.MaxHealth);
    private void RemoveHPinPercent(int percent) => _targetScript.RemoveHealth((percent / 100) * _targetScript.MaxHealth);
}
