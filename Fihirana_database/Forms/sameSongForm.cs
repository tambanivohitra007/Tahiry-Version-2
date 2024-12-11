using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.XtraEditors;
using Fihirana_database.Classes;
using Fihirana_database.fihirana;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Fihirana_database.Forms
{
    public partial class sameSongForm : XtraForm
    {
        private Hymnal h;
        private List<Hymnal> tmp = [];
        public sameSongForm()
        {
            InitializeComponent();
            //refresh();
            init();
        }
        public sameSongForm(Hymnal hm)
        {
            InitializeComponent();
            h = hm;
            //refresh();            
            init();
            chantTitre.Text = $"Titre: {h.Title} ({h.Number})";
        }

        private void init()
        {
            XPQuery<Hymnal> hymnal = session.Query<Hymnal>();
            ClassFihirana.SessionHymnal = session;

            //var list = from s in hymnal select s.ID;

            var order = from s in hymnal orderby s.Category select s;

            songList.Properties.DataSource = order;
            songList.Properties.ValueMember = "ID";
            songList.Properties.DisplayMember = "Title";

            try
            {
                if (!string.IsNullOrEmpty(h.CrossReference))
                {
                    string[] splitCrossRef = h.CrossReference.Split('#');

                    for (int i = 0; i < splitCrossRef.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(splitCrossRef[i]))
                        {
                            int index = int.Parse(splitCrossRef[i]);
                            //XPQuery<Hymnal> Hm = session.FindObject<Hymnal>(typeof(Hymnal), 
                            //    CriteriaOperator.Parse($"ID = {index}"));

                            Hymnal Hm = (Hymnal)session.FindObject(typeof(Hymnal),
                            CriteriaOperator.Parse($"ID={index}"));
                            tmp.Add(Hm);
                        }
                    }

                    gridControl.DataSource = tmp;
                }
                else
                    gridControl.DataSource = null;

            }
            catch { }
        }

        private void addToListBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string test = songList.EditValue.ToString();

                Hymnal Hm = (Hymnal)session.FindObject(typeof(Hymnal),
                      CriteriaOperator.Parse($"ID={test}"));

                tmp.Add(Hm);
                gridControl.DataSource = null;
                gridControl.DataSource = tmp;
            }
            catch { }
        }
        private void iSave_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Hymnal item in tmp)
                {
                    LogClass.CrossList.Add(item.ID.ToString());
                }

                this.DialogResult = DialogResult.OK;
                Close();
            }
            catch { }
        }

        private void delBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvCrossRef.FocusedRowHandle > 0)
                {
                    int id = int.Parse(dgvCrossRef.GetRowCellValue(dgvCrossRef.FocusedRowHandle, dgvCrossRef.Columns[0]).ToString());

                    tmp.RemoveAt(dgvCrossRef.FocusedRowHandle);
                    gridControl.DataSource = null;
                    gridControl.DataSource = tmp;
                }

            }
            catch { }
        }
    }
}