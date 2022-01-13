using UnityEditor;
using UnityEditor.UI;

namespace Game
{
    [CustomEditor(typeof(VpButton))]
    public class VpButtonEditor : ButtonEditor
    {
        private SerializedProperty _successSoundProp, _failSoundProp, _hapticSuccessProp,
            _hapticFailProp, _doPressAnimProp, _animationPunchProp;

        protected override void OnEnable()
        {
            base.OnEnable();

            _successSoundProp = serializedObject.FindProperty("pressSuccessSound");
            _failSoundProp = serializedObject.FindProperty("pressFailSound");
            _hapticSuccessProp = serializedObject.FindProperty("pressHapticsSuccess");
            _hapticFailProp = serializedObject.FindProperty("pressHapticsFail");
            _doPressAnimProp = serializedObject.FindProperty("doPressAnimation"); 
            _animationPunchProp = serializedObject.FindProperty("animationPunch"); 
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            VpButton targetButton = (VpButton) target;

            EditorGUILayout.PropertyField(_successSoundProp);
            EditorGUILayout.PropertyField(_failSoundProp);
            EditorGUILayout.PropertyField(_hapticSuccessProp);
            EditorGUILayout.PropertyField(_hapticFailProp);
            EditorGUILayout.PropertyField(_doPressAnimProp);
            EditorGUILayout.PropertyField(_animationPunchProp);

            serializedObject.ApplyModifiedProperties(); 
        }
    }
}
