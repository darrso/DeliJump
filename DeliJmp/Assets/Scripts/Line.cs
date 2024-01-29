using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Line : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCollider;
    public Rigidbody2D rigidBody;

    [HideInInspector] public List<Vector2> points = new List<Vector2>();
    [HideInInspector] public int pointsCount;

    float pointsMinDistance = 0.1f;
    int maxPoints;
    float circleColliderRadius;
    public bool AddPoint(Vector2 newPoint)
    {
        if (pointsCount >= 1 && Vector2.Distance(newPoint, GetLastPoint()) < pointsMinDistance)
        {
            return true;
        }
        if(pointsCount + 1 > maxPoints)
        {
            return false;
        }
        points.Add(newPoint);
        pointsCount++;

        CircleCollider2D circleCollider2D = this.gameObject.AddComponent<CircleCollider2D>();
        circleCollider2D.offset = newPoint;
        circleCollider2D.radius = circleColliderRadius;

        lineRenderer.positionCount= pointsCount;
        lineRenderer.SetPosition(pointsCount - 1, newPoint);
        
        if(pointsCount > 1)
        {
            edgeCollider.points = points.ToArray();
        }
        return true;
    }
    public Vector2 GetLastPoint()
    {
        return (Vector2)lineRenderer.GetPosition(pointsCount-1);
    }
    public void UsePhysics(bool use)
    {
        rigidBody.isKinematic= !use;
    }
    public void setPointsMinDistance(float distance)
    {
        pointsMinDistance= distance;
    }
    public void setLineWindth(float lineWidth)
    {
        lineRenderer.startWidth= lineWidth;
        lineRenderer.endWidth= lineWidth;
        edgeCollider.edgeRadius = lineWidth / 2f;
        circleColliderRadius = lineWidth / 2f;
    }
    public void setMaxPoints(int count)
    {
        maxPoints= count;
    }
    public void setLineColor(Gradient gradientColor)
    {
        lineRenderer.colorGradient = gradientColor;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.name == "DeadZone")
        {
            Destroy(this.gameObject);
                }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.y < 0 && collision.collider.name == "Deli")
        {
            Rigidbody2D deliRigidbody = Deli.instance.rigidbody; 
            Vector2 normal = collision.contacts[0].normal;
            
            normal = new Vector2(normal.x * -1, normal.y * -1);
            Debug.DrawRay(transform.position, normal, Color.red);
            float forceY = 7f;     
            float forceX = 5f;     
            Vector2 force = new Vector2(normal.x * forceX,  normal.y * forceY);
            deliRigidbody.AddForce(force, ForceMode2D.Impulse);

            UsePhysics(true);
        }
    }
}
