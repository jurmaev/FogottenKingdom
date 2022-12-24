using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBarController : MonoBehaviour
{
    private PlayerController player;
    private List<FrameController> frames;
    private FrameController currentActiveFrame;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        frames = GetAllFrames();
        DrawStartPlayerSkills();
        ActivateFrame(0);
        EventManager.OnChangeMagic.AddListener(ActivateFrame);
        EventManager.OnMagicStartCooldown.AddListener(ActivateFrameCooldown);
    }
    
    private void DrawStartPlayerSkills()
    {
        var currentFrameIndex = 0;
        foreach (var magic in player.magicWand.availableMagic)
        {
            frames[currentFrameIndex].SetSkillImage(magic.GetComponent<SpriteRenderer>());
            currentFrameIndex++;
        }
    }
    
    private void ActivateFrame(int frameNumber)
    {
        if (currentActiveFrame != null)
        {
            currentActiveFrame.IsActive = false;
            currentActiveFrame = frames[frameNumber];
        }
        else
            currentActiveFrame = frames[frameNumber];

        currentActiveFrame.IsActive = true;
    }

    private void ActivateFrameCooldown(int frameNumber, float cooldownTime)
    {
        frames[frameNumber].Cooldown.Activate(cooldownTime);
    }
    
    private List<FrameController> GetAllFrames()
    {
        var frames = new List<FrameController>();
        foreach (Transform child in transform)
            frames.Add(child.gameObject.GetComponent<FrameController>());
        return frames;
    }
}