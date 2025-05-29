using App.Weapons.View;
using UnityEngine;

namespace App.Entities
{
    public class EntityAnimController : MonoBehaviour
    {
        [SerializeField] private EntityView entityView;
        [SerializeField] private Animator animator;
        [SerializeField] private WeaponViewHolder weaponViewHolder;
        [SerializeField] private AnimationClip deathClip;
        
        [Tooltip("Debug")]
        [SerializeField] private bool useDebug;
        [SerializeField] private bool debugIsAlive = true;
        [SerializeField, Range(-1, 1)] private float debugVelocityX;
        [SerializeField, Range(-1, 1)] private float debugVelocityY;

        private static readonly int VelocityX = Animator.StringToHash("VelocityX");
        private static readonly int VelocityY = Animator.StringToHash("VelocityY");
        private static readonly int IsAlive = Animator.StringToHash("IsAlive");

        private void Awake() 
            => entityView.OnAliveStateChanged += SetAliveState;

        private void LateUpdate()
        {
            if (useDebug)
            {
                entityView.SetAliveState(debugIsAlive);
                animator.SetFloat(VelocityX, debugVelocityX);
                animator.SetFloat(VelocityY, debugVelocityY);   
            }
            else
            {
                animator.SetFloat(VelocityX, entityView.AnimationVelocity.x);
                animator.SetFloat(VelocityY, entityView.AnimationVelocity.y);
            }
        }

        private void SetAliveState(bool isAlive)
        {
            animator.SetBool(IsAlive, entityView.IsAlive);
                
            if (isAlive)
            {
                weaponViewHolder.Default();
            }
            else
            {
                var duration = deathClip.length;
                weaponViewHolder.Death(duration);
            }
        }
    }
}