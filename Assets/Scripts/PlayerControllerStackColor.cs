using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerStackColor : MonoBehaviour
{
    [SerializeField] bool _isPlaying;
    [SerializeField] Renderer[] _myRends;
    [SerializeField] float _forwardSpeed;
    [SerializeField] Color _myColor;
    [SerializeField] float _sideLerpSpeed;
    Transform parentPickup;
    [SerializeField] Transform _stackPosition;
    private Rigidbody _myRB;

    // Start is called before the first frame update
    void Start()
    {
        _myRB = GetComponent<Rigidbody>();

        SetColor(_myColor);
    }

    // Update is called once per frame
    void Update()
    {
        if (_isPlaying)
        {
            MoveForward();
        }
        
        if (Input.GetMouseButton(0))
        {
            if (!_isPlaying) _isPlaying = true;
            MoveSideWays();
        }
    }

    void SetColor(Color myColor)
    {
        _myColor = myColor;
        for(int i = 0; i < _myRends.Length; i++)
        {
            _myRends[i].material.SetColor("_Color", _myColor);
        }
    }

    void MoveForward()
    {
        _myRB.velocity = Vector3.forward * _forwardSpeed;
    }

    void MoveSideWays()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100) && transform.position.z - transform.localScale.z / 2 < hit.point.z && hit.point.z < transform.position.z + transform.localScale.z/2)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(hit.point.x, transform.position.y, hit.point.z), _sideLerpSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pickup")
        {
            Transform otherTranform = other.transform.parent;
            Rigidbody otherRB = otherTranform.GetComponent<Rigidbody>();
            otherRB.isKinematic = true;
            other.enabled = false;
            
            if (parentPickup == null)
            {
                parentPickup = otherTranform;
                parentPickup.position = _stackPosition.position;
                parentPickup.parent = _stackPosition;
            } else
            {
                parentPickup.position += Vector3.up * (otherTranform.localScale.y);
                otherTranform.position = _stackPosition.position;
                otherTranform.parent = _stackPosition;
            }

        }
    }
}
