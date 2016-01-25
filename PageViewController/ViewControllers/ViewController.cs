using System;

using UIKit;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using ImageIO;
using System.Linq;
using VideoToolbox;
using CoreGraphics;

namespace PageViewController.ViewControllers
{
	public partial class ViewController : UIViewController
	{ 
		private UIPageViewController pageViewController;
		private List<string> _pageTitles;
		private List<string> _images;  

		public ViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			_pageTitles = new List<string> {"Explore", "Widget", "Navigation", "Random"};
			_images = new List<string> {"page1", "page2", "page3", "page4"};

			pageViewController = this.Storyboard.InstantiateViewController ("PageViewController") as UIPageViewController;
			pageViewController.DataSource = new PageViewControllerDataSource (this, _pageTitles);

			var startVC = this.ViewControllerAtIndex (0) as ContentViewController;
			var viewControllers = new UIViewController[] {startVC};

			pageViewController.SetViewControllers (viewControllers, UIPageViewControllerNavigationDirection.Forward, false, null);
			pageViewController.View.Frame = new CGRect (0, 0, this.View.Frame.Width, this.View.Frame.Size.Height-50); 
			AddChildViewController (this.pageViewController);
			View.AddSubview (this.pageViewController.View);
			pageViewController.DidMoveToParentViewController (this);

			button.TouchUpInside += RestartTutorial;
		}


		private void RestartTutorial (object sender, EventArgs e){
			var startVC = this.ViewControllerAtIndex (0) as ContentViewController;
			var viewControllers = new UIViewController[] { startVC };
			this.pageViewController.SetViewControllers (viewControllers, UIPageViewControllerNavigationDirection.Forward, false, null);
		}

		public UIViewController ViewControllerAtIndex(int index){
			var vc = this.Storyboard.InstantiateViewController ("ContentViewController") as ContentViewController;
			vc.titleText = _pageTitles.ElementAt (index);
			vc.imageFile = _images.ElementAt (index);
			vc.pageIndex = index;
			return vc;
		}

		private class PageViewControllerDataSource : UIPageViewControllerDataSource{
			private ViewController _parentViewController;
			private List<string> _pageTitles;

			public PageViewControllerDataSource (UIViewController parentViewController, List<string> pageTitles)
			{
				_parentViewController = parentViewController as ViewController;
				_pageTitles = pageTitles;
			}

			public override UIViewController GetPreviousViewController (UIPageViewController pageViewController, UIViewController referenceViewController)
			{
				var vc = referenceViewController as ContentViewController;
				var index = vc.pageIndex;
				if (index == 0) {
					return null;
				} else {
					index--;
					return _parentViewController.ViewControllerAtIndex (index);
				}
			}

			public override UIViewController GetNextViewController (UIPageViewController pageViewController, UIViewController referenceViewController)
			{
				var vc = referenceViewController as ContentViewController;
				var index = vc.pageIndex;

				index++;
				if (index == _pageTitles.Count) {
					return null;
				} else {
					return _parentViewController.ViewControllerAtIndex (index);
				}
			}

			public override nint GetPresentationCount (UIPageViewController pageViewController)
			{
				return _pageTitles.Count;
			}

			public override nint GetPresentationIndex (UIPageViewController pageViewController)
			{
				return 0;
			}	
		}
	}
}

