using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BossSkills))]
public class SmartBoolArrayDrawer : Editor
{
    private static readonly string[] Titles = { "ê∂ê¨ínì_", "å¯â îÕàÕ"};

    public override void OnInspectorGUI()
    {
        var bossSkills = target as BossSkills;

        bossSkills._skillSpeed = EditorGUILayout.FloatField("ãZë¨ìx", bossSkills._skillSpeed);
        bossSkills._waitTime = EditorGUILayout.FloatField("åxçêéûä‘ÅiïbÅj", bossSkills._waitTime);

        EditorGUILayout.Space(20);

        bossSkills._red = EditorGUILayout.ColorField("Red", bossSkills._red);
        bossSkills._gray = EditorGUILayout.ColorField("Gray", bossSkills._gray);

        EditorGUILayout.Space(20);

        EditorGUILayout.BeginHorizontal();
        EditorGUIUtility.labelWidth = 50;
        bossSkills.Title = EditorGUILayout.Popup(bossSkills.Title, Titles);
        bossSkills.Title2 = EditorGUILayout.Popup(bossSkills.Title2, Titles);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(20);

        EditorGUILayout.BeginHorizontal();
        bossSkills._skillPop[0] = EditorGUILayout.Toggle(bossSkills._skillPop[0], GUILayout.Width(30));
        bossSkills._skillPop[1] = EditorGUILayout.Toggle(bossSkills._skillPop[1], GUILayout.Width(30));
        bossSkills._skillPop[2] = EditorGUILayout.Toggle(bossSkills._skillPop[2], GUILayout.Width(125));
        bossSkills._skillRange[0] = EditorGUILayout.Toggle(bossSkills._skillRange[0], GUILayout.Width(30));
        bossSkills._skillRange[1] = EditorGUILayout.Toggle(bossSkills._skillRange[1], GUILayout.Width(30));
        bossSkills._skillRange[2] = EditorGUILayout.Toggle(bossSkills._skillRange[2], GUILayout.Width(30));
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space(10);

        EditorGUILayout.BeginHorizontal();
        bossSkills._skillPop[3] = EditorGUILayout.Toggle(bossSkills._skillPop[3], GUILayout.Width(30));
        bossSkills._skillPop[4] = EditorGUILayout.Toggle(bossSkills._skillPop[4], GUILayout.Width(30));
        bossSkills._skillPop[5] = EditorGUILayout.Toggle(bossSkills._skillPop[5], GUILayout.Width(125));
        bossSkills._skillRange[3] = EditorGUILayout.Toggle(bossSkills._skillRange[3], GUILayout.Width(30));
        bossSkills._skillRange[4] = EditorGUILayout.Toggle(bossSkills._skillRange[4], GUILayout.Width(30));
        bossSkills._skillRange[5] = EditorGUILayout.Toggle(bossSkills._skillRange[5], GUILayout.Width(30));
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space(10);

        EditorGUILayout.BeginHorizontal();
        bossSkills._skillPop[6] = EditorGUILayout.Toggle(bossSkills._skillPop[6], GUILayout.Width(30));
        bossSkills._skillPop[7] = EditorGUILayout.Toggle(bossSkills._skillPop[7], GUILayout.Width(30));
        bossSkills._skillPop[8] = EditorGUILayout.Toggle(bossSkills._skillPop[8], GUILayout.Width(125));
        bossSkills._skillRange[6] = EditorGUILayout.Toggle(bossSkills._skillRange[6], GUILayout.Width(30));
        bossSkills._skillRange[7] = EditorGUILayout.Toggle(bossSkills._skillRange[7], GUILayout.Width(30));
        bossSkills._skillRange[8] = EditorGUILayout.Toggle(bossSkills._skillRange[8], GUILayout.Width(30));
        EditorGUILayout.EndHorizontal();
    }
}