using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Color myColor;
    [SerializeField] Renderer[] myRends;

    [SerializeField] bool isPlaying;
    [SerializeField] float forwardSpeed;
    Rigidbody myRb;

    [SerializeField] float sideLerpSpeed;

    Transform parentPickup;
    [SerializeField] Transform stackPosition;



    bool atEnd;
    [SerializeField] float forwardForce;
    [SerializeField] float forceAdder;
    [SerializeField] float forceReducer;

    public static Action<float> Kick;
    

    void Start()
    {
        myRb = GetComponent<Rigidbody>();

        SetColor(myColor);


    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            MoveForward();

        }

        if (atEnd)
        {
            forwardForce -= forceReducer * Time.deltaTime;
            if (forwardForce <0)
            {
                forwardForce = 0;
            }



        }

        if (Input.GetMouseButton(0))
        {
            if (atEnd)
            {

                forwardForce += forceAdder;

            }

        }





        if (Input.GetMouseButton(0))
        {
            if (atEnd)
            {
                return;
            }

            if (isPlaying == false)
            {
                isPlaying = true;

            }

            MoveSideways();
        }


    }


    void SetColor(Color colorIn)
    {

        myColor = colorIn;

        for (int i = 0; i < myRends.Length; i++)
        {

            myRends[i].material.SetColor("_Color", myColor);

        }
          
    }


    void MoveForward()
    {
        myRb.velocity = Vector3.forward * forwardSpeed;
    }

    void MoveSideways()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {

            transform.position = Vector3.Lerp(transform.position, new Vector3(hit.point.x, transform.position.y, transform.position.z), sideLerpSpeed*Time.deltaTime);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "ColorWall")
        {
            SetColor(other.GetComponent<ColorWall>().GetColor());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FinishLineStart")
        {
            atEnd = true;

            


        }
        if (other.tag == "FinishLineEnd")
        {
            myRb.velocity = Vector3.zero;
            isPlaying = false;
            LaunchStack();

        }
        if (atEnd)
        {
            return;
        }



        if (other.tag == "Pickup")
        {

            Transform otherTransform = other.transform;

            // GameController.instance.UpdateScore(otherTransform.GetComponent<Pickups>().value);

            if (myColor == otherTransform.GetComponent<Pickups>().GetColor())
            {
                GameController.instance.UpdateScore(otherTransform.GetComponent<Pickups>().value);
            }
            else
            {
                GameController.instance.UpdateScore(otherTransform.GetComponent<Pickups>().value * -1);
                Destroy(other.gameObject);
                if (parentPickup != null)
                {

                    if (parentPickup.childCount > 1)
                    {
                        parentPickup.position -= Vector3.up * parentPickup.GetChild(parentPickup.childCount - 1).localScale.y;
                        Destroy(parentPickup.GetChild(parentPickup.childCount - 1).gameObject);


                    }
                    else
                    {
                        Destroy(parentPickup.gameObject);
                    }

                }
                return;
            }



            Rigidbody otherRB = otherTransform.GetComponent<Rigidbody>();
            otherRB.isKinematic = true;
            other.enabled = false;
            if (parentPickup == null)
            {
                parentPickup = otherTransform;
                parentPickup.position = stackPosition.position;
                parentPickup.parent = stackPosition;



            }
            else
            {
                parentPickup.position += Vector3.up * (otherTransform.localScale.y);
                otherTransform.position = stackPosition.position;
                otherTransform.parent = parentPickup;

            }


        }
    }

    void LaunchStack()
    {
       // Camera.main.GetComponent<CameraFollow>().SetTarget(parentPickup);
        Kick(forwardForce);
    }




}


