using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Json;
using MonoTouch.CoreFoundation;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Currency
{
    public class CurrenciesController : UIViewController
    {
        public UITableView InfosTableView { get; private set; }
        public UISearchDisplayController SearchController { get; private set; }

        public CurrenciesController()
        {
            Title = "Currency";
        }

        public override void LoadView()
        {
            base.LoadView();

            InfosTableView = new UITableView(RectangleF.Empty, UITableViewStyle.Plain)
            {
                Frame = this.ContentFrame(),
                Source = new InfosTableSource(),
                RowHeight = 60
            };
            View.AddSubview(InfosTableView);

            var searchBar = new UISearchBar(RectangleF.Empty)
            {
                ShowsCancelButton = true,
                Placeholder = "Find Currency",
                KeyboardType = UIKeyboardType.ASCIICapable
            };
            searchBar.SizeToFit();
            InfosTableView.TableHeaderView = searchBar;
    
            SearchController = new UISearchDisplayController(searchBar, this)
            {
                Delegate = new SearchDisplayDelegate(),
                SearchResultsSource = new InfosTableSource()
            };
            SearchController.SearchResultsTableView.RowHeight = 60;
        }

        public override void ViewDidLoad()
        {
            DispatchQueue.DefaultGlobalQueue.DispatchAsync(() => {
                try
                {
                    var update = CurrencyUpdate.Latest();

                    DispatchQueue.MainQueue.DispatchSync(() => {
                        (InfosTableView.Source as InfosTableSource).UpdateAll(update.Infos);
                        InfosTableView.ReloadData();
                        (SearchController.Delegate as SearchDisplayDelegate).UpdateAll(update.Infos);
                        Title = "Last Update: " + update.Timestamp.ToString("HH:mm");
                    });
                }
                catch (Exception ex)
                {
                    DispatchQueue.MainQueue.DispatchSync(() => {
                        var alert = new UIAlertView();
                        alert.Message = ex.Message;
                        alert.AddButton("OK");
                        alert.Show();
                    });
                }
            });
        }
    }

    public class InfosTableSource : UITableViewSource
    {
        public List<CurrencyInfo> Infos = new List<CurrencyInfo>();

        public void UpdateAll(IEnumerable<CurrencyInfo> infos)
        {
            Infos.Clear();
            Infos.AddRange(infos);
        }

        public override int RowsInSection(UITableView tableview, int section)
        {
            return Infos.Count;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var info = Infos[indexPath.Row];
            return CurrencyInfoCell.CellForInfo(info, tableView);
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            tableView.DeselectRow(indexPath, true);
        }
    }

    public class SearchDisplayDelegate : UISearchDisplayDelegate
    {
        public List<CurrencyInfo> Infos = new List<CurrencyInfo>();

        public void UpdateAll(IEnumerable<CurrencyInfo> infos)
        {
            Infos.Clear();
            Infos.AddRange(infos);
        }

        public override bool ShouldReloadForSearchScope(UISearchDisplayController controller, int forSearchOption)
        {
            return true;
        }

        public override bool ShouldReloadForSearchString(UISearchDisplayController controller, string forSearchString)
        {
            if (!string.IsNullOrWhiteSpace(forSearchString))
            {
                var searchText = forSearchString.ToUpper();
                var searchInfos = Infos.Where(i => i.Code.StartsWith(searchText) || i.Name.ToUpper().StartsWith(searchText));
                (controller.SearchResultsTableView.Source as InfosTableSource).UpdateAll(searchInfos);
            }
            return true;
        }
    }
}

