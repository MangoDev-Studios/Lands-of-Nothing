using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    // Start is called before the first frame update
    public UnityEvent<Vector3> PointerClick;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DetectMouseClick();
    }

    private void DetectMouseClick()
    {
     if (Input.GetMouseButtonDown(0))
     {
        Vector3 mousePos = Input.mousePosition;
        PointerClick?.Invoke(mousePos);
     }   
    }
}
