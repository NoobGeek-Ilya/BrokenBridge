using UnityEngine;

public class STutorialPages : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private GameObject[] buttons;

    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        //animator.CrossFade("TutorialPages", 0.001f);
        //����� �������� �� �����, �������: �������� ������ �������������� � �����
        float animationDuration = animator.GetCurrentAnimatorStateInfo(0).length;
        animator.CrossFade("TutorialPage2", 0.001f, -1, animationDuration);
        buttons[0].SetActive(true);
        buttons[1].SetActive(false);
    }

    public void RightButt()
    {
        animator.SetInteger("PageNum", 2);
        buttons[0].SetActive(false);
        buttons[1].SetActive(true);

    }
    public void LeftButt()
    {
        animator.SetInteger("PageNum", 1);
        buttons[0].SetActive(true);
        buttons[1].SetActive(false);
    }
}
