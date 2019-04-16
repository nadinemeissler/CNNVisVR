using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Pointer : MonoBehaviour
{
    public float m_DefaultLength = 5.0f;
    public GameObject m_Dot;
    public VRInputModule m_InputModule;
    public bool m_PointerCollision, m_PointerExit;

    private LineRenderer m_LineRenderer = null;

    private void Awake()
    {
        m_LineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateLine();
    }

    private void UpdateLine()
    {
        
        PointerEventData data = m_InputModule.GetData();

        // Use default lenght if pointer is not hitting anything, else use distance
        float targetLength = data.pointerCurrentRaycast.distance == 0 ? m_DefaultLength : data.pointerCurrentRaycast.distance;

        // Raycast
        RaycastHit hit = CreateRaycast(targetLength);

        // Default endPosition
        Vector3 endPosition = transform.position + (transform.forward * targetLength);

        // Or endPosition based on hit
        if (hit.collider != null)
        {
            endPosition = hit.point;
            print(hit.collider.gameObject.name);

            // change pointer color if it hits a button

            // Not used until end of second else if
            // if collided with gameobject with tag "InteractionObj" set boolean
            // Reset boolean when they are no longer colliding
            if(hit.collider.CompareTag("InteractionObj") && !m_PointerCollision)
            {
                m_PointerCollision = true;
            } else if(!hit.collider.CompareTag("InteractionObj") && m_PointerCollision)
            {
                m_PointerCollision = false;
                m_PointerExit = true;
            }
        } else if(hit.collider == null && m_PointerCollision)
        {
            m_PointerCollision = false;
            m_PointerExit = true;
        }

        // Set position of the dot
        m_Dot.transform.position = endPosition;

        // Set linerenderer
        m_LineRenderer.SetPosition(0, transform.position);
        m_LineRenderer.SetPosition(1, endPosition);

    }

    private RaycastHit CreateRaycast(float length)
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out hit, m_DefaultLength);

        return hit;
    }
}
