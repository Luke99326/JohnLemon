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

        
        float dist = Vector3.Distance(player.transform.position, ghost.transform.position);
        Vector3 lerp = Vector3.Lerp(player.position, ghost.position, dist);

        var main = ps.main;
        main.startColor = new Color(1.5f - Mathf.Abs(lerp.z / 3), .71f, .80f, 1f);
        if (ghost.name == "Ghost")
        {
            print("lerp: " + (1.5 - Mathf.Abs(lerp.z / 3) + ghost.name));
            print("dist: " + dist);

        }


        if (m_IsPlayerInRange)
        {

            print("player:" + player.position);
            print("ghost:" + ghost.position);

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