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
        if (Physics.Raycast(ray, out hit, 100))
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(hit.point.x, transform.position.y, hit.point.z), _sideLerpSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pickup")
        {
            GameControllerStackColor.Instance.UpdateScore(other.GetComponent<PickupStackColor>().GetValue());

            Transform otherTranform = other.transform;
            Rigidbody otherRB = other.GetComponent<Rigidbody>();
            otherRB.isKinematic = true;  // set no physics
            other.enabled = false;  //set no collider (khong the va cham voi object khac)
            
            if (parentPickup == null)
            {
                parentPickup = otherTranform;  // set parent has tranform of the otherTranform
                parentPickup.position = _stackPosition.position;  // set cho gia tri _stackPosition dau tien vi gia tri _stackPositon lien tuc thay doi
                parentPickup.parent = _stackPosition;  // set relative to parent 
            } else
            {
                parentPickup.position += Vector3.up * (otherTranform.localScale.y); // dich chuyen vi tri cua parentPickup
                otherTranform.position = _stackPosition.position;  // set otherTranform has  position of _stackPosition
                otherTranform.parent = parentPickup;
            }
        }
    }
}
