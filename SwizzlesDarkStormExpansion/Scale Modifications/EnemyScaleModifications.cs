using UnityEngine;
using HarmonyLib;

namespace DysonSphereProgram.Modding.SwizzlesDarkStormExpansion
{
    class EnemyRescalingPatch
    {
        [HarmonyPatch(typeof(VFPreload), nameof(VFPreload.InvokeOnLoadWorkEnded))]
        [HarmonyPostfix]
        public static void AfterLDBLoad()
        {
            EnemyRescaling.Apply(8128, new Vector3(0.5f, 0.5f, 0.5f), new Vector3(0.5f, 0.5f, 0.5f)); //raider
        }

        public class EnemyRescaling
        {
            //private static readonly Vector3 colliderScale = new Vector3(0.1f, 0.1f, 0.1f);
            //private static readonly Vector3 meshScale = new Vector3(0.1f, 0.1f, 0.1f);
            //private static readonly Vector3 selectOffset = new Vector3(0f, 0f, 0f);
            //private static readonly Vector3 selectScale = meshScale;

            public static void Apply(int protoID, Vector3 colliderScale, Vector3 meshScale)
            {
                
                EnemyUnitOriginal.InitData(protoID);
                var combatUnitProto = LDB.enemies.Select(protoID);
                
                //Plugin.Log.LogInfo("Attempting to modify enemy " + protoID + "'s scale.");

                var combatUnitPrefab = combatUnitProto.prefabDesc;

                ColliderData tmpCollider;
                
                for (int i = 0; i < combatUnitPrefab.colliders.Length; i++)
                {
                    tmpCollider = EnemyUnitOriginal.colliders[i];
                    tmpCollider.ext.Scale(colliderScale);
                    combatUnitPrefab.colliders[i] = tmpCollider;
                }
                /*
                for (int i = 0; i < combatUnitPrefab.buildColliders.Length; i++)
                {
                    tmpCollider = CombatUnitOriginal.buildColliders[i];
                    tmpCollider.ext.Scale(colliderScale);
                    combatUnitPrefab.buildColliders[i] = tmpCollider;
                }*/

                //tmpCollider = CombatUnitOriginal.buildCollider;
                //tmpCollider.ext.Scale(colliderScale);
                //combatUnitPrefab.buildCollider = tmpCollider;

                for (int i = 0; i < combatUnitPrefab.lodCount; i++)
                {
                    var mesh = combatUnitPrefab.lodMeshes[i];
                    var originalVerts = EnemyUnitOriginal.meshVertices[i];
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
                //combatUnitPrefab.selectCenter = CombatUnitOriginal.selectCenter + selectOffset;
                //combatUnitPrefab.selectSize = CombatUnitOriginal.selectSize;
                //combatUnitPrefab.selectSize.Scale(selectScale);
                //combatUnitPrefab.selectSize += 2 * selectOffset;

            }
        }
    }
}
