using System.Linq;
using App.Weapons.View;
using UnityEngine;

namespace App.Entities
{
    public class AnimController : MonoBehaviour
    {
        [SerializeField] private SolderView solderView;
        [SerializeField] private Animator animator;
        [SerializeField] private WeaponViewHolder weaponViewHolder;
        [SerializeField] private AnimationClip deathClip;
        
        [Tooltip("Debug")]
        [SerializeField] private bool useDebug;
        [SerializeField] private bool isAlive = true;
        [SerializeField, Range(-1, 1)] private float velocityX;
        [SerializeField, Range(-1, 1)] private float velocityY;

        private static readonly int VelocityX = Animator.StringToHash("VelocityX");
        private static readonly int VelocityY = Animator.StringToHash("VelocityY");
        private static readonly int IsAlive = Animator.StringToHash("IsAlive");

        private void Awake() 
            => solderView.OnAliveStateChanged += SetAliveState;

        private void LateUpdate()
        {
            if (useDebug)
            {
                solderView.SetAliveState(isAlive);
                animator.SetFloat(VelocityX, velocityX);
                animator.SetFloat(VelocityY, velocityY);   
            }
            else
            {
                animator.SetFloat(VelocityX, solderView.AnimationVelocity.x);
                animator.SetFloat(VelocityY, solderView.AnimationVelocity.y);
            }
        }

        private void SetAliveState(bool isAlive)
        {
            animator.SetBool(IsAlive, solderView.IsAlive);
                
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