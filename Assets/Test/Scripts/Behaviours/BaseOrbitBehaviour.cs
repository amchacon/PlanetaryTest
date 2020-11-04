using UnityEngine;

namespace Test
{
    public abstract class BaseOrbitBehaviour : IEntityBehaviour
    {
        public const float G = 6.78f;

        internal IEntity CentreOfGravity = null;
        internal Vector2 GravitalDirection;
        internal Vector2 TangentialDirection;
        internal Vector2 OrbitalVelocity;
        internal float GravitalForce;
        internal float SquaredRadius;

        private Vector2 orbitToCenter;

        public BaseOrbitBehaviour(IEntity centreEntity)
        {
            CentreOfGravity = centreEntity;
        }

        public abstract void Update(IEntity entity, float deltaTime);

        internal void UpdateForces(IEntity entity)
        {
            orbitToCenter = CentreOfGravity.Position - entity.Position;
            GravitalDirection = orbitToCenter.normalized;
            TangentialDirection = Vector2.Perpendicular(GravitalDirection).normalized;
            SquaredRadius = orbitToCenter.sqrMagnitude;
            GravitalForce = (G * CentreOfGravity.Mass) / SquaredRadius;
        }
    }
}


