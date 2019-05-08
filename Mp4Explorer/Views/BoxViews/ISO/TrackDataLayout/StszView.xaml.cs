//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using CMStream.Mp4;

namespace Mp4Explorer
{
    /// <summary>
    /// 
    /// </summary>
    [BoxViewType(typeof(Mp4StszBox))]
    public partial class StszView : UserControl, IBoxView
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private Mp4StszBox box;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public StszView()
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
                box = (Mp4StszBox)value;

                BoxViewUtil.AddHeader(grid, "Sample Size Box.");
                BoxViewUtil.AddField(grid, "Sample size:", box.SampleSize);
                BoxViewUtil.AddField(grid, "Sample count:", box.SampleCount);
                BoxViewUtil.AddSubHeader(grid, "Entries");
                BoxViewUtil.AddControl(grid, BuildListView(box)).Height = new GridLength(380, GridUnitType.Pixel);
            }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stsz"></param>
        /// <returns></returns>
        private ListView BuildListView(Mp4StszBox stsz)
        {
            ListView listView = new ListView();

            GridView grid = new GridView();

            GridViewColumn c1 = new GridViewColumn();
            //c1.DisplayMemberBinding = new Binding("?");
            c1.Header = "Sample size";
            grid.Columns.Add(c1);

            ObservableCollection<uint> coll = new ObservableCollection<uint>(stsz.Entries);

            listView.ItemsSource = coll;
            listView.View = grid;

            return listView;
        }

        #endregion
    }
}
