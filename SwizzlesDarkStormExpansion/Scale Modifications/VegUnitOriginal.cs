using UnityEngine;

namespace DysonSphereProgram.Modding.SwizzlesDarkStormExpansion;

public static class VegUnitOriginal
{
  //private static bool initialized = false;

  public static ColliderData[] colliders { get; private set; }
  public static Vector3[][] meshVertices { get; private set; }
  public static Vector3 selectCenter { get; private set; }
  public static Vector3 selectSize { get; private set; }

  public static void InitData(int id)
  {
    //if (initialized)
    //  return;

    var vegProto = LDB.veges.Select(id);
    //Plugin.Log.LogInfo("ID " + id + " selected.");

    var vegPrefab = vegProto.prefabDesc;

    var originalColliders = new ColliderData[vegPrefab.colliders.Length];
    var originalMeshVertices = new Vector3[vegPrefab.lodCount][];

    for (int i = 0; i < vegPrefab.lodCount; i++)
    {
      var vertices = vegPrefab.lodMeshes[i].vertices;
      originalMeshVertices[i] = new Vector3[vertices.Length];
      for (int j = 0; j < vertices.Length; j++)
        originalMeshVertices[i][j] = vertices[j];
    }

    for (int i = 0; i < originalColliders.Length; i++)
      originalColliders[i] = vegPrefab.colliders[i];

    colliders = originalColliders;
    meshVertices = originalMeshVertices;
    selectCenter = vegPrefab.selectCenter;
    selectSize = vegPrefab.selectSize;

    //initialized = true;
  }
}