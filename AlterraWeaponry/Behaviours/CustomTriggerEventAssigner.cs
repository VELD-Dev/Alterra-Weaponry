#if BZ
namespace VELD.AlterraWeaponry.Behaviours;

public class CustomTriggerStoryGoalAssigner : MonoBehaviour
{
    public StoryGoal storyGoal { get; private set; }
    public string id { get; private set; }

    public Action action { get; private set; }

    public CustomTriggerStoryGoalAssigner(StoryGoal storyGoal, string id, Action action = null)
    {
        this.storyGoal = storyGoal;
        this.id = id;
        this.action = action;
    }

    private void Start()
    { 
        bool flag = this.GetComponentInParent<BoxCollider>();
        if(!flag)
        {
            throw new Exception($"The CustomTriggerEventAssigner monobehaviour of parent CustomEventTrigger has no Collider.");
        }

        bool flag2 = this.storyGoal != null;
        if(!flag2)
        {
            throw new Exception($"The StoryGoal of the CustomTriggerEventAssigner is undefined.");
        }
    }

    private void OnTriggerEnter(Collision collision)
    {
        Main.logger.LogInfo("Custom collider triggered.");
        bool flag = this.storyGoal != null;
        if(!flag)
        {
            Main.logger.LogWarning($"TriggerBox {this.id} has no StoryGoal to play.");
            return;
        }

        Main.logger.LogInfo($"Trying to play storyGoal {this.storyGoal.key}");
        this.storyGoal.Trigger();
        Main.logger.LogInfo($"Has finished playing storyGoal {this.storyGoal.key}");

        bool flag2 = this.action != null;
        if(flag2) {
            Main.logger.LogInfo("Trigger Callback is trying to invoke Action...");
            this.action.Invoke();
            Main.logger.LogInfo("Trigger Callback has successfully executed Action");
        }
    }

}
#endif