﻿#pragma checksum "..\..\..\Shell.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "3D1D1A6BE891B6BD0B97D9E7D63E67DD73F528EAD20D9DDF5D0C681624FDB2C2"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Practices.Composite.Presentation.Regions;
using Mp4Explorer;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Mp4Explorer {
    
    
    /// <summary>
    /// Shell
    /// </summary>
    public partial class Shell : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 18 "..\..\..\Shell.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Menu menuMain;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\Shell.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem menuItemExit;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\Shell.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem menuItemConvertToMp4;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\Shell.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem menuItemCreateFixedOffset;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\Shell.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem menuItemUploadToAzure;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\Shell.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem menuItemAbout;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\..\Shell.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.StatusBarItem statusBarItemPath;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\..\Shell.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ContentControl LeftRegion;
        
        #line default
        #line hidden
        
        
        #line 65 "..\..\..\Shell.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ContentControl MainRegion;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Mp4Explorer;component/shell.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Shell.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.menuMain = ((System.Windows.Controls.Menu)(target));
            return;
            case 2:
            this.menuItemExit = ((System.Windows.Controls.MenuItem)(target));
            
            #line 22 "..\..\..\Shell.xaml"
            this.menuItemExit.Click += new System.Windows.RoutedEventHandler(this.menuItemExit_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.menuItemConvertToMp4 = ((System.Windows.Controls.MenuItem)(target));
            
            #line 25 "..\..\..\Shell.xaml"
            this.menuItemConvertToMp4.Click += new System.Windows.RoutedEventHandler(this.menuItemConvertVideoToMp4_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.menuItemCreateFixedOffset = ((System.Windows.Controls.MenuItem)(target));
            
            #line 27 "..\..\..\Shell.xaml"
            this.menuItemCreateFixedOffset.Click += new System.Windows.RoutedEventHandler(this.menuItemCreateFixedOffset_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.menuItemUploadToAzure = ((System.Windows.Controls.MenuItem)(target));
            
            #line 28 "..\..\..\Shell.xaml"
            this.menuItemUploadToAzure.Click += new System.Windows.RoutedEventHandler(this.menuItemUploadToAzure_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.menuItemAbout = ((System.Windows.Controls.MenuItem)(target));
            
            #line 32 "..\..\..\Shell.xaml"
            this.menuItemAbout.Click += new System.Windows.RoutedEventHandler(this.menuItemAbout_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 42 "..\..\..\Shell.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.menuItemCreateFixedOffset_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 45 "..\..\..\Shell.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.menuItemUploadToAzure_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.statusBarItemPath = ((System.Windows.Controls.Primitives.StatusBarItem)(target));
            return;
            case 10:
            this.LeftRegion = ((System.Windows.Controls.ContentControl)(target));
            return;
            case 11:
            this.MainRegion = ((System.Windows.Controls.ContentControl)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
