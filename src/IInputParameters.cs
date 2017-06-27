//  Authors:  Robert M. Scheller

using Landis.Core;
using System.Collections.Generic;

namespace Landis.Extension.Output.BiomassCommunity
{
	/// <summary>
	/// The parameters for the plug-in.
	/// </summary>
	public interface IInputParameters
	{
		/// <summary>
		/// Timestep (years)
		/// </summary>
		int Timestep
		{
			get;set;
		}

    }
}
