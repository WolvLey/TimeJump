using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class GameController : MonoBehaviour
{
    public Player player;
    public Slider healthBar;

    private Camera _mainCam;
    private DIRECTIONS _playerDirection;

    private void Awake()
    {
        _mainCam = Camera.main;
    }

    // Use this for initialization
    void Start()
    {
        player = Instantiate(player, new Vector2(0, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        AssignPlayersDirection();

        MoveCamera(_playerDirection);
    }

    /// <summary>
    /// Calculate which direction the player is and write it into the playerDirection enum.
    /// </summary>
    /// <param name="screenPoint"></param>
    private void AssignPlayersDirection()
    {
        Vector3 screenPoint = _mainCam.WorldToViewportPoint(player.transform.position);

        if (screenPoint.x <= 0f)
        {
            _playerDirection = DIRECTIONS.WEST;
            return;
        }
        else if (screenPoint.x >= 1f)
        {
            _playerDirection = DIRECTIONS.EAST;
            return;
        }

        if (screenPoint.y <= 0f)
        {
            _playerDirection = DIRECTIONS.SOUTH;
            return;
        }
        else if (screenPoint.y >= 1f)
        {
            _playerDirection = DIRECTIONS.NORTH;
            return;
        }
        _playerDirection = DIRECTIONS.CENTER;
        return;
    }

    private void MoveCamera(DIRECTIONS playerDirection)
    {
        Vector3 camPosition;
        float height = 2f * _mainCam.orthographicSize;
        float width = height * _mainCam.aspect;

        switch (playerDirection)
        {
            case DIRECTIONS.NORTH:
                camPosition = new Vector3(0, height, 0);
                break;
            case DIRECTIONS.EAST:
                camPosition = new Vector3(width, 0, 0);
                break;
            case DIRECTIONS.SOUTH:
                camPosition = new Vector3(0, -height, 0);
                break;
            case DIRECTIONS.WEST:
                camPosition = new Vector3(-width, 0, 0);
                break;

            default:
                return;
        }
        _mainCam.transform.Translate(camPosition);
    }

    void UpdatePlayerUI(float healthRatio)
    {
        healthBar.value = healthRatio;
    }
}
