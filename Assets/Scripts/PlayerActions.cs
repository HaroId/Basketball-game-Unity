using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerActions : MonoBehaviour
{
    public GameObject ball;
    TrailRenderer ballTrail;
    SphereCollider ballCollider;
    public Transform cam;
    public float ballDistance = 1f;
    public float ballForceMin = 150f;
    public float ballForceMax = 800f;
    public float ballForce;
    public float totalTime = 2f;
    float currentTime = 0.0f;

    public bool holdingBall = true;
    Rigidbody ballRB;
    //

    bool isPickableBall = false;
    bool pickableBtn = false;
    public CanvasController canvasScript;
    public LayerMask pickableLayer;
    public LayerMask pickbtn;
    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        ballRB = ball.GetComponent<Rigidbody>();
        ballCollider = ball.GetComponent<SphereCollider>();
        ballTrail = ball.GetComponent<TrailRenderer>();
        ballTrail.enabled = false;
        canvasScript.ActivarSlider(false);
        holdingBall = false;
        // ballRB.useGravity = false;
        // canvasScript.OcultarCursor(true);
        // ballCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(holdingBall == true)
        {
            if(Input.GetMouseButtonDown(0))
            {
                currentTime = 0.0f;
                canvasScript.SetValueBar(0);
                canvasScript.ActivarSlider(true);
            }
            if (Input.GetMouseButton(0))
            {
                currentTime += Time.deltaTime;
                ballForce = Mathf.Lerp(ballForceMin, ballForceMax, currentTime/totalTime);
                canvasScript.SetValueBar(currentTime/totalTime);
            }
            if(Input.GetMouseButtonUp(0))
            {
            holdingBall = false;
            ballCollider.enabled = true;
            ballRB.useGravity = true;
            ballRB.AddForce(cam.forward * ballForce);
            canvasScript.OcultarCursor(false);
            canvasScript.ActivarSlider(false);
            ballTrail.enabled = true;
            }
        }
        else
        {
            // if(Physics.Raycast(cam.position, cam.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, pickableLayer))
            if(Physics.Raycast(cam.position, cam.TransformDirection(Vector3.forward), out hit, 2, pickbtn))
            {   
                if(pickableBtn == false)
                {
                    pickableBtn = true;
                    canvasScript.changePickableBallColor(true);
                }
                if(pickableBtn && Input.GetKeyDown(KeyCode.E))
                {
                    ball.transform.position = new Vector3(25.216f, 1.566f,42.681f);
                    ballRB.velocity = Vector3.zero;
                    ballRB.angularVelocity = Vector3.zero;
                }
            }
            else if(pickableBtn == true){
                    pickableBtn = false;
                    canvasScript.changePickableBallColor(false);
            }
            if(Physics.Raycast(cam.position, cam.TransformDirection(Vector3.forward), out hit, 2, pickableLayer))
            {
                if(isPickableBall == false)
                {
                    isPickableBall = true;
                    canvasScript.changePickableBallColor(true);
                }
                if(isPickableBall && Input.GetKeyDown(KeyCode.E))
                {                        
                    holdingBall = true;
                    // ballCollider.enabled = false;
                    ballRB.useGravity = false;
                    ballRB.velocity = Vector3.zero;
                    ballRB.angularVelocity = Vector3.zero;
                    ball.transform.localRotation = Quaternion.identity;
                    GameController.instance.canScore = false;
                    canvasScript.changePickableBallColor(true);
                    canvasScript.OcultarCursor(true);
                    ballTrail.enabled = false;
                }
            }else if(isPickableBall == true){
                isPickableBall = false;
                canvasScript.changePickableBallColor(false);
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    private void LateUpdate() {
        if (holdingBall == true){
            ball.transform.position = cam.position + cam.forward * ballDistance;
        }
    }
}
