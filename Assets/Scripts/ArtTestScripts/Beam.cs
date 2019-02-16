using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour {

	public float goalRadius = 1.35f;
	public float goalLength = 10f;

	private float initialRadius;
	private float initialLength;

	public GameObject particlePrefab;
	private Dictionary<Collider, GameObject> particleSystems = new Dictionary<Collider, GameObject>();

	public GameObject residueParticleSystem;

	//Saves the initial radius / scale so changes in the prefab can be respected here
	void Start() {
		initialRadius = gameObject.transform.localScale.x;
		initialLength = gameObject.transform.localScale.z;
		StartCoroutine(growDisk());
		StartCoroutine(pulse());
	}

	public IEnumerator growDisk() {
		Vector3 currentDisk = new Vector3(initialRadius, initialRadius, initialLength);
		Vector3 goalDisk = new Vector3(goalRadius, goalRadius, initialLength);

		yield return Beam.Lerp(1f, t => {
			gameObject.transform.localScale = Vector3.Lerp(currentDisk, goalDisk, Mathf.Pow(t, 0.5f));
		});
	}

	public IEnumerator extendBeam() {
		Vector3 currentBeam = new Vector3(goalRadius, goalRadius, initialLength);
		Vector3 goalBeam = new Vector3(goalRadius, goalRadius, goalLength);

		yield return Beam.Lerp(0.65f, t => {
			gameObject.transform.localScale = Vector3.Lerp(currentBeam, goalBeam, Mathf.Pow(t, 0.5f));
		});
	}

	public IEnumerator collapseBeam() {

		// spawn the particles
		GameObject particles = Instantiate(residueParticleSystem, gameObject.transform.position, Quaternion.Inverse(residueParticleSystem.transform.rotation), gameObject.transform);
		ParticleSystem.ShapeModule sm = particles.GetComponent<ParticleSystem>().shape;
		sm.scale = new Vector3(sm.scale.x, sm.scale.y, 0.5f * goalLength);
		ParticleSystem.EmissionModule em = particles.GetComponent<ParticleSystem>().emission;
		em.rateOverTime = em.rateOverTime.constant * 0.5f * goalLength;

		// Make the beam shrink
		Vector3 currentBeam = new Vector3(goalRadius, goalRadius, goalLength);
		Vector3 goalBeam = new Vector3(0.001f, 0.001f, goalLength);

		yield return Beam.Lerp(0.5f, t => {
			gameObject.transform.localScale = Vector3.Lerp(currentBeam, goalBeam, Mathf.Pow(t, 2f));
		});

		//Set the Z size to zero so that oncolexit events get called to clean up particles
		gameObject.transform.localScale = new Vector3(goalBeam.x, goalBeam.y, 0);
	}

	//This handles instantiation of particles. #TODO consider averaging the normal of all points instead of using a sphere emiter
	void OnCollisionEnter(Collision other) {
		if (other.gameObject.tag != "Enemy") {

			Vector3 particlePoint = new Vector3(0, 0, 0);
			ContactPoint[] contacts = new ContactPoint[other.contactCount];
			other.GetContacts(contacts);

			//Spawn the particle effect at the average of the contacts
			foreach (ContactPoint contact in contacts) {
				particlePoint += (contact.point / contacts.Length);
			}

			GameObject particles = Instantiate(particlePrefab, particlePoint, particlePrefab.transform.rotation, other.transform);
			particleSystems.Add(other.collider, particles);
		}
	}

	//this cleans up particles when the beam leaves
	void OnCollisionExit(Collision other) {
		if (other.gameObject.tag != "Enemy") {
			GameObject particles = particleSystems[other.collider];
			particleSystems.Remove(other.collider);
			Destroy(particles);
		}
	}

	//Pulsing visual effect
	private IEnumerator pulse() {
		Material mat = gameObject.GetComponent<Renderer>().material;

		Color initialColor = mat.GetColor("_EmissionColor") * Mathf.GammaToLinearSpace(0.92f);
		Color targetColor = mat.GetColor("_EmissionColor") * Mathf.GammaToLinearSpace(1.1f);

		//conveniently, coroutines are stopped when an object is destroyed, so we don't really have to handle anything
		while (true) {
			yield return Beam.Lerp(0.35f, t => {
				mat.SetColor("_EmissionColor", Color.Lerp(initialColor, targetColor, t));
			});
			yield return Beam.Lerp(0.35f, t => {
				mat.SetColor("_EmissionColor", Color.Lerp(targetColor, initialColor, t));
			});
		}
	}


	//Utility function :^)
	public static IEnumerator Lerp(float duration, Action<float> perStep) {
		float timer = 0;
		while ((timer += Time.deltaTime) < duration) {
			perStep(timer / duration);
			yield return null;
		}
		perStep(1);
	}

}
