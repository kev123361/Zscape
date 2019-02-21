using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ParticleSystem))]
public class ParticlesOrbit : MonoBehaviour
{

    ParticleSystem m_System;
    ParticleSystem.Particle[] m_Particles;

    [Header("Keep in mind that this script works only in Playmode")]
    [Tooltip("Make sure to set Particle Simulation Space to world. Otherwise it uses only the center of the Particle System to Orbit.")]
    [SerializeField]
    Transform CenterOfGravity;

    public float strength = 1.0f;

    void Start()
    {
        InitializeIfNeeded();
        if (CenterOfGravity == null)
            CenterOfGravity = transform;

        if (m_System.simulationSpace == ParticleSystemSimulationSpace.Local && CenterOfGravity != null)
        {
            Debug.LogWarning("You can only use the Center of gravity variable if the ParticleSystemSimulationSpace is set to World. Now its using the center of the ParticleSystem.", this);
        }
    }

    private void LateUpdate()
    {

        // GetParticles is allocation free because we reuse the m_Particles buffer between updates
        int numParticlesAlive = m_System.GetParticles(m_Particles);

        // Change only the particles that are alive
        for (int i = 0; i < numParticlesAlive; i++)
        {
            Vector3 gravitation = new Vector3(0, 0, 0);
            if (m_System.simulationSpace == ParticleSystemSimulationSpace.World)
            {
                gravitation = CenterOfGravity.position - m_Particles[i].position;
            }
            else
            {
                gravitation = Vector3.zero - m_Particles[i].position;
            }

            Vector3 normalizedGravitation = Vector3.Normalize(gravitation);
            m_Particles[i].velocity += normalizedGravitation * strength;
        }

        // Apply the particle changes to the particle system
        m_System.SetParticles(m_Particles, numParticlesAlive);
    }

    void InitializeIfNeeded()
    {
        if (m_System == null)
            m_System = GetComponent<ParticleSystem>();

        if (m_Particles == null || m_Particles.Length < m_System.maxParticles)
            m_Particles = new ParticleSystem.Particle[m_System.maxParticles];
    }
}