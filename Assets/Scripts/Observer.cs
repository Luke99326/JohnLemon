using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public Transform player;
    public Transform ghost;
    public GameEnding gameEnding;
    public ParticleSystem ps;

    bool m_IsPlayerInRange;
    string gLabel = "";
    string g1Label = "";
    string g2Label = "";
    string g3Label = "";


    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 1000, 20), gLabel);
        GUI.Label(new Rect(10, 30, 1000, 20), g1Label);
        GUI.Label(new Rect(10, 50, 1000, 20), g2Label);
        GUI.Label(new Rect(10, 70, 1000, 20), g3Label);

    }

    private void Start()
    {
        var main = ps.main;
        main.startColor = new Color(.54f, .71f, .80f, 1f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform == player)
        {
            m_IsPlayerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform == player)
        {
            m_IsPlayerInRange = false;
        }
    }

    void Update()
    {

        var targetDir = Vector3.Normalize(ghost.position - player.position);
        var dot = Vector3.Dot(player.forward, targetDir);

        if (dot < .707)
        {
            switch (ghost.name)
            {
                case "Ghost":
                    gLabel = "Ghost is behind you!";
                    break;

                case "Ghost (1)":
                    g1Label = "Ghost(1) is behind you!";
                    break;

                case "Ghost (2)":
                    g2Label = "Ghost(2) is behind you!";
                    break;

                case "Ghost (3)":
                    g3Label = "Ghost(3) is behind you!";
                    break;
            }

        } else if (dot > .707)
        {
            switch (ghost.name)
            {
                case "Ghost":
                    gLabel = "Ghost is in front of you!";
                    break;

                case "Ghost (1)":
                    g1Label = "Ghost(1) is in front of you!";
                    break;

                case "Ghost (2)":
                    g2Label = "Ghost(2) is in front of you!";
                    break;

                case "Ghost (3)":
                    g3Label = "Ghost(3) is in front of you!";
                    break;
            }

        }





        float dist = Vector3.Distance(player.transform.position, ghost.transform.position);
        Vector3 lerp = Vector3.Lerp(player.position, ghost.position, dist);

        var main = ps.main;
        main.startColor = new Color(1.5f - Mathf.Abs(lerp.z / 3), .71f, .80f, 1f);

        if (m_IsPlayerInRange)
        {
            Vector3 direction = player.position - transform.position + Vector3.up;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit raycastHit;

            if (Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.collider.transform == player)
                {
                    gameEnding.CaughtPlayer();
                }
            }
        }
    }
}