using DevExpress.Xpo;

namespace Fihirana_database.fihirana
{
    public partial class Hymnal
    {
        public Hymnal(Session session) : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            //ID &= Convert.ToInt32(Session.Evaluate()(DevExpress.Data.Filtering.CriteriaOperator.Parse("Max(ID)"), null)) + 1;
        }
    }
}