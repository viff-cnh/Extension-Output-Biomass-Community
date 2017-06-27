//  Copyright 2005-2017 Landis-II-Foundation
//  Authors:  Robert M. Scheller

using Landis.Core;
using Edu.Wisc.Forest.Flel.Util;
using Landis.Library.BiomassCohorts;
using Landis.SpatialModeling;
using Landis.Library.Biomass;
using Landis.Library.Metadata;
using System;
using System.Collections.Generic;
using System.IO;

namespace Landis.Extension.Output.BiomassCommunity
{
    public class PlugIn
        : ExtensionMain
    {
        public static readonly ExtensionType ExtType = new ExtensionType("output");
        public static readonly string ExtensionName = "Output Biomass Community";

        private IInputParameters parameters;
        private static ICore modelCore;
        private string outputMapName = "output-community-{timestep}.img";

        //---------------------------------------------------------------------

        public PlugIn()
            : base(ExtensionName, ExtType)
        {
        }

        //---------------------------------------------------------------------

        public static ICore ModelCore
        {
            get
            {
                return modelCore;
            }
        }
        //---------------------------------------------------------------------

        public override void LoadParameters(string dataFile, ICore mCore)
        {
            modelCore = mCore;
            InputParametersParser parser = new InputParametersParser();
            parameters = Landis.Data.Load<IInputParameters>(dataFile, parser);
        }

        //---------------------------------------------------------------------

        public override void Initialize()
        {

            Timestep = parameters.Timestep;
            MetadataHandler.Initialize(Timestep, outputMapName);

            SiteVars.Initialize();
        }

        //---------------------------------------------------------------------

        public override void Run()
        {
            CreateCommunityMap();
        }

        //---------------------------------------------------------------------

        private void CreateCommunityMap()
        {
            string path = MapNames.ReplaceTemplateVars(outputMapName, PlugIn.ModelCore.CurrentTime);
            PlugIn.ModelCore.UI.WriteLine("   Writing community biomass map to {1} ...", path);

            using (IOutputRaster<IntPixel> outputRaster = modelCore.CreateRaster<IntPixel>(path, modelCore.Landscape.Dimensions))
            {
                IntPixel pixel = outputRaster.BufferPixel;
                foreach (Site site in PlugIn.ModelCore.Landscape.AllSites)
                {
                    if (site.IsActive)
                        pixel.MapCode.Value = 1; //(int) ListOfCommunities<>;
                    else
                        pixel.MapCode.Value = 0;

                    outputRaster.WriteBufferPixel();
                }
            }
            

        }

    }
}
