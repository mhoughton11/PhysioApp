using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(PhysioApp.CustomRenderers.ProfileTableView), typeof(PhysioApp.Droid.CustomRenderers.ProfileTableViewRenderer))]
namespace PhysioApp.Droid.CustomRenderers
{
    public class ProfileTableViewRenderer : TableViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<TableView> e)
        {
            base.OnElementChanged(e);

            if (Control == null) { return; }

            var listView = Control as global::Android.Widget.ListView;
            listView.DividerHeight = 0;
        }
    }
}
