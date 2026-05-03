using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{
    // Ссылка на компонент Animator
    public Animator animator;

    // Этот метод вызывается через Animation Event
    public void OnAnimationEvent()
    {
        // Запускаем следующую анимацию, например, с триггера "Next"
        animator.SetTrigger("Next");
    }
}