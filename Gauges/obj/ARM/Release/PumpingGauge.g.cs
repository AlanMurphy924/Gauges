﻿#pragma checksum "C:\Users\Alan\Documents\Visual Studio 2015\Projects\Gauges\Gauges\PumpingGauge.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "49A7170CCDD2F5E6A86EF87614C245AF"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Gauges
{
    partial class PumpingGauge : 
        global::Windows.UI.Xaml.Controls.UserControl, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                {
                    global::Windows.UI.Xaml.Controls.UserControl element1 = (global::Windows.UI.Xaml.Controls.UserControl)(target);
                    #line 11 "..\..\..\PumpingGauge.xaml"
                    ((global::Windows.UI.Xaml.Controls.UserControl)element1).SizeChanged += this.UserControl_SizeChanged;
                    #line default
                }
                break;
            case 2:
                {
                    this.gauge = (global::Microsoft.Graphics.Canvas.UI.Xaml.CanvasControl)(target);
                    #line 14 "..\..\..\PumpingGauge.xaml"
                    ((global::Microsoft.Graphics.Canvas.UI.Xaml.CanvasControl)this.gauge).Draw += this.gauge_Draw;
                    #line default
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

