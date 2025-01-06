using UnityEngine;

namespace Ursaanimation.CubicFarmAnimals
{
    public class AnimationController : MonoBehaviour
    {
        [Header("Animations")]
        public Animator animator;
        public string walkForwardAnimation = "walk_forward";

        [Header("Movement Settings")]
        public float moveSpeed = 2f; // Vitesse de marche
        public float rotationSpeed = 100f; // Vitesse de rotation
        public float changeDirectionInterval = 3f; // Temps entre les changements de direction
        public float directionChangeRadius = 10f; // Rayon dans lequel l'animal peut se déplacer

        private float changeDirectionTimer;
        private Rigidbody rb;

        void Start()
        {
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody>();

            if (rb == null)
            {
                Debug.LogError("Un Rigidbody est nécessaire pour ce script !");
                return;
            }

            // Initialiser le timer pour changer la direction
            changeDirectionTimer = changeDirectionInterval;
        }

        void FixedUpdate()
        {
            if (rb == null) return;

            // Compter le temps avant de changer de direction
            changeDirectionTimer -= Time.deltaTime;
            if (changeDirectionTimer <= 0)
            {
                // Lancer un changement de direction aléatoire
                ChangeDirection();
                changeDirectionTimer = changeDirectionInterval; // Réinitialiser le timer
            }

            // Appliquer le mouvement dans la direction actuelle
            MoveAnimal();

            // Ajuster la hauteur de l'animal en fonction du terrain
            AdjustHeightToTerrain();
        }

        void ChangeDirection()
        {
            // Calculer une direction aléatoire
            float randomX = Random.Range(-directionChangeRadius, directionChangeRadius);
            float randomZ = Random.Range(-directionChangeRadius, directionChangeRadius);

            // Appliquer cette direction pour se déplacer
            Vector3 newDirection = new Vector3(randomX, 0, randomZ);
            transform.forward = newDirection.normalized; // Rotation vers la nouvelle direction
        }

        void MoveAnimal()
        {
            // Se déplacer dans la direction actuelle
            rb.velocity = new Vector3(transform.forward.x * moveSpeed, rb.velocity.y, transform.forward.z * moveSpeed);

            // Jouer l'animation de marche si l'animal se déplace
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName(walkForwardAnimation))
            {
                animator.Play(walkForwardAnimation);
            }
        }

        void AdjustHeightToTerrain()
        {
            // Récupérer la hauteur du terrain sous l'animal
            float terrainHeight = Terrain.activeTerrain.SampleHeight(transform.position);
            Vector3 newPosition = transform.position;
            newPosition.y = terrainHeight; // Mettre à jour la position Y en fonction du terrain
            transform.position = newPosition;
        }
    }
}
