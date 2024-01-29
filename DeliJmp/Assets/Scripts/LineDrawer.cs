using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LineDrawer : MonoBehaviour
{
    public GameObject linePrefab;
    public Camera cam;
    public LayerMask cantDrawOver;
    
    int cantDrawLayerIndex;
    [Space(20f)]
    public Gradient lineColor;
    [Space(30f)]
    public float linePointsMinDistance;
    public float lineWidth;
    public int pointsMax;

    Line currentLine;
    private Vector2 currentScreenPosition;

    private bool disabled;
    private void Start()
    {
        disabled = false;
        cantDrawLayerIndex = LayerMask.NameToLayer("CantDrawOver");
    }
    
    void Update()
    {
        if (currentLine!=null)
        {
            Draw();
        }
    }
    public void test(InputAction.CallbackContext context)
    {
        Deli.instance.Pew();
    }
    public void setCurrentScreenPosition(InputAction.CallbackContext context)
    {
        currentScreenPosition = context.ReadValue<Vector2>();
    }
    public void BeginDraw(InputAction.CallbackContext context)
    {
        Debug.Log("Begin");
        if (disabled)
        {
            return;
        }
        currentLine = Instantiate(linePrefab, this.transform).GetComponent<Line>();
        currentLine.UsePhysics(false);
        currentLine.setPointsMinDistance(linePointsMinDistance);
        currentLine.setLineWindth(lineWidth);
        currentLine.setMaxPoints(pointsMax);
        currentLine.setLineColor(lineColor);
    }
    void Draw()
    {
        Vector2 mousePosition = cam.ScreenToWorldPoint(currentScreenPosition);
        RaycastHit2D raycastHit = Physics2D.CircleCast(mousePosition, lineWidth / 3f, Vector2.zero, 1f, cantDrawLayerIndex);
        bool res = currentLine.AddPoint(mousePosition);

        
        if (!res || raycastHit)
        {
            EndDraw();
        }
    }
    void EndDraw()
    {
        if(currentLine != null)
        {
            if(currentLine.pointsCount < 2)
            {
                Destroy(currentLine.gameObject);
            }
            else
            {
                currentLine.gameObject.layer = cantDrawLayerIndex;
                // currentLine.UsePhysics(true);
                currentLine = null;
            }
        }
    }
    public void DisableDrawer()
    {
        disabled = true;
    }
}
