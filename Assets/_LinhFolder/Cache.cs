using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Cache
{
    //private static Dictionary<Collider, IHit> ihits = new Dictionary<Collider, IHit>();

    //public static IHit GetIHit(Collider collider)
    //{
    //    if (!ihits.ContainsKey(collider))
    //    {
    //        ihits.Add(collider, collider.GetComponent<IHit>());
    //    }

    //    return ihits[collider];
    //}

    private static Dictionary<Collider, Character> characters = new Dictionary<Collider, Character>();

    public static Character GetCharacter(Collider collider)
    {
        if (!characters.ContainsKey(collider))
        {
            characters.Add(collider, collider.GetComponent<Character>());
        }

        return characters[collider];
    }

    private static Dictionary<Collider, Bot> bots = new Dictionary<Collider, Bot>();
    public static Bot GetBot(Collider collider)
    {
        if (!bots.ContainsKey(collider))
        {
            bots.Add(collider, collider.GetComponent<Bot>());
        }

        return bots[collider];
    }

    private static Dictionary<Collider, Player> player = new Dictionary<Collider, Player>();
    public static Player GetPlayer(Collider collider)
    {
        if (!player.ContainsKey(collider))
        {
            player.Add(collider, collider.GetComponent<Player>());
        }

        return player[collider];
    }

    private static Dictionary<Collider, Exp> exp = new Dictionary<Collider, Exp>();
    public static Exp GetExp(Collider collider)
    {
        if (!exp.ContainsKey(collider))
        {
            exp.Add(collider, collider.GetComponent<Exp>());
        }

        return exp[collider];
    }
}
