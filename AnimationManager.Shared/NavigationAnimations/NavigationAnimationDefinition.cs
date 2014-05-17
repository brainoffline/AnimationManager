using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Brain.Animate.NavigationAnimations
{
    /// <summary>
    /// Definition of animations used when navigating FORWARD from page 1 to page 2
    /// or Navigating BACK from page 2 back to page 1
    /// </summary>
    abstract public class NavigationAnimationDefinition
    {
        /// <summary>
        /// Animation Definition for closing page 1 when navigating forward
        /// </summary>
        public AnimationDefinition ForwardAnimationCloseOld;

        /// <summary>
        /// Animation Definition for opening page 2 when navigating forward
        /// </summary>
        public AnimationDefinition ForwardAnimationOpenNew;

        /// <summary>
        /// Do the Close page 1 animation and Open page 2 animation run syncronously or sequentially
        /// when navigating forward
        /// </summary>
        public bool ForwardAnimationSequential;

        /// <summary>
        /// Does Page 1 drop away to reveal page 2
        /// </summary>
        public bool ForwardAnimationReveal;


        /// <summary>
        /// Animation Definition for closing page 2 when navigating backwards
        /// </summary>
        public AnimationDefinition BackAnimationCloseTop;

        /// <summary>
        /// Animation Definition for re-opening page 1 when navigating backwards
        /// </summary>
        public AnimationDefinition BackAnimationReOpenBottom;

        /// <summary>
        /// Do the Close page 2 animation and Re-Open page 1 animation run syncronously or sequentially
        /// when navigating back
        /// </summary>
        public bool BackAnimationSequential;

        /// <summary>
        /// 
        /// </summary>
        public bool BackAnimationReveal;
    }

    public class JumpLeftNavigationAnimation : NavigationAnimationDefinition
    {
        public JumpLeftNavigationAnimation()
        {
            ForwardAnimationCloseOld = new FrameFadeBackAnimation { Duration = 0.4, HorizontalCentre = 0.0 };
            ForwardAnimationOpenNew = new FrameMoveInLeftAnimation { Duration = 0.4 };
            ForwardAnimationSequential = false;

            BackAnimationCloseTop = new FrameMoveOutRightAnimation { Duration = 0.4 };
            BackAnimationReOpenBottom = new FrameFadeInAnimation { Duration = 0.4 };
            BackAnimationSequential = false;
        }
    }

    public class JumpDoubleLeftNavigationAnimation : NavigationAnimationDefinition
    {
        public JumpDoubleLeftNavigationAnimation()
        {
            ForwardAnimationCloseOld = new FrameFadeBackAnimation { Duration = 0.4, HorizontalCentre = -1.4 };
            ForwardAnimationOpenNew = new FrameMoveInLeftAnimation { Duration = 0.4, JumpUp = true };
            ForwardAnimationSequential = false;

            BackAnimationCloseTop = new FrameMoveOutRightAnimation { Duration = 0.4 };
            BackAnimationReOpenBottom = new FrameFadeInAnimation { Duration = 0.4, HorizontalCentre = -1.4 };
            BackAnimationSequential = false;
        }
    }

    public class SuperScaleNavigationAnimation : NavigationAnimationDefinition
    {
        public SuperScaleNavigationAnimation()
        {
            ForwardAnimationCloseOld = new ScaleOutAnimation { EndScale = 0.0, Duration = 0.4 };
            ForwardAnimationOpenNew = new ScaleInAnimation { StartScale = 1.2, Duration = 0.4 };
            ForwardAnimationSequential = false;

            BackAnimationCloseTop = new ScaleOutAnimation { EndScale = 1.2, Duration = 0.4 };
            BackAnimationReOpenBottom = new ScaleInAnimation { StartScale = 0.0, Duration = 0.4 };
            BackAnimationSequential = false;
        }
    }

    public class FadeDownNavigationAnimation : NavigationAnimationDefinition
    {
        public FadeDownNavigationAnimation()
        {
            ForwardAnimationCloseOld = new FadeOutDownAnimation {Distance = 50};
            ForwardAnimationOpenNew = new ScaleInAnimation { Delay = 0.2 };
            ForwardAnimationSequential = false;

            BackAnimationCloseTop = new ScaleOutAnimation();
            BackAnimationReOpenBottom = new FadeInUpAnimation {Distance = 50, Delay = 0.2};
            BackAnimationSequential = false;
        }
    }


    public class SwipeLeftNavigationAnimation : NavigationAnimationDefinition
    {
        public SwipeLeftNavigationAnimation()
        {
            ForwardAnimationCloseOld = null;
            ForwardAnimationOpenNew = new FrameMoveInLeftAnimation();
            ForwardAnimationSequential = false;

            BackAnimationCloseTop = new FrameMoveOutRightAnimation();
            BackAnimationReOpenBottom = null;
            BackAnimationSequential = false;
        }
    }

    public class HingeNavigationAnimation : NavigationAnimationDefinition
    {
        public HingeNavigationAnimation()
        {
            ForwardAnimationCloseOld = new HingeAnimation();
            ForwardAnimationOpenNew = null;
            ForwardAnimationSequential = false;
            ForwardAnimationReveal = true;

            BackAnimationCloseTop = new HingeAnimation();
            BackAnimationReOpenBottom = null;
            BackAnimationSequential = false;
        }
    }

    public class RotateScaleUpNavigationAnimation : NavigationAnimationDefinition
    {
        public RotateScaleUpNavigationAnimation()
        {
            ForwardAnimationCloseOld = new FrameFadeBackAnimation();
            ForwardAnimationOpenNew = new RotateInAnimation();
            ForwardAnimationSequential = false;

            BackAnimationCloseTop = new RotateOutAnimation();
            BackAnimationReOpenBottom = new FrameFadeInAnimation();
            BackAnimationSequential = false;
        }
    }

    public class RotateScaleDownNavigationAnimation : NavigationAnimationDefinition
    {
        public RotateScaleDownNavigationAnimation()
        {
            ForwardAnimationCloseOld = new FrameFadeBackAnimation();
            ForwardAnimationOpenNew = new RotateInAnimation { RotateDirection = RotateAnimationDirection.RotateDown };
            ForwardAnimationSequential = false;

            BackAnimationCloseTop = new RotateOutAnimation { RotateDirection = RotateAnimationDirection.RotateDown };
            BackAnimationReOpenBottom = new FrameFadeInAnimation();
            BackAnimationSequential = false;
        }
    }

    public class TurnstileNavigationAnimation : NavigationAnimationDefinition
    {
        public TurnstileNavigationAnimation()
        {
            ForwardAnimationCloseOld = new TurnstileLeftOutAnimation();
            ForwardAnimationOpenNew = new TurnstileRightInAnimation { Delay = 0.2 };
            ForwardAnimationSequential = false;

            BackAnimationCloseTop = new TurnstileRightOutAnimation();
            BackAnimationReOpenBottom = new TurnstileLeftInAnimation { Delay = 0.2 };
            BackAnimationSequential = false;
            BackAnimationReveal = true;
        }
    }

    public class SwapPageNavigationAnimation : NavigationAnimationDefinition
    {
        public SwapPageNavigationAnimation()
        {
            ForwardAnimationCloseOld = new SwapLeftOutAnimation();
            ForwardAnimationOpenNew = new SwapRightInAnimation(); 
            ForwardAnimationSequential = false;
            ForwardAnimationReveal = true;

            BackAnimationCloseTop = new SwapRightOutAnimation();
            BackAnimationReOpenBottom = new SwapLeftInAnimation();
            BackAnimationSequential = false;
            
        }
    }

    public class CentreFlipNavigationAnimation : NavigationAnimationDefinition
    {
        public CentreFlipNavigationAnimation()
        {
            ForwardAnimationCloseOld = new FlipOutYAnimation { Centre = 0.5, Duration = 0.4 };
            ForwardAnimationOpenNew = new FlipInYAnimation { Centre = 0.5, Delay = 0.2, Duration = 0.4 };
            ForwardAnimationSequential = false;
            ForwardAnimationReveal = true;

            BackAnimationCloseTop = new FlipOutYAnimation { Centre = 0.5, Reverse = true, Duration = 0.4 };
            BackAnimationReOpenBottom = new FlipInYAnimation { Centre = 0.5, Reverse = true, Delay = 0.2, Duration = 0.4 };
            BackAnimationSequential = false;

        }
    }
}

