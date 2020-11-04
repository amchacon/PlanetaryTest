using UnityEngine;
using System.Collections.Generic;

namespace Test
{
    public interface IEntity
    {
        Vector2 Position { get; }
        Vector2 Velocity { get; }
        float Mass { get; }
        bool IsActive { get; }
        List<IEntityBehaviour> EntityBehaviours { get; }
        World World { get; }
        void SetVelocity(Vector2 velocity);
        void Update(float deltaTime);
        void Render();
        void AddBehaviour(IEntityBehaviour behaviour);
        void SetRenderer(Color renderColor, int renderSegments, float xRadius, float yRadius);
        void RemoveBehaviour<T>() where T : IEntityBehaviour;
        void SetActive(bool v);
    }
}