using DevExpress.Xpo;
namespace Fihirana_database.fihirana
{

    public partial class books
    {
        public books(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
