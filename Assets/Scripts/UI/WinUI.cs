using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinUI : MonoBehaviour
{
    private Animator anim;

    private void Awake() {
        anim = GetComponent<Animator>();
    }

    public void Hide()
    {
        anim.enabled = false;
    }

    public void Show()
    {
        anim.enabled = true;
    }

    private void Start() {
        Hide();
    }
}
