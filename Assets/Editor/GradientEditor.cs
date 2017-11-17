using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GradientEditor : EditorWindow {

    CustomGradient gradient;
    const int borderSize = 10;
    const float keyWidth = 10;
    const float keyHeight = 20;

    private void OnGUI() {
        Event guiEvent = Event.current;

        Rect gradientPrevRect = new Rect(borderSize, borderSize, position.width - borderSize * 2, 25);
        GUI.DrawTexture(gradientPrevRect, gradient.GetTexture((int)gradientPrevRect.width));

        for (int i = 0; i < gradient.NumKeys; i++) {
            CustomGradient.ColorKey key = gradient.GetKey(i);
            Rect keyRect = new Rect(gradientPrevRect.x + gradientPrevRect.width * key.Time - keyWidth/ 2.0f, gradientPrevRect.yMax + borderSize, keyWidth, keyHeight);
            EditorGUI.DrawRect(keyRect, key.Color);
        }

        if (guiEvent.type == EventType.MouseDown && guiEvent.button == 0) {
            Color randColor = new Color(Random.value, Random.value, Random.value);
            float keyTime = Mathf.InverseLerp(gradientPrevRect.x, gradientPrevRect.xMax, guiEvent.mousePosition.x);
            gradient.AddKey(randColor, keyTime);
            Repaint();
        }
    }

    public void SetGradient(CustomGradient gradient) {
        this.gradient = gradient;
    }

	private void OnEnable() {
        titleContent.text = "Gradient Editor";
    }

}
