using UnityEngine;

namespace Test
{
	public class AutonomousBehaviour : BaseOrbitBehaviour
    {
        private float m_distanceSquared = 0;

        public AutonomousBehaviour(IEntity centreEntity) : base(centreEntity)
        {
            CentreOfGravity = centreEntity;
        }

        public override void Update(IEntity entity, float deltaTime)
        {
            UpdateForces(entity);
            m_distanceSquared = m_distanceSquared <= 0 ? SquaredRadius : m_distanceSquared;
            GravitalForce = (G * CentreOfGravity.Mass) / m_distanceSquared;
            OrbitalVelocity = (GravitalDirection * GravitalForce + TangentialDirection * GravitalForce) * deltaTime;
            Vector2 newVelocity = entity.Velocity + (OrbitalVelocity * deltaTime);
            newVelocity = Vector2.ClampMagnitude(newVelocity, 2);
            entity.SetVelocity(newVelocity);
        }
    }
}