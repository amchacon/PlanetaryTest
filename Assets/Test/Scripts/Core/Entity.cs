using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Test
{
    public class Entity : IEntity
    {
        public Vector2 Position { get; private set; }
        public Vector2 Velocity { get; private set; }
        public float Mass { get; private set; }
        public bool IsActive { get; private set; }
        public List<IEntityBehaviour> EntityBehaviours { get; private set; }
        public World World { get; private set; }

        private Renderer m_renderer;
        private int m_behavioursCount = 0;

        public Entity(World world, Vector2 position, float mass = 25)
        {
            World = world;
            Position = position;
            EntityBehaviours = new List<IEntityBehaviour>();
            Mass = mass;
        }

        public void SetVelocity(Vector2 velocity)
        {
            Velocity = velocity;
        }

        public void SetMass(float newMass) => Mass = newMass;

        public void SetActive(bool value) => IsActive = value;

        public void Render()
        {
            Vector2 pos = new Vector2(Position.x, Position.y);
            m_renderer.DrawShape(pos);
        }

        public void Update(float deltaTime)
        {
            UpdateBehaviours(deltaTime);
            Position += Velocity * deltaTime;
        }

        public void AddBehaviour(IEntityBehaviour behaviour)
        {
            EntityBehaviours.Add(behaviour);
            m_behavioursCount++;
        }

        private void UpdateBehaviours(float deltaTime)
        {
            for (int i = 0; i < m_behavioursCount; i++)
            {
                EntityBehaviours[i].Update(this, deltaTime);
            }
        }

        public void SetRenderer(Color renderColor, int renderSegments, float xRadius, float yRadius)
        {
            m_renderer = new Renderer(renderSegments, renderColor, xRadius, yRadius);
        }

        public void RemoveBehaviour<T>() where T : IEntityBehaviour
        {
            IEntityBehaviour behaviour = EntityBehaviours.FirstOrDefault(b => b.GetType() == typeof(T));
            if (behaviour != null)
            {
                EntityBehaviours.Remove(behaviour);
            }
        }

    }
}