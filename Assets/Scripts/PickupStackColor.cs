using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupStackColor : MonoBehaviour
{
    [SerializeField] int _value;
    [SerializeField] Color _pickUpColor;

    // Start is called before the first frame update
    void Start()
    {
        Renderer rend = GetComponent<Renderer>();
        rend.material.SetColor("_Color", _pickUpColor);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int GetValue()
    {
        return _value;
    }

    public Color GetColor()
    {
        return _pickUpColor;
    }
}
