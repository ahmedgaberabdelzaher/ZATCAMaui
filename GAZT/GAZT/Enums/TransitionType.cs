using System;
using Xamarin.Forms.Internals;

namespace EGAZT.Enums
{
    [Preserve(AllMembers = true)]
    public enum TransitionType
    {
		/// <summary>
		/// Do not animate the transition.
		/// </summary>
		None = -1,

        /// <summary>
        /// Let the OS decide how to animate the transition.
        /// </summary>
        Default = 0,

        /// <summary>
        /// Show a fade transition animation.
        /// </summary>
        Fade = 1,

        /// <summary>
        /// Show a flip transition animation.
        /// </summary>
        Flip = 2,

        /// <summary>
        /// Show a scale transition animation.
        /// </summary>
        Scale = 3,

        /// <summary>
        /// Show a slide form left transition animation.
        /// </summary>
        SlideFromLeft = 4,

        /// <summary>
        /// Show a slide form right transition animation.
        /// </summary>
        SlideFromRight = 5,

        /// <summary>
        /// Show a slide form top transition animation.
        /// </summary>
        SlideFromTop = 6,

        /// <summary>
        /// Show a slide form bottom transition animation.
        /// </summary>
        SlideFromBottom = 7
    }
    [Preserve(AllMembers = true)]
    public enum PageExecutionType
    {
        Amend=5,
        Reactivation=7,
        Register,
        Update
    }
    [Preserve(AllMembers = true)]
    public enum VATRegDetailsExecutionType
    {
        Amend = 5,
        Reactivation = 7,
        Register,
        Update
    }
}
