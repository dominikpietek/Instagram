﻿#pragma checksum "..\..\..\..\Views\CreateAccountWindowView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "D1A21B83924DE32FD23C4BDF1188BFD0D75690AE"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Instagram.Components;
using Instagram.Validations;
using Instagram.Views;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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
using System.Windows.Shell;


namespace Instagram.Views {
    
    
    /// <summary>
    /// CreateAccountWindowView
    /// </summary>
    public partial class CreateAccountWindowView : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 58 "..\..\..\..\Views\CreateAccountWindowView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox nicknameBox;
        
        #line default
        #line hidden
        
        
        #line 71 "..\..\..\..\Views\CreateAccountWindowView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox emailBox;
        
        #line default
        #line hidden
        
        
        #line 84 "..\..\..\..\Views\CreateAccountWindowView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Instagram.Components.PasswordBoxUserControl firstPasswordBox;
        
        #line default
        #line hidden
        
        
        #line 100 "..\..\..\..\Views\CreateAccountWindowView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Instagram.Components.PasswordBoxUserControl secondPasswordBox;
        
        #line default
        #line hidden
        
        
        #line 116 "..\..\..\..\Views\CreateAccountWindowView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox firstNameBox;
        
        #line default
        #line hidden
        
        
        #line 129 "..\..\..\..\Views\CreateAccountWindowView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox lastNameBox;
        
        #line default
        #line hidden
        
        
        #line 142 "..\..\..\..\Views\CreateAccountWindowView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker birthdateBox;
        
        #line default
        #line hidden
        
        
        #line 150 "..\..\..\..\Views\CreateAccountWindowView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox profilePhotoRadioButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Instagram;component/views/createaccountwindowview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\CreateAccountWindowView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.nicknameBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.emailBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.firstPasswordBox = ((Instagram.Components.PasswordBoxUserControl)(target));
            return;
            case 4:
            this.secondPasswordBox = ((Instagram.Components.PasswordBoxUserControl)(target));
            return;
            case 5:
            this.firstNameBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.lastNameBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.birthdateBox = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 8:
            this.profilePhotoRadioButton = ((System.Windows.Controls.CheckBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

