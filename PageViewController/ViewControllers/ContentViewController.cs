using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace PageViewController.ViewControllers
{
	partial class ContentViewController : UIViewController
	{
		public int pageIndex = 0;
		public string titleText;
		public string imageFile;

		public ContentViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			imageView.Image = UIImage.FromBundle(imageFile);
			label.Text = titleText;
		}
	}
}
