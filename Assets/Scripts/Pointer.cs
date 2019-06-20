using UnityEngine;
using UnityEngine.EventSystems;

public class Pointer : MonoBehaviour
{
    /***************************************************************************************
    *    Code copied from:
    *    Title: [01][Unity] SteamVR Canvas Pointer - Pointer
    *    Author: Youtube Channel "VR with Andrew"
    *    Date: 27.02.2019
    *    Link: https://www.youtube.com/watch?v=3mRI1hu9Y3w
    *
    ***************************************************************************************/

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
