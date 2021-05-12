using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class TwoHandedInteractable : Interactable
{
    // Start is called before the first frame update
    protected override void OnAttachedToHand(Hand hand)
    {
        if (activateActionSetOnAttach != null)
            activateActionSetOnAttach.Activate(hand.handType);
        if (skeletonPoser != null && hand.skeleton != null)
        {
            hand.skeleton.BlendToPoser(skeletonPoser, blendToPoseTime);
        }

        attachedToHand = hand;
    }
}
