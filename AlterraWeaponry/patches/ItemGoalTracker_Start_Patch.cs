using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VELD.AlterraWeaponry.patches;

[HarmonyPatch(typeof(ItemGoalTracker))]
internal class ItemGoalTracker_Start_Patch
{
    [HarmonyPrefix]
    [HarmonyPatch("Start")]
    public static void Start(ItemGoalTracker __instance)
    {
        List<TechType> techTypes = new()
        {
            ExplosiveTorpedo.techType,
            //PrawnLaserArm.techType,
        };

        var goals = __instance.goalData.goals;
        foreach (TechType techType in techTypes)
        {
            var goal = new ItemGoal()
            {
                techType = techType,
                goalType = Story.GoalType.PDA,
                key = "Log_PDA_Goal_FirstLethal",
                playInCreative = true,
                playInCinematics = false,
            };

            goals = goals.AddItem(goal).ToArray();
        }
        __instance.goalData.goals = goals;
    }
}
