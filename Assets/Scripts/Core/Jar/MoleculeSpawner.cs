using UnityEngine;

namespace Core
{
    public class MoleculeSpawner : GenericSpawner
    {
        [SerializeField] private Transform spawnedObjectsParent;
        [SerializeField] private Molecule moleculePrefab;
        void Start()
        {
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
