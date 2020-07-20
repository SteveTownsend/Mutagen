﻿using Noggog.WPF;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Windows;

namespace Mutagen.Bethesda.Tests.GUI.Views
{
    public class MainViewBase : ReactiveUserControl<MainVM> { }

    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : MainViewBase
    {
        public MainView()
        {
            InitializeComponent();
            this.WhenActivated(disposable =>
            {
                this.WhenAny(x => x.ViewModel.RunningTests)
                    .Select(x => x != null)
                    .Select(running => running ? Visibility.Hidden : Visibility.Visible)
                    .BindToStrict(this, x => x.Config.Visibility)
                    .DisposeWith(disposable);
                this.WhenAny(x => x.ViewModel.RunningTests)
                    .Select(x => x != null)
                    .Select(running => running ? Visibility.Visible : Visibility.Hidden)
                    .BindToStrict(this, x => x.RunningTests.Visibility)
                    .DisposeWith(disposable);
                this.WhenAny(x => x.ViewModel.RunningTests)
                    .BindToStrict(this, x => x.RunningTests.ViewModel)
                    .DisposeWith(disposable);
            });
        }
    }
}
