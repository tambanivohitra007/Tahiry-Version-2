// ------------------------------------------------------------
// Author: Rindra Razafinjatovo
// Created on: 2018
// Last Modified: Dec 2024
// Project: Tahiry
// Description: A collection of Bible and Hymnals to streamline and enhance worship presentations for churches.
// ------------------------------------------------------------

﻿using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace Fihirana_database.Helpers
{
    public class GridFocusedRowHighlightHelper
    {
        private GridView _view = null;
        private DrawFocusRectStyle prevFocusRectStyle;
        private bool IsFocusedRowHighlighted = true;
        public bool ShowIndicationOnFirstClick { get; set; }

        public GridFocusedRowHighlightHelper(GridView view)
        {
            _view = view;
            ShowIndicationOnFirstClick = false;
            Activate();
            SetFocusedRowIndication(false);
        }

        public void SetFocusedRowIndication(bool highlight)
        {
            IsFocusedRowHighlighted = highlight;
            if (!highlight)
                prevFocusRectStyle = _view.FocusRectStyle;
            _view.BeginUpdate();
            _view.FocusRectStyle = highlight ? prevFocusRectStyle : DrawFocusRectStyle.None;
            _view.OptionsSelection.EnableAppearanceFocusedCell = IsFocusedRowHighlighted;
            _view.OptionsSelection.EnableAppearanceFocusedRow = IsFocusedRowHighlighted;
            _view.OptionsSelection.EnableAppearanceHideSelection = IsFocusedRowHighlighted;
            //_view.OptionsView.ShowIndicator = IsFocusedRowHighlighted;
            _view.EndUpdate();
        }

        private void _view_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (!IsFocusedRowHighlighted)
                e.Info.ImageIndex = -1;
        }

        private void View_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (ShowIndicationOnFirstClick)
            {
                GridHitInfo hi = _view.CalcHitInfo(e.Location);
                if (hi.InRow)
                    SetFocusedRowIndication(true);
            }
        }

        public void Deactivate()
        {
            SetFocusedRowIndication(true);
            _view.MouseDown -= View_MouseDown;
            _view.CustomDrawRowIndicator -= _view_CustomDrawRowIndicator;
        }

        public void Activate()
        {
            _view.MouseDown += View_MouseDown;
            _view.CustomDrawRowIndicator += _view_CustomDrawRowIndicator;
        }
    }
}