using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Currency
{
    public class CurrencyInfoCell : UITableViewCell
    {
        const string CellId = "CellId";

        public UILabel NameLabel { get; set; }
        public UILabel CodeLabel { get; set; }
        public UILabel RateLabel { get; set; }

        public CurrencyInfoCell() : base(UITableViewCellStyle.Subtitle, CellId)
        {
            NameLabel = new UILabel(new RectangleF(36, 10, 160, 22))
            {
                AdjustsFontSizeToFitWidth = true,
            };
            AddSubview(NameLabel);

            CodeLabel = new UILabel(new RectangleF(36, 32, 100, 22))
            {
                TextColor = UIColor.Gray,
                Font = UIFont.SystemFontOfSize(12)
            };
            AddSubview(CodeLabel);

            RateLabel = new UILabel(new RectangleF(210, 18, 100, 22))
            {
                TextAlignment = UITextAlignment.Right,
                AdjustsFontSizeToFitWidth = true
            };
            AddSubview(RateLabel);
        }

        public static CurrencyInfoCell CellForInfo(CurrencyInfo info, UITableView tableView)
        {
            var cell = (CurrencyInfoCell)tableView.DequeueReusableCell(CellId) ?? new CurrencyInfoCell();
            cell.NameLabel.Text = info.Name;
            cell.CodeLabel.Text = info.Code;
            cell.RateLabel.Text = info.Rate.ToString("0.####");
            if (info.CountryCode != null)
            {
                cell.ImageView.Image = UIImage.FromFile(string.Format("Assets/Flags/{0}.png", info.CountryCode.ToLower()));
            }
            return cell;
        }
    }
}

