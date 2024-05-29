using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEditor;
using System.Collections.Generic;
using System.Collections;

public class BloodDecalSpawner : MonoBehaviour
{
    public GameObject bloodDecalPrefab;
    private ParticleSystem particleSystem;
    private List<ParticleCollisionEvent> collisionEvents;
    public float minWidth, maxWidth;
    public float transitionSpeed = 1.0f; // Vitesse de transition (taille maximale par seconde)

    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();

        var collisionModule = particleSystem.collision;
        collisionModule.sendCollisionMessages = true;
    }

    void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = ParticlePhysicsExtensions.GetCollisionEvents(particleSystem, other, collisionEvents);

        for (int i = 0; i < numCollisionEvents; i++)
        {
            Vector3 collisionPos = collisionEvents[i].intersection;
            Vector3 collisionNormal = collisionEvents[i].normal;

            // Créer un nouveau décal
            GameObject decal = Instantiate(bloodDecalPrefab, collisionPos, Quaternion.identity);

            // Rotation du décalcomanie pour qu'il regarde dans la direction inverse de la normale de la collision
            Quaternion decalRotation = Quaternion.LookRotation(-collisionNormal);
            decal.transform.rotation = decalRotation;

            // Accéder et modifier les propriétés de taille du DecalProjector
            DecalProjector decalProjector = decal.GetComponent<DecalProjector>();
            if (decalProjector != null)
            {
                SerializedObject serializedObject = new SerializedObject(decalProjector);
                SerializedProperty sizeProperty = serializedObject.FindProperty("m_Size");

                if (sizeProperty != null && sizeProperty.vector3Value != null)
                {
                    // Générer une valeur aléatoire pour la largeur du décalcomanie
                    float width = Random.Range(minWidth, maxWidth);

                    // Assigner la même valeur à la hauteur
                    float height = width;

                    // Taille initiale
                    Vector3 initialSize = sizeProperty.vector3Value;
                    // Taille maximale
                    Vector3 targetSize = new Vector3(width, height, initialSize.z);

                    // Durée de la transition (en secondes)
                    float transitionDuration = Mathf.Abs(Vector3.Distance(initialSize, targetSize) / transitionSpeed); // Calculer la durée basée sur la vitesse

                    // Démarrer la coroutine pour effectuer la transition progressive de la taille
                    StartCoroutine(SmoothSizeTransition(decalProjector, serializedObject, sizeProperty, initialSize, targetSize, transitionDuration));
                }
                else
                {
                    Debug.LogWarning("Property 'm_Size' not found on the DecalProjector component.");
                }
            }
            else
            {
                Debug.LogWarning("DecalProjector component not found on the instantiated decal.");
            }
        }
    }

    // Coroutine pour la transition progressive de la taille du décal
    private IEnumerator SmoothSizeTransition(DecalProjector decalProjector, SerializedObject serializedObject, SerializedProperty sizeProperty, Vector3 initialSize, Vector3 targetSize, float duration)
    {
        Material decalMaterial = decalProjector.material;

        float elapsedTime = 0.0f;
        while (elapsedTime < duration)
        {
            // Interpoler progressivement entre la taille initiale et la taille cible
            Vector3 newSize = Vector3.Lerp(initialSize, targetSize, elapsedTime / duration);
            // Mettre à jour les propriétés de taille
            sizeProperty.vector3Value = newSize;

            // Appliquer les modifications
            serializedObject.ApplyModifiedProperties();

            // Modifier une propriété du matériau pour forcer la mise à jour
            decalMaterial.SetFloat("_RandomValue", Random.value);

            // Attendre le prochain frame
            yield return null;

            // Mettre à jour le temps écoulé
            elapsedTime += Time.deltaTime;
        }
        // Assurer que la taille finale soit exactement égale à la taille cible
        sizeProperty.vector3Value = targetSize;
        // Appliquer les modifications finales
        serializedObject.ApplyModifiedProperties();

        // Modifier une propriété du matériau pour forcer la mise à jour finale
        decalMaterial.SetFloat("_RandomValue", Random.value);
    }
}
