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
            EnemyRescaling.Apply(8129, new Vector3(0.5f, 0.5f, 0.5f), new Vector3(0.5f, 0.5f, 0.5f)); //ranger
            EnemyRescaling.Apply(8130, new Vector3(0.5f, 0.5f, 0.5f), new Vector3(0.5f, 0.5f, 0.5f)); //guardian
            EnemyRescaling.Apply(8113, new Vector3(0.15f, 0.15f, 0.15f), new Vector3(0.15f, 0.15f, 0.15f)); //lancer
            EnemyRescaling.Apply(8112, new Vector3(0.15f, 0.15f, 0.15f), new Vector3(0.15f, 0.15f, 0.15f)); //hump
            EnemyRescaling.Apply(8114, new Vector3(0.25f, 0.25f, 0.25f), new Vector3(0.25f, 0.25f, 0.25f)); //interceptor
            EnemyRescaling.Apply(8115, new Vector3(0.25f, 0.25f, 0.25f), new Vector3(0.25f, 0.25f, 0.25f)); //small bomber
            //EnemyRescaling.Apply(8117, new Vector3(0.4f, 0.4f, 0.4f), new Vector3(0.4f, 0.4f, 0.4f)); //ant
            //EnemyRescaling.Apply(8118, new Vector3(0.25f, 0.25f, 0.25f), new Vector3(0.25f, 0.25f, 0.25f)); //heavy barge
            
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
                foreach(Material mat in combatUnitPrefab.materials)
                {
                    //Plugin.Log.LogInfo("Shader's name is: " + mat.shader.name);
                    
                    if (protoID == 8128)
                    {
                        mat.SetVector("_Position101", new Vector4(0.0f, 0.0575f, -0.77f));
                        mat.SetVector("_SizeSettings", new Vector4(1.35f, 0.26f, 0.7f,0.06f));
                    }
                    else if(protoID == 8129)
                    {
                        mat.SetVector("_Position101", new Vector4(0.0f, 0.050f, -0.75f));
                        mat.SetVector("_SizeSettings", new Vector4(3.5f, 0.20f, 0.35f,0.035f));
                    }
                }

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
                combatUnitPrefab.selectSize.Scale(meshScale);
                combatUnitPrefab.selectSize.x *= meshScale.x;
                combatUnitPrefab.selectSize.y *= meshScale.y;
                combatUnitPrefab.selectSize.z *= meshScale.z;

            }
        }
    }
}
