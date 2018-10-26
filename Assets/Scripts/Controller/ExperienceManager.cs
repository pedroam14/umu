using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Party = System.Collections.Generic.List<UnityEngine.GameObject>;

public static class ExperienceManager
{
    //a system which can distribute experience among a team of heroes, hence the whole "using Party" thing xd
    //would like heroes that are lower level to receive more experience than the heroes with a higher level, but make sure that all heroes still gain experience points
    //there can still be exceptions here like perhaps any KO’d heroes will not be able to receive experience
    const float minLevelBonus = 1.5f;
    const float maxLevelBonus = 0.5f;
    public static void AwardExperience(int amout, Party party)
    {
        //grab a list of all the Rank components from the party
        List<Rank> ranks = new List<Rank>(party.Count);
        for (int i = 0; i < party.Count; ++i)
        {
            Rank r = party[i].GetComponent<Rank>();
            if (r != null)
            {
                ranks.Add(r);
            }
        }

        //step 1: determine the range in actor level stats
        int min = int.MaxValue;
        int max = int.MinValue;
        for (int i = ranks.Count - 1; i >= 0; --i)
        {
            min = Mathf.Min(ranks[i].LVL, min);
            max = Mathf.Max(ranks[i].LVL, max);
        }

        //step 2: weight the amount to award per actor based on their level
        float[] weights = new float[party.Count];
        float summedWeights = 0;
        for (int i = ranks.Count - 1; i >= 0; --i)
        {
            float percent = (float)(ranks[i].LVL - min) / (float)(max - min);
            weights[i] = Mathf.Lerp(minLevelBonus, maxLevelBonus, percent);
            summedWeights += weights[i];
        }

        //step 3: hand out the weighted award
        for (int i = ranks.Count - 1; i >= 0; --i)
        {
            int subAmount = Mathf.FloorToInt((weights[i] / summedWeights) * amout);
            ranks[i].EXP += subAmount;
        }
    }
}