using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsForTheCamera : MonoBehaviour
{
    [HideInInspector]public Transform player;

    private void Start()
    {
        GameManager.Instance.points = this;
    }

    private void FixedUpdate()
    {
        //player = GameManager.instance.assignedPlayer;
        transform.position = player.position;
    }
}
