using UnityEditor;
using UnityEngine;
using UnityEngine.Localization.Components;
using TMPro;
using UnityEngine.Events;
using UnityEditor.Events;

public class LocalizeStringEventUpdater : EditorWindow
{
    [MenuItem("Tools/Actualizar LocalizeStringEvent")]
    public static void ShowWindow()
    {
        GetWindow<LocalizeStringEventUpdater>("Actualizar LocalizeStringEvent");
    }

    void OnGUI()
    {
        GUILayout.Label("Actualizar eventos de LocalizeStringEvent", EditorStyles.boldLabel);

        if (GUILayout.Button("Buscar y asignar SetText al m_UpdateString"))
        {
            UpdateAllLocalizeStringEvents();
        }
    }
    private void ClearAllLocalizeStringEvents()
    {

    }
    private void UpdateAllLocalizeStringEvents()
    {
        int updatedCount = 0;

        foreach (GameObject obj in FindObjectsByType<GameObject>(FindObjectsSortMode.None))
        {
            var localizeComponent = obj.GetComponent<LocalizeStringEvent>();
            var textComponent = obj.GetComponent<TextMeshProUGUI>();

            if (localizeComponent != null && textComponent != null)
            {
                SerializedObject serializedObject = new SerializedObject(localizeComponent);
                SerializedProperty updateStringProp = serializedObject.FindProperty("m_UpdateString");

                // Evitamos duplicados limpiando los listeners
                updateStringProp.FindPropertyRelative("m_PersistentCalls.m_Calls").ClearArray();

                EditorUtility.SetDirty(localizeComponent);

                UnityAction<string> action = textComponent.SetText;

                UnityEventTools.AddPersistentListener(localizeComponent.OnUpdateString, action);
                serializedObject.ApplyModifiedProperties();

                EditorUtility.SetDirty(localizeComponent);
                updatedCount++;
            }
        }

        Debug.Log($"Eventos asignados correctamente a {updatedCount} componentes.");
    }
}

