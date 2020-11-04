using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Test
{
    public class World
    {
		private List<IEntity> m_entities;
		private List<IEntity> m_staticEntities;
		private float m_timeStepAccumulator;
        private float m_fixedTimeStep;
		private float m_fieldOfView;
		private int m_count = 0;
		private float m_timeScale;
		private int m_entitiesCount;
		private bool m_updated;
		private Camera m_camera;
		private IEntity m_activeSat;
		private IEntity m_ship;

		public IEntity RandomEntity => m_entities[Random.Range(0, m_entitiesCount)];

		public IEntity RandomStaticEntity
		{
			get
			{
				int staticsCount = RefreshStaticEntitiesList;
				return m_staticEntities[Random.Range(0, staticsCount)];
			}
		}

		public IEntity RandomSatellite
		{
			get
			{
				List<IEntity> sats = m_entities.Where(e => e.EntityBehaviours.Count > 0).ToList();
				return sats[Random.Range(0, sats.Count)];
			}
		}

		public int RefreshStaticEntitiesList
		{
			get
			{
				if (!m_updated)
				{
					m_updated = true;
					m_staticEntities = m_entities.Where(e => e.Velocity == Vector2.zero && e.EntityBehaviours.Count == 0).ToList();
				}
				return m_staticEntities.Count;
			}
		}

		public World()
        {
			m_entities = new List<IEntity>();
			m_staticEntities = new List<IEntity>();
			m_entitiesCount = 0;
			m_updated = false;
			m_timeStepAccumulator = 0.0f;
			m_fixedTimeStep = 0.016f;
			m_count = 0;
			m_timeScale = 1;
		}

		public void Initialize()
        {
			m_camera = Camera.main;
			m_fieldOfView = m_camera?.fieldOfView ?? 90;
		}

        internal void CreateShip()
        {
            IEntity ship = AddEntity(new Entity(this, RandomStaticEntity.Position + Vector2.one));
            ship.SetRenderer(new Color(1,0,1,1), 6, 0.15f, 0.25f);
            ship.AddBehaviour(new SpaceShipBehaviour(RandomStaticEntity));
		}

        internal IEntity AddEntity(IEntity entity)
		{
			m_entities.Add(entity);
			m_entitiesCount++;
			m_updated = false;
			return entity;
		}

		internal void RemoveEntity(IEntity entity)
		{
			m_entities.Remove(entity);
		}

        public void Update(float deltaTime)
        {
            m_timeStepAccumulator += deltaTime;

			for (; m_timeStepAccumulator > m_fixedTimeStep; m_timeStepAccumulator -= m_fixedTimeStep)
			{
				for (int i = 0; i < m_entitiesCount; i++)
                {
					m_entities[i].Update(m_fixedTimeStep);
                }
			}
			UpdateInput();
		}

        internal void Render()
        {
			for (int i = 0; i < m_entitiesCount; i++)
			{
				m_entities[i].Render();
			}
		}

		private void UpdateInput()
		{
			if(m_camera != null)
            {
				m_fieldOfView += Input.GetAxis("Vertical");
				m_camera.fieldOfView = Mathf.Clamp(m_fieldOfView, 40f, 160);
            }

			m_timeScale = Time.timeScale + Input.GetAxis("Horizontal") * 0.5f;
			Time.timeScale = Mathf.Clamp(m_timeScale, 0.2f, 30);

			if (Input.GetKeyDown(KeyCode.Space))
			{
				int staticsCount = RefreshStaticEntitiesList;
				if (staticsCount > 0)
				{
					if (++m_count >= staticsCount)
					{
						m_count = 0;
					}
					Entity spot = (Entity)m_staticEntities[m_count];
					Vector3 newPos = new Vector3(spot.Position.x, spot.Position.y, -10);
					if(newPos.x == float.NaN || newPos.y == float.NaN)
                    {
						newPos = new Vector3(0,0,-10);
                    }
					m_camera.transform.position = newPos;
				}
			}

			//TODO: Change visual => color/size/shape
			if (Input.GetKeyDown(KeyCode.I))
            {
				m_activeSat = RandomEntity;
				m_activeSat.SetActive(true);
			}
		}
    }
}