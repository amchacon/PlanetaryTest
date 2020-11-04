using UnityEngine;
using Test;

public class TestMain : MonoBehaviour
{
    private World m_world = new World();
    public WorldData data;

    private void Start()
    {
        if (data == null)
            return;

        if (data.Stars.Length > 0)
        {
            for (int i = 0; i < data.Stars.Length; i++)
            {
                var cur = data.Stars[i];
                float renderSize = cur.Mass / 100;

                IEntity starEntity = m_world.AddEntity(new Entity(m_world, data.Stars[i].StartPos, cur.Mass));
                starEntity.SetRenderer(cur.RenderColor, cur.Segments, renderSize, renderSize);

                CreateSatellites(cur, renderSize, starEntity);
            }

            m_world.Initialize();

            if (data.ShowShip)
            {
                m_world.CreateShip();
            }
        }
    }

    private void CreateSatellites(Star cur, float renderSize, IEntity starEntity)
    {
        int segments;
        Color color;
        Vector2 randPos;

        for (int j = 0; j < cur.Satellites.Length; j++)
        {
            Satellite sat = cur.Satellites[j];
            segments = sat.SameShape ? cur.Segments : sat.Segments;
            color = sat.SameColor ? cur.RenderColor : sat.RenderColor;
            randPos = new Vector2(Random.Range(cur.StartPos.x + 0.5f, cur.StartPos.x + 1.5f), Random.Range(cur.StartPos.y + 0.5f, cur.StartPos.y + 1.5f));
            IEntity satEntity = m_world.AddEntity(new Entity(m_world, randPos));
            satEntity.AddBehaviour(new SatelliteRepositionBehaviour(starEntity, Random.Range(2, 8)));            
            satEntity.SetRenderer(color, segments, Random.Range(0.1f, 0.15f), Random.Range(0.1f, 0.15f));
        }
    }

    private void Update()
    {
        // Update the world
        m_world.Update(Time.deltaTime);
        // Render the world using debug rendering.
        m_world.Render();
    }
}