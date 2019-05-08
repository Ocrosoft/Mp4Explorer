﻿//===============================================================================
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
    [BoxViewType(typeof(Mp4MehdBox))]
    public partial class MehdView : UserControl, IBoxView
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private Mp4MehdBox box;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public MehdView()
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
                box = (Mp4MehdBox)value;

                BoxViewUtil.AddHeader(grid, "Movie Extends Header Box");

                BoxViewUtil.AddField(grid, "Fragment duration:", box.FragmentDuration);
            }
        }

        #endregion
    }
}
