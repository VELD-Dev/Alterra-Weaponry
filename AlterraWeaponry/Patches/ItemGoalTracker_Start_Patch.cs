/*namespace VELD.AlterraWeaponry.Patches;

[HarmonyPatch(typeof(ItemGoalTracker))]
internal class ItemGoalTracker_Start_Patch
{
    [HarmonyPrefix]
    [HarmonyPatch(nameof(ItemGoalTracker.Start))]
    public static void Start(ItemGoalTracker __instance)
    {
        List<TechType> techTypes = new()
        {
            ExplosiveTorpedo.TechType,
            //PrawnLaserArm.TechType,
        };

        ItemGoal[] goals = __instance.goalData.goals;
        foreach (TechType techType in techTypes)
        {
            ItemGoal goal = new ItemGoal()
            {
                techType = techType,
                goalType = Story.GoalType.PDA,
                key = "AWFirstLethal",
                playInCreative = true,
                playInCinematics = false,
            };

            goals = goals.AddItem(goal).ToArray();
        }

        foreach (TechType techType in techTypes)
        {
            ItemGoal goal = new ItemGoal()
            {
                techType = techType,
                goalType = Story.GoalType.Encyclopedia,
                key = $"Ency_{nameof(techType)}",
                playInCreative = true,
                playInCinematics = false,
            };

            goals = goals.AddItem(goal).ToArray();
        }

        __instance.goalData.goals = goals;
    }
}*/
