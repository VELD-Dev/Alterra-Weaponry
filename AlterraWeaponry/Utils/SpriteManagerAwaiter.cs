#if BZ
using rail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace VELD.AlterraWeaponry.Utils;

public class SpriteManagerAwaiter
{

    public Sprite Result;
    private Sprite _Result = SpriteManager.defaultSprite;

    public SpriteManagerAwaiter()
    {
        Result = _Result;
    }

    public static SpriteManagerAwaiter Get(string atlasName, string name, Sprite defaultSprite)
    {
        var sms = new SpriteManagerAwaiter();
        CoroutineHost.StartCoroutine(sms.GetAsync(atlasName, name, defaultSprite));
        return sms;
    }

    public static SpriteManagerAwaiter Get(SpriteManager.Group group, string name, Sprite defaultSprite = null)
    {
        var sms = new SpriteManagerAwaiter();
        CoroutineHost.StartCoroutine(sms.GetAsync(group, name, defaultSprite));
        return sms;
    }

    public static SpriteManagerAwaiter Get(TechType techType, Sprite defaultSprite = null)
    {
        var sms = new SpriteManagerAwaiter();
        CoroutineHost.StartCoroutine(sms.GetAsync(techType, defaultSprite));
        return sms;
    }


    private IEnumerator GetAsync(string atlasName, string name, Sprite defaultSprite)
    {
        yield return new WaitUntil(() => SpriteManager.hasInitialized);
        SetResult(SpriteManager.Get(atlasName, name, defaultSprite));
    }

    private IEnumerator GetAsync(SpriteManager.Group group, string name, Sprite defaultSprite = null)
    {
        yield return new WaitUntil(() => SpriteManager.hasInitialized);
        SetResult(SpriteManager.Get(group, name, defaultSprite));
    }

    private IEnumerator GetAsync(TechType techType, Sprite defaultSprite = null)
    {
        yield return new WaitUntil(() => SpriteManager.hasInitialized);
        if (defaultSprite != null)
            SetResult(SpriteManager.Get(techType, defaultSprite));
        else
            SetResult(SpriteManager.Get(techType));
    }

    private void SetResult(Sprite sprite)
    {
        _Result = sprite;
        if(sprite != null)
        {
            Interlocked.Exchange<Sprite>(ref Result, _Result);
        }
        else
        {
            Interlocked.Exchange<Sprite>(ref Result, SpriteManager.defaultSprite);
        }
    }
    
    // This is here just in case.
    private void SetResult()
    {
        if (_Result != null)
        {
            Interlocked.Exchange<Sprite>(ref Result, _Result);
        }
        else
        {
            throw new InvalidOperationException("You cannot set the result of the SpriteManagerAwaiter class because the _Result was not fetched yet!");
        }
    }
}
#endif