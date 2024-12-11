using DevExpress.Xpo;

namespace Fihirana_database.fihirana
{
    public partial class Category
    {
        public Category(Session session) : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}