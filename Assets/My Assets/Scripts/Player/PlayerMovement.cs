using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LineRenderer lr;
    private Vector3 grapplePoint;
    [SerializeField] private LayerMask whatIsGrapple;
    [SerializeField] private Camera cam;
    private float maxDistance = 100f;
    private SpringJoint joint;
    [SerializeField] private GameObject deathParticle;
    private Rigidbody rb;
    [SerializeField] private TrailRenderer tr;
    private bool trailCheck;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 newPos = transform.position;
        newPos.z = 0;
        transform.position = newPos;

        if (rb.velocity.magnitude >= 11)
        {
            if (trailCheck)
            {
                tr.Clear();
                trailCheck = false;
            }
            tr.startWidth = Mathf.Lerp(tr.startWidth, 1, Time.deltaTime * 5.0f);
        }
        else
        {
            if (!trailCheck)
            {
                trailCheck = true;
            }
            tr.startWidth = Mathf.Lerp(tr.startWidth, 0, Time.deltaTime * 5.0f);
        }

        if (Input.GetMouseButtonDown(0)) 
        {
            StartGrapple();
            InvokeRepeating("AddForceToPlayer", 1.0f, 0.5f);
        }
        if (Input.GetMouseButtonUp(0)) 
        {
            StopGrapple();
            CancelInvoke("AddForceToPlayer");
        }
        if (transform.position.y < -100)
            PlayerDeath();
    }

    private void LateUpdate()
    {
        DrawRope();
    }

    void AddForceToPlayer() 
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(rb.velocity * 10.0f);
    }

    void StartGrapple() 
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, maxDistance, whatIsGrapple)) 
        {
            Vector3 newPoint = hit.point;
            newPoint.z = 0;
            grapplePoint = newPoint;
            joint = gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(transform.position, grapplePoint);

            joint.maxDistance = distanceFromPoint * 0.4f;
            joint.minDistance = distanceFromPoint * 0.25f;

            joint.spring = 10.0f;
            joint.damper = 7f;
            joint.massScale = 10.0f;

            lr.positionCount = 2;
            currentGrapplePosition = transform.position;

            Vector3 force = (newPoint - transform.position);
            rb.AddForce(force * 50f);
        }
    }

    void StopGrapple()
    {
        lr.positionCount = 0;
        Destroy(joint);
    }

    private Vector3 currentGrapplePosition;

    void DrawRope() 
    {
        if (!joint) return;

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 8f);

        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, currentGrapplePosition);
    }

    public void PlayerDeath() 
    {
        Instantiate(deathParticle, transform.position, Quaternion.Euler(90, 0, 0));
        Destroy(gameObject);
    }
}
