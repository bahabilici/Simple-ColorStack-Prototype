using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
   public int value;
    [SerializeField] Color pickUpColor;

    [SerializeField] Rigidbody pickUpRB;
    [SerializeField] Collider pickUpCollider;

    private void OnEnable()
    {
        PlayerController.Kick += MyKick;
    }

    private void OnDisable()
    {
        PlayerController.Kick -= MyKick;
    }

    private void MyKick(float forceSent)
    {
        transform.parent = null;
        pickUpCollider.enabled = true;
        pickUpRB.isKinematic = false;
        pickUpRB.AddForce(new Vector3(0, forceSent, forceSent));
    }

    // Start is called before the first frame update
    void Start()
    {
        Renderer rend = GetComponent<Renderer>();
        rend.material.SetColor("_Color", pickUpColor);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Color GetColor()
    {
        return pickUpColor;
    }
}
