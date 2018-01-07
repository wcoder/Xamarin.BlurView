using System.Collections.Generic;
using Android.App;
using Android.Content.PM;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Com.EightbitLab.BlurViewBinding;
//using Com.EightbitLab.SupportRenderScriptBlurBinding;

// Ported from:
// https://github.com/Dimezis/BlurView/blob/master/app/src/main/java/com/eightbitlab/blurview_sample/MainActivity.java
namespace SampleApp
{
    [Activity(
        MainLauncher = true,
        LaunchMode = LaunchMode.SingleTop,
        Label = "@string/app_name",
        Icon = "@drawable/icon",
        Theme = "@style/Theme.MyTheme")]
    public class MainActivity : AppCompatActivity
    {
        ViewGroup root;
        ViewPager viewPager;
        SeekBar radiusSeekBar;
        BlurView topBlurView;
        BlurView bottomBlurView;
        TabLayout tabLayout;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            root = FindViewById<ViewGroup>(Resource.Id.root);
            viewPager = FindViewById<ViewPager>(Resource.Id.viewPager);
            radiusSeekBar = FindViewById<SeekBar>(Resource.Id.radiusSeekBar);
            topBlurView = FindViewById<BlurView>(Resource.Id.topBlurView);
            bottomBlurView = FindViewById<BlurView>(Resource.Id.bottomBlurView);
            tabLayout = FindViewById<TabLayout>(Resource.Id.tabLayout);

            SetupBlurView();
            SetupViewPager();
        }
        
        void SetupBlurView()
        {
            float radius = 25f;
            float minBlurRadius = 10f;
            float step = 4f;

            //set background, if your root layout doesn't have one
            Drawable windowBackground = Window.DecorView.Background;
            
            var topViewSettings = topBlurView.SetupWith(root)
                .WindowBackground(windowBackground)
                .BlurAlgorithm(new RenderScriptBlur(this)) // SupportRenderScriptBlur
                .BlurRadius(radius);
            

            var bottomViewSettings = bottomBlurView.SetupWith(root)
                .WindowBackground(windowBackground)
                .BlurAlgorithm(new RenderScriptBlur(this)) // SupportRenderScriptBlur
                .BlurRadius(radius);

            int initialProgress = (int)(radius * step);
            radiusSeekBar.SetProgress(initialProgress, true);

            radiusSeekBar.ProgressChanged += (sender, args) =>
            {
                float blurRadius = args.Progress / step;
                blurRadius = Math.Max(blurRadius, minBlurRadius);
                topViewSettings.BlurRadius(blurRadius);
                bottomViewSettings.BlurRadius(blurRadius);
            };
        }

        void SetupViewPager()
        {
            viewPager.OffscreenPageLimit = 2;
            viewPager.Adapter = new ViewPagerAdapter(SupportFragmentManager);
            tabLayout.SetupWithViewPager(viewPager);
        }
    }

    class ViewPagerAdapter : FragmentPagerAdapter
    {
        List<BaseFragment> pages;

        public ViewPagerAdapter(Android.Support.V4.App.FragmentManager fragmentManager)
            : base (fragmentManager)
        {
            pages = new List<BaseFragment>
            {
                new ScrollFragment(),
                new ListFragment(),
                new ImageFragment()
            };
        }

        public override int Count => pages.Count;

        public override Android.Support.V4.App.Fragment GetItem(int position)
        {
            return pages[position];
        }

        public override ICharSequence GetPageTitleFormatted(int position)
        {
            return new Java.Lang.String(pages[position].Title);
        }
    }
}

