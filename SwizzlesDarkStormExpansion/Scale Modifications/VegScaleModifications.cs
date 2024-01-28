using UnityEngine;
using HarmonyLib;

namespace DysonSphereProgram.Modding.SwizzlesDarkStormExpansion
{
    class VegRescalingPatch
    {
        [HarmonyPatch(typeof(VFPreload), nameof(VFPreload.InvokeOnLoadWorkEnded))]
        [HarmonyPostfix]
        public static void AfterLDBLoad()
        {
            foreach (var veg in LDB.veges.dataArray)
            {
                Plugin.Log.LogInfo("Modifying a veg with ID: " + veg.ID);
                VegRescaling.Apply(veg.ID, new Vector3(0.6f, 0.6f, 0.6f), new Vector3(0.6f, 0.6f, 0.6f));
            }

        }

        public class VegRescaling
        {
            //private static readonly Vector3 colliderScale = new Vector3(0.1f, 0.1f, 0.1f);
            //private static readonly Vector3 meshScale = new Vector3(0.1f, 0.1f, 0.1f);
            //private static readonly Vector3 selectOffset = new Vector3(0f, 0f, 0f);
            //private static readonly Vector3 selectScale = meshScale;

            public static void Apply(int protoID, Vector3 colliderScale, Vector3 meshScale)
            {
                VegUnitOriginal.InitData(protoID);
                var vegUnitProto = LDB.veges.Select(protoID);

                vegUnitProto.CircleRadius = vegUnitProto.CircleRadius / 2;

                var vegUnitPrefab = vegUnitProto.prefabDesc;

                ColliderData tmpCollider;
                
                for (int i = 0; i < vegUnitPrefab.colliders.Length; i++)
                {
                    tmpCollider = VegUnitOriginal.colliders[i];
                    tmpCollider.ext.Scale(colliderScale);
                    vegUnitPrefab.colliders[i] = tmpCollider;

                }


                for (int i = 0; i < vegUnitPrefab.lodCount; i++)
                {
                    var mesh = vegUnitPrefab.lodMeshes[i];
                    var originalVerts = VegUnitOriginal.meshVertices[i];
                    var vertices = mesh.vertices;
                    for (int j = 0; j < vertices.Length; j++)
                    {
                        Vector3 vert = originalVerts[j];
                        vert.x *= meshScale.x;
                        vert.y *= meshScale.y;
                        vert.z *= meshScale.z;
                        vertices[j] = vert;
                    }
                    mesh.vertices = vertices;
                }

                vegUnitPrefab.selectSize.Scale(meshScale);
                //vegUnitPrefab.selectSize += 2 * selectOffset;

            }
        }
    }
}
