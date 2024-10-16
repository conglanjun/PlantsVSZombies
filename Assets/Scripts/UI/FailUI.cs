using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailUI : MonoBehaviour
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
    // Start is called before the first frame update
    void Start()
    {
        Hide();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
