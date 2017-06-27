//  Authors:  Robert M. Scheller

using Landis.Core;
using Edu.Wisc.Forest.Flel.Util;
using System.Collections.Generic;

namespace Landis.Extension.Output.BiomassCommunity
{
    /// <summary>
    /// The input parameters for the plug-in.
    /// </summary>
    public class InputParameters
        : IInputParameters
    {
        private int timestep;


        //---------------------------------------------------------------------

        public int Timestep
        {
            get {
                return timestep;
            }
            set {
                if (value < 0)
                    throw new InputValueException(value.ToString(), "Value must be = or > 0");
                timestep = value;
            }
        }

    }
}
