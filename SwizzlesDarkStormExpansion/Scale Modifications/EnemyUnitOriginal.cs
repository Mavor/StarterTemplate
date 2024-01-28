using UnityEngine;

namespace DysonSphereProgram.Modding.SwizzlesDarkStormExpansion;

public static class EnemyUnitOriginal
{
  //private static bool initialized = false;

  public static ColliderData[] colliders { get; private set; }
  public static ColliderData buildCollider { get; private set; }
  public static ColliderData[] buildColliders { get; private set; }
  public static Vector3[][] meshVertices { get; private set; }
  public static Vector3 selectCenter { get; private set; }
  public static Vector3 selectSize { get; private set; }

  public static void InitData(int id)
  {
    //if (initialized)
    //  return;

    var enemyProto = LDB.enemies.Select(id);
    //Plugin.Log.LogInfo("ID " + id + " selected.");

    var enemyPrefab = enemyProto.prefabDesc;

    var originalColliders = new ColliderData[enemyPrefab.colliders.Length];
    //var originalBuildColliders = new ColliderData[enemyPrefab.buildColliders.Length];
    var originalMeshVertices = new Vector3[enemyPrefab.lodCount][];

    for (int i = 0; i < enemyPrefab.lodCount; i++)
    {
      var vertices = enemyPrefab.lodMeshes[i].vertices;
      originalMeshVertices[i] = new Vector3[vertices.Length];
      for (int j = 0; j < vertices.Length; j++)
        originalMeshVertices[i][j] = vertices[j];
    }

    for (int i = 0; i < originalColliders.Length; i++)
      originalColliders[i] = enemyPrefab.colliders[i];
    //for (int i = 0; i < originalBuildColliders.Length; i++)
    //  originalBuildColliders[i] = enemyPrefab.buildColliders[i];

    buildCollider = enemyPrefab.buildCollider;
    colliders = originalColliders;
    //buildColliders = originalBuildColliders;
    meshVertices = originalMeshVertices;
    selectCenter = enemyPrefab.selectCenter;
    selectSize = enemyPrefab.selectSize;

    //initialized = true;
  }
}