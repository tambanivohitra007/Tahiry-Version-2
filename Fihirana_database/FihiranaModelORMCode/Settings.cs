using DevExpress.Xpo;
namespace Fihirana_database.fihirana
{

    public partial class Settings
    {
        public Settings(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
