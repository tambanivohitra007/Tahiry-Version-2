// ------------------------------------------------------------
// Author: Rindra Razafinjatovo
// Created on: 2018
// Last Modified: Dec 2024
// Project: Tahiry
// Description: A collection of Bible and Hymnals to streamline and enhance worship presentations for churches.
// ------------------------------------------------------------

﻿using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections;

namespace Fihirana_database.Classes
{
    public class RefreshHelper
    {
        [Serializable]
        public struct RowInfo
        {
            public object Id;
            public int level;
        };

        private GridView view;
        private string keyFieldName;
        private ArrayList saveExpList;
        private ArrayList saveSelList;
        private ArrayList saveMasterRowsList;
        private int visibleRowIndex = -1;

        public RefreshHelper(GridView view, string keyFieldName)
        {
            this.view = view;
            this.keyFieldName = keyFieldName;
        }

        public ArrayList SaveExpList
        {
            get
            {
                if (saveExpList == null)
                    saveExpList = [];
                return saveExpList;
            }
        }

        public ArrayList SaveSelList
        {
            get
            {
                if (saveSelList == null)
                    saveSelList = [];
                return saveSelList;
            }
        }

        public ArrayList SaveMasterRowsList
        {
            get
            {
                if (saveMasterRowsList == null)
                    saveMasterRowsList = [];
                return saveMasterRowsList;
            }
        }

        protected int FindParentRowHandle(RowInfo rowInfo, int rowHandle)
        {
            int result = view.GetParentRowHandle(rowHandle);
            while (view.GetRowLevel(result) != rowInfo.level)
                result = view.GetParentRowHandle(result);
            return result;
        }

        protected void ExpandRowByRowInfo(RowInfo rowInfo)
        {
            int dataRowHandle = view.LocateByValue(0, view.Columns[keyFieldName], rowInfo.Id);
            if (dataRowHandle != GridControl.InvalidRowHandle)
            {
                int parentRowHandle = FindParentRowHandle(rowInfo, dataRowHandle);
                view.SetRowExpanded(parentRowHandle, true, false);
            }
        }

        protected int GetRowHandleToSelect(RowInfo rowInfo)
        {
            int dataRowHandle = view.LocateByValue(0, view.Columns[keyFieldName], rowInfo.Id);
            if (dataRowHandle != GridControl.InvalidRowHandle)
                if (view.GetRowLevel(dataRowHandle) != rowInfo.level)
                    return FindParentRowHandle(rowInfo, dataRowHandle);
            return dataRowHandle;
        }

        protected void SelectRowByRowInfo(RowInfo rowInfo, bool isFocused)
        {
            if (isFocused)
                view.FocusedRowHandle = GetRowHandleToSelect(rowInfo);
            else
                view.SelectRow(GetRowHandleToSelect(rowInfo));
        }

        public void SaveSelectionViewInfo(ArrayList list)
        {
            list.Clear();
            GridColumn column = view.Columns[keyFieldName];
            RowInfo rowInfo;
            int[] selectionArray = view.GetSelectedRows();
            if (selectionArray != null)  // otherwise we have a single focused but not selected row
                for (int i = 0; i < selectionArray.Length; i++)
                {
                    int dataRowHandle = selectionArray[i];
                    rowInfo.level = view.GetRowLevel(dataRowHandle);
                    if (dataRowHandle < 0) // group row
                        dataRowHandle = view.GetDataRowHandleByGroupRowHandle(dataRowHandle);
                    rowInfo.Id = view.GetRowCellValue(dataRowHandle, column);
                    _ = list.Add(rowInfo);
                }
            rowInfo.Id = view.GetRowCellValue(view.FocusedRowHandle, column);
            rowInfo.level = view.GetRowLevel(view.FocusedRowHandle);
            _ = list.Add(rowInfo);
        }

        public void SaveExpansionViewInfo(ArrayList list)
        {
            if (view.GroupedColumns.Count == 0) return;
            list.Clear();
            GridColumn column = view.Columns[keyFieldName];
            for (int i = -1; i > int.MinValue; i--)
            {
                if (!view.IsValidRowHandle(i)) break;
                if (view.GetRowExpanded(i))
                {
                    RowInfo rowInfo;
                    int dataRowHandle = view.GetDataRowHandleByGroupRowHandle(i);
                    rowInfo.Id = view.GetRowCellValue(dataRowHandle, column);
                    rowInfo.level = view.GetRowLevel(i);
                    _ = list.Add(rowInfo);
                }
            }
        }

        public void SaveExpandedMasterRows(ArrayList list)
        {
            if (view.GridControl.Views.Count == 1) return;
            list.Clear();
            GridColumn column = view.Columns[keyFieldName];
            for (int i = 0; i < view.DataRowCount; i++)
                if (view.GetMasterRowExpanded(i))
                    _ = list.Add(view.GetRowCellValue(i, column));
        }

        public void SaveVisibleIndex()
        {
            visibleRowIndex = view.GetVisibleIndex(view.FocusedRowHandle) - view.TopRowIndex;
        }

        public void LoadVisibleIndex()
        {
            view.MakeRowVisible(view.FocusedRowHandle, true);
            view.TopRowIndex = view.GetVisibleIndex(view.FocusedRowHandle) - visibleRowIndex;
        }

        public void LoadSelectionViewInfo(ArrayList list)
        {
            view.BeginSelection();
            try
            {
                view.ClearSelection();
                for (int i = 0; i < list.Count; i++)
                    SelectRowByRowInfo((RowInfo)list[i], i == list.Count - 1);
            }
            finally
            {
                view.EndSelection();
            }
        }

        public void LoadExpansionViewInfo(ArrayList list)
        {
            if (view.GroupedColumns.Count == 0) return;
            view.BeginUpdate();
            try
            {
                view.CollapseAllGroups();
                foreach (RowInfo info in list)
                    ExpandRowByRowInfo(info);
            }
            finally
            {
                view.EndUpdate();
            }
        }

        public void LoadExpandedMasterRows(ArrayList list)
        {
            view.BeginUpdate();
            try
            {
                view.CollapseAllDetails();
                GridColumn column = view.Columns[keyFieldName];
                for (int i = 0; i < list.Count; i++)
                {
                    int rowHandle = view.LocateByValue(0, column, list[i]);
                    view.SetMasterRowExpanded(rowHandle, true);
                }
            }
            finally
            {
                view.EndUpdate();
            }
        }

        public void SaveViewInfo()
        {
            SaveExpandedMasterRows(SaveMasterRowsList);
            SaveExpansionViewInfo(SaveExpList);
            SaveSelectionViewInfo(SaveSelList);
            SaveVisibleIndex();
        }

        public void LoadViewInfo()
        {
            LoadExpandedMasterRows(SaveMasterRowsList);
            LoadExpansionViewInfo(SaveExpList);
            LoadSelectionViewInfo(SaveSelList);
            LoadVisibleIndex();
        }
    }
}