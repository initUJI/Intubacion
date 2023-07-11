// Copyright (c) 2018 Emiliana (twitter.com/Emiliana_vt)


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDPMouseMovement : MonoBehaviour {
    public TextMesh tm;
    public TextMesh fpsTM;
    private DokoDemoPainterPen pen;
    private float fps = 0.0f;

    void Start() {
        pen = GetComponent<DokoDemoPainterPen>();
    }

    void FixedUpdate() {
        Camera cam = Camera.main;
        Vector3 pos = transform.position;
        Vector3 mouse = cam.ScreenToWorldPoint(Input.mousePosition);
        pos.x = mouse.x;
        pos.y = mouse.y;
        transform.position = pos;
        pen.penDown = Input.GetMouseButton(0) || Input.GetMouseButton(1);
        pen.eraser = Input.GetMouseButton(1);
        if (Input.GetKey("1")) {
            pen.color = new Color(0.05209844f, 0.6132076f, 0.03181738f, 1.0f);
            pen.radius = 20f;
            pen.opacity = 0.5f;
            pen.smoothTip = true;
            pen.smoothTipExponent = 2.0f;
            pen.keepTarget = false;
            pen.paintInvisible = false;
        } else if (Input.GetKey("2")) {
            pen.color = Color.red;
            pen.radius = 10f;
            pen.opacity = 1.0f;
            pen.smoothTip = false;
            pen.keepTarget = false;
            pen.paintInvisible = false;
        } else if (Input.GetKey("3")) {
            pen.color = Color.blue;
            pen.radius = 50f;
            pen.opacity = 1f;
            pen.smoothTip = true;
            pen.smoothTipExponent = 0.05f;
            pen.keepTarget = false;
            pen.paintInvisible = false;
        } else if (Input.GetKey("4")) {
            pen.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
            pen.radius = 20f;
            pen.opacity = 1.0f;
            pen.smoothTip = true;
            pen.smoothTipExponent = 3.0f;
            pen.keepTarget = false;
            pen.paintInvisible = false;
        } else if (Input.GetKey("5")) {
            pen.color = new Color(0.05209844f, 0.6132076f, 0.03181738f, 1.0f);
            pen.radius = 20f;
            pen.opacity = 0.5f;
            pen.smoothTip = true;
            pen.smoothTipExponent = 2.0f;
            pen.keepTarget = true;
            pen.paintInvisible = false;
        } else if (Input.GetKey("6")) {
            pen.color = Color.red;
            pen.radius = 10f;
            pen.opacity = 1.0f;
            pen.smoothTip = false;
            pen.keepTarget = true;
            pen.paintInvisible = false;
        } else if (Input.GetKey("7")) {
            pen.color = Color.blue;
            pen.radius = 50f;
            pen.opacity = 1f;
            pen.smoothTip = true;
            pen.smoothTipExponent = 0.05f;
            pen.keepTarget = true;
            pen.paintInvisible = false;
        } else if (Input.GetKey("8")) {
            pen.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
            pen.radius = 20f;
            pen.opacity = 1.0f;
            pen.smoothTip = true;
            pen.smoothTipExponent = 3.0f;
            pen.keepTarget = true;
            pen.paintInvisible = false;
        } else if (Input.GetKey("9")) {
            pen.color = Color.black;
            pen.radius = 20f;
            pen.opacity = 1.0f;
            pen.smoothTip = true;
            pen.smoothTipExponent = 8.0f;
            pen.keepTarget = false;
            pen.paintInvisible = true;
        } else if (Input.GetKey("0")) {
            pen.color = Color.black;
            pen.radius = 20f;
            pen.opacity = 1.0f;
            pen.smoothTip = true;
            pen.smoothTipExponent = 8.0f;
            pen.keepTarget = true;
            pen.paintInvisible = true;
        }
        if (tm != null) {
            tm.text = "Eraser: " + pen.eraser + "\nColor: " + pen.color + "\nRadius: " + pen.radius + "\nOpacity: " + pen.opacity + "\nSmooth Tip: " + pen.smoothTip + "\nSmooth Tip Exponent: " + pen.smoothTipExponent + "\nKeep Target: " + pen.keepTarget + "\nPaint Invisible: " + pen.paintInvisible;
        }
    }

    void Update() {
        fps += ((Time.deltaTime/Time.timeScale) - fps) * 0.03f;
        if (fpsTM != null) {
            fpsTM.text = "FPS: " + Mathf.Floor(1.0f/fps);
        }
    }
}
