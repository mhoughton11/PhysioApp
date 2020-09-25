using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(DIYHIIT.CustomRenderers.ProfileTableView), typeof(DIYHIIT.Droid.CustomRenderers.ProfileTableViewRenderer))]
namespace DIYHIIT.Droid.CustomRenderers
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
