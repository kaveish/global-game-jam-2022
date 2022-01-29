using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class NPCPatrol : MonoBehaviour
{
    public GameObject pathSprite;
    public float speed = 1f;
    Rigidbody2D rb;
    int nextPointIndex = -1;
    Vector2 destination;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        GotoNextPoint();
    }

    void GotoNextPoint()
    {
        GotoPoint(nextPointIndex + 1);
    }

    void GotoLastPoint()
    {
        GotoPoint(nextPointIndex - 1);
    }

    void GotoPoint(int index)
    {
        List<Vector2> points = GetPoints();

        if (points.Count == 0)
            return;

        nextPointIndex = Mod(index, points.Count);
        destination = points[nextPointIndex];
    }

    void FixedUpdate()
    {
        Vector2 toDestination = destination - rb.position;
        Vector2 translation = toDestination.normalized * speed * Time.fixedDeltaTime;

        if (toDestination.magnitude < translation.magnitude)
        {
            rb.MovePosition(destination);
            GotoNextPoint();
        }
        else 
        {
            rb.MovePosition(rb.position + translation);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);

        foreach (Vector2 point in GetPoints())
            Gizmos.DrawCube(point, new Vector3(0.5f, 0.5f, 0.5f));
    }

    List<Vector2> GetPoints()
    {
        SpriteShapeController shape = pathSprite.GetComponent<SpriteShapeController>();
        List<Vector2> points = new List<Vector2>();
        for (int i = 0; i < shape.spline.GetPointCount(); i++)
            points.Add(pathSprite.transform.position + shape.spline.GetPosition(i));
        return points;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        GotoLastPoint();
    }

    int Mod(int a, int n) {
        return ((a%n)+n) % n;
    }
}
