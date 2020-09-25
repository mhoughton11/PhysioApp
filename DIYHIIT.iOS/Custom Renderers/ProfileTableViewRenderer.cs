using System;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace DIYHIIT.iOS.CustomRenderers
{
    public class ProfileTableViewRenderer : TableViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<TableView> e)
        {
            base.OnElementChanged(e);

            if (Control == null) { return; }

            var tableView = Control as UITableView;
            tableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
        }
    }
}
