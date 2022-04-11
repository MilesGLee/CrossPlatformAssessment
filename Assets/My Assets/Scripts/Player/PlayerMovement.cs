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

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            StartGrapple();
        }
        if (Input.GetMouseButtonUp(0)) 
        {
            StopGrapple();
        }
    }

    private void LateUpdate()
    {
        DrawRope();
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

            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;

            lr.positionCount = 2;
            currentGrapplePosition = transform.position;

            Rigidbody rb = GetComponent<Rigidbody>();
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
}
