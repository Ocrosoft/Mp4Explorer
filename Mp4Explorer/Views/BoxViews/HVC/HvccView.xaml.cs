//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System.Windows.Controls;
using CMStream.Mp4;

namespace Mp4Explorer
{
    /// <summary>
    /// 
    /// </summary>
    [BoxViewType(typeof(Mp4HvccBox))]
    public partial class HvccView : UserControl, IBoxView
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private Mp4HvccBox box;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public HvccView()
        {
            InitializeComponent();
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public Mp4Box Box
        {
            get
            {
                return box;
            }
            set
            {
                box = (Mp4HvccBox)value;

                BoxViewUtil.AddHeader(grid, "HVC Decoder Configuration Record");

                BoxViewUtil.AddField(grid, "Configuration version:", box.ConfigurationVersion);
                BoxViewUtil.AddField(grid, "General profile space:", box.GeneralProfileSpace);
                BoxViewUtil.AddField(grid, "General tier flag:", Mp4Util.GetTierNameHEVC((byte)box.GeneralTierFlag).ToString() + "(" + box.GeneralTierFlag.ToString("X") + ")");
                BoxViewUtil.AddField(grid, "General profile indication:", Mp4Util.GetProfileNameHEVC(box.GeneralProfileIdc).ToString() + "(" + box.GeneralProfileIdc.ToString("X") + ")");
                BoxViewUtil.AddField(grid, "General profile compatibility flag:", box.GeneralProfileCompatibilityFlags.ToString("X"));
                BoxViewUtil.AddField(grid, "General constraint indicator flag:", box.GeneralConstraintIndicatorFlags.ToString("X"));
                BoxViewUtil.AddField(grid, "General level indication:", box.GeneralLevelIdc);
                BoxViewUtil.AddField(grid, "Min spatial segmentation indication:", box.MinSpatialSegmentationIdc);
                BoxViewUtil.AddField(grid, "Parallelism type:", box.ParallelismType);
                BoxViewUtil.AddField(grid, "Chroma format:", box.ChromaFormat);
                BoxViewUtil.AddField(grid, "Bit depth chroma:", box.BitDepthChromaMinus8 + 8);
                BoxViewUtil.AddField(grid, "Bit depth luma:", box.BitDepthLumaMinus8 + 8);
                BoxViewUtil.AddField(grid, "Avg frame rate:", box.avgFrameRate);
                BoxViewUtil.AddField(grid, "Constant frame rate:", box.constantFrameRate);
                BoxViewUtil.AddField(grid, "Num temporal layers:", box.numTemporalLayers);
                BoxViewUtil.AddField(grid, "Temporal id nested:", box.temporalIdNested);
                BoxViewUtil.AddField(grid, "Length size:", box.lengthSizeMinusOne + 1);
                BoxViewUtil.AddField(grid, "Num of arrays:", box.numOfArrays);

                for (int i = 0; i < box.VideoParameters.Count; i++)
                {
                    BoxViewUtil.AddField(grid, "Video parameter " + i + ":", box.VideoParameters[i]);
                }

                for (int i = 0; i < box.SequenceParameters.Count; i++)
                {
                    BoxViewUtil.AddField(grid, "Sequence parameter " + i + ":", box.SequenceParameters[i]);
                }

                for (int i = 0; i < box.PictureParameters.Count; i++)
                {
                    BoxViewUtil.AddField(grid, "Picture parameter " + i + ":", box.PictureParameters[i]);
                }
            }
        }

        #endregion
    }
}
