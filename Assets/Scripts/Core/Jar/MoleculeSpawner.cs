using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(Jar))]
    public class MoleculeSpawner : GenericSpawner
    {
        [SerializeField] private Transform spawnedObjectsParent;
        [SerializeField] private Molecule moleculePrefab;

        private Jar jar;

        private void Awake()
        {
            jar = GetComponent<Jar>();
        }
        void Start()
        {
            SpawnMolecule();
            SpawnMolecule();
            SpawnMolecule();
            SpawnMolecule();
        }

        public void SpawnMolecule()
        {
            var randomPoint = GetRandomPointInSpawnArea();
            var molecule = Instantiate(moleculePrefab, randomPoint, Quaternion.identity);
            molecule.transform.SetParent(spawnedObjectsParent);
            molecule.Init();
        }
    }
}
