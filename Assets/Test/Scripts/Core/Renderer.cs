using UnityEngine;

public class Renderer
{
    private int m_segments;
    private Color m_color = Color.blue;
    private float m_xRadius;
    private float m_yRadius;

    public Renderer()
    {
        m_segments = 4;
        m_color = Color.green;
        m_xRadius = 0.1f;
        m_yRadius = 0.1f;
}

    public Renderer(int segments, Color color, float xRadius, float yRadius)
    {
        m_segments = segments;
        m_color = color;
        m_xRadius = xRadius;
        m_yRadius = yRadius;
    }

    public void DrawShape(Vector2 pos)
    {
        float angle = 0f;
        Vector2 lastPoint = Vector2.zero;
        Vector2 thisPoint = Vector2.zero;

        for (int i = 0; i < m_segments + 1; i++)
        {
            thisPoint.x = Mathf.Sin(Mathf.Deg2Rad * angle) * m_xRadius;
            thisPoint.y = Mathf.Cos(Mathf.Deg2Rad * angle) * m_yRadius;

            if (i > 0)
            {
                Debug.DrawLine(lastPoint + pos, thisPoint + pos, m_color);
            }

            lastPoint = thisPoint;
            angle += 360f / m_segments;
        }
    }
}