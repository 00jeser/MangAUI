﻿using Microsoft.Maui.Controls;
using Microsoft.Maui.Essentials;
using System;
using MangAUI.Models;
using System.Collections.Generic;
using MangAUI.ViewModels;

namespace MangAUI.Views
{
    public partial class ReadPage : ContentPage
    {
        public ReadPage(ChapterCard chapter)
        {
            InitializeComponent();
            BindingContext = new ReadViewModel(chapter);
            Title = chapter.name;
        }

    }
}
