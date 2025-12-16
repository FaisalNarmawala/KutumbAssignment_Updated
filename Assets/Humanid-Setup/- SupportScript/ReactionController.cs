using System.Collections;
using UnityEngine;

public class ReactionController : MonoBehaviour
{
    private bool isPlaying; // For Validation

    [Header("UI Setup/Properties")]
    public Animator animator; // Charcter Anim
    public SkinnedMeshRenderer faceMesh; // Using BlendKey Control
    public AudioSource audioClip; // Audio Play
    public Animation eyeBlink; // EyeBlink Randomly

    void Start()
    {
        Invoke("EyeBlink",Random.Range(2,7));
    }

    void EyeBlink()
    {
        eyeBlink.Play();
        Invoke("EyeBlink",Random.Range(2,7));
    }

    public void PlayReactionSequence()
    {
        if (isPlaying) return;
        StartCoroutine(ReactionRoutine());
    }

    private IEnumerator ReactionRoutine()
    {
        isPlaying = true;
        audioClip.Play();

        HappyState(true);
        yield return new WaitForSeconds(2.3f);

        SadState();
        yield return new WaitForSeconds(2.24f);

        HappyState();
        yield return new WaitForSeconds(4f);

        SadState();
        yield return new WaitForSeconds(4f);
        faceMesh.SetBlendShapeWeight(15, 0);

        isPlaying = false;
    }

    private void SadState()
    {
        animator.SetTrigger("isSad");
        faceMesh.SetBlendShapeWeight(8, 0);
        faceMesh.SetBlendShapeWeight(15, 100);
    }

    private void HappyState(bool isFirstTime = false)
    {
        if(isFirstTime)
            animator.SetTrigger("isWaving");
        else
            animator.SetTrigger("isHappy");

        faceMesh.SetBlendShapeWeight(15, 0);
        faceMesh.SetBlendShapeWeight(8, 100);
    }
}
