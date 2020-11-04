using UnityEngine;

namespace Test
{
    public class SatelliteRepositionBehaviour : AutonomousBehaviour
    {
        private float m_speed;

        public SatelliteRepositionBehaviour(IEntity centreEntity, float speed = 5) : base(centreEntity)
        {
            CentreOfGravity = centreEntity;
            m_speed = speed;
        }

        public override void Update(IEntity entity, float deltaTime)
        {
            base.Update(entity, deltaTime);
            if (Input.GetKey(KeyCode.R) && entity.IsActive)
            {
                Vector2 newVelocity = entity.Velocity + (TangentialDirection * m_speed);
                entity.SetVelocity(newVelocity);
            }

            if (Input.GetKeyUp(KeyCode.R))
            {
                entity.SetActive(false);
            }
        }
    }
}
