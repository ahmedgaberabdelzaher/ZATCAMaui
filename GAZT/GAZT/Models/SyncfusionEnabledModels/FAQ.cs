using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Xamarin.Forms.Internals;

namespace GAZT.Models.SyncfusionEnabledModels
{
    [Preserve(AllMembers = true)]
    public class FAQ
    {
        #region Properties
        /// <summary>
        /// Gets or sets the question for FAQ.
        /// </summary>
        [DataMember(Name = "question")]
        public string Question { get; set; }
        /// <summary>
        /// Gets or sets the answer for FAQ.
        /// </summary>
        [DataMember(Name = "answer")]
        public List<string> Answer { get; set; }
        #endregion
    }
}
