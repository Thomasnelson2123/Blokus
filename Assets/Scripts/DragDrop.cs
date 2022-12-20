using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour
{
    [SerializeField] GridManager grid;
    

    [SerializeField] Transform[] transforms;
    private float gridW, gridH;

    private float startPosX, startPosY;
    private bool isBeingHeld = false;
    private bool moveInteger = false;


    private void Start()
    {
        (gridW, gridH) = grid.getDims();
    }  

    private void Update()
    {
        
        if(isBeingHeld)
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            if (inGrid(transforms))
            {
                moveInteger = true;
                grid.placeBlock.SetActive(true);

            }
            else
            {
                moveInteger = false;

            }

            if (Input.GetMouseButtonDown(1))
            {
                Rotate(mousePos);

            }
            else if (moveInteger)
            {
                int x = (int)Mathf.Round(mousePos.x - startPosX);
                int y = (int)Mathf.Round(mousePos.y - startPosY);
                this.gameObject.transform.position = new Vector3(x, y, 0);
            }
            else
            {
                this.gameObject.transform.position = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, 0);
            }
            

        }
    }

    private bool inGrid(Transform[] transforms)
    {
        bool check = true;
        foreach (Transform t in transforms)
        {
            check &= (t.position.x >= -0.5f && t.position.x <= gridW - 0.5f && t.position.y >= -0.5f && t.position.y <= gridH - 0.5f);
        }
        return check;
    }

    private void OnMouseDown()
    {

        if(Input.GetMouseButtonDown(0))
        {

            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            startPosX = mousePos.x - this.transform.position.x;
            startPosY = mousePos.y - this.transform.position.y;

            isBeingHeld = true;
        }
        
        
    }

    private void OnMouseUp()
    {
        isBeingHeld = false;
        if (!inGrid(transforms))
        {
            grid.placeBlock.SetActive(false);
        }
    }

    private void Rotate(Vector3 mousePos)
    {
        mousePos += new Vector3(0, 0, 10);
        Vector3 radius = mousePos - this.gameObject.transform.position;
        float angle = Mathf.Atan2(-mousePos.y + this.gameObject.transform.position.y, -mousePos.x + this.gameObject.transform.position.x);
        this.gameObject.transform.Rotate(0, 0, 90);
        this.gameObject.transform.position = new Vector3(radius.magnitude * Mathf.Cos(angle + (Mathf.PI / 2)) + mousePos.x, radius.magnitude * Mathf.Sin(angle + (Mathf.PI / 2)) + mousePos.y, 0);

        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        startPosX = mousePos.x - this.transform.position.x;
        startPosY = mousePos.y - this.transform.position.y;



    }

    

}
