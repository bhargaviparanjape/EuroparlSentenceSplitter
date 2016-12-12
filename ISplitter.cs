using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentenceSplitter
{
    public interface ISplitter
    {
        /// <summary>
		/// Split Text into sentences
		/// </summary>
		/// <param name="Text"></param>
		/// <returns></returns>
		IList<string> Split(string Text);
    }
}
