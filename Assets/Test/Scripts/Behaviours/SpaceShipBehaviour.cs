using UnityEngine;

namespace Test
{
    public class SpaceShipBehaviour : AutonomousBehaviour
    {
        private float m_speed;
        private IEntity m_nextStar = null;

        public SpaceShipBehaviour(IEntity centreEntity, float speed = 5) : base(centreEntity)
        {
            CentreOfGravity = centreEntity;
            m_speed = speed;
            m_nextStar = null;
        }

        public override void Update(IEntity entity, float deltaTime)
        {
            base.Update(entity, deltaTime);

            //TODO: Add visual feedback about what is the next Star
            //Hold Left Shift to Travel to the Next Star
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (m_nextStar != null)
                {
                    Vector2 diff = m_nextStar.Position - entity.Position;
                    Vector2 thrust = diff.normalized * m_speed * deltaTime;
                    if (diff.magnitude <= 1 && m_nextStar != CentreOfGravity)
                    {
                        entity.SetVelocity(Vector2.zero);
                        thrust = Vector2.zero;
                        CentreOfGravity = m_nextStar;
                    }
                    entity.SetVelocity(entity.Velocity + thrust);
                }
                else
                {
                    m_nextStar = entity.World.RandomStaticEntity;
                }
            }
            else
            {
                m_nextStar = null;
            }
        }
    }
}

