using System.Collections.Generic;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Com.EightbitLab.BlurViewBinding;
using AndroidX.ViewPager.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.Tabs;
using AndroidX.Fragment.App;
using Math = Java.Lang.Math;
using String = Java.Lang.String;

//using Com.EightbitLab.SupportRenderScriptBlurBinding;

// Ported from:
// https://github.com/Dimezis/BlurView/blob/b5f6f414ae16885acb709fe5a65d24df05c4c62a/app/src/main/java/com/eightbitlab/blurview_sample/MainActivity.java
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
        private ViewPager viewPager;
        private TabLayout tabLayout;
        private BlurView bottomBlurView;
        private BlurView topBlurView;
        private SeekBar radiusSeekBar;
        private ViewGroup root;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            InitView();
            SetupBlurView();
            SetupViewPager();
        }

        private void InitView()
        {
            viewPager = FindViewById<ViewPager>(Resource.Id.viewPager);
            tabLayout = FindViewById<TabLayout>(Resource.Id.tabLayout);
            bottomBlurView = FindViewById<BlurView>(Resource.Id.bottomBlurView);
            topBlurView = FindViewById<BlurView>(Resource.Id.topBlurView);
            radiusSeekBar = FindViewById<SeekBar>(Resource.Id.radiusSeekBar);
            root = FindViewById<ViewGroup>(Resource.Id.root);
        }

        private void SetupViewPager()
        {
            viewPager.OffscreenPageLimit = 2;
            viewPager.Adapter = new ViewPagerAdapter(SupportFragmentManager);
            tabLayout.SetupWithViewPager(viewPager);
        }

        void SetupBlurView()
        {
            const float radius = 25f;
            const float minBlurRadius = 4f;
            const float step = 4f;

            //set background, if your root layout doesn't have one
            var windowBackground = Window?.DecorView.Background;
            var algorithm = GetBlurAlgorithm();

            var topViewSettings = topBlurView.SetupWith(root, algorithm)
                .SetFrameClearDrawable(windowBackground)
                .SetBlurRadius(radius);

            var bottomViewSettings = bottomBlurView.SetupWith(root, algorithm)
                .SetFrameClearDrawable(windowBackground)
                .SetBlurRadius(radius);

            var initialProgress = (int) (radius * step);
            radiusSeekBar.Progress = initialProgress;

            radiusSeekBar.ProgressChanged += (sender, args) =>
            {
                var blurRadius = args.Progress / step;
                blurRadius = Math.Max(blurRadius, minBlurRadius);
                topViewSettings.SetBlurRadius(blurRadius);
                bottomViewSettings.SetBlurRadius(blurRadius);
            };
        }

        private IBlurAlgorithm GetBlurAlgorithm()
        {
            IBlurAlgorithm algorithm;
            if (Build.VERSION.SdkInt > BuildVersionCodes.R)
            {
                algorithm = new RenderEffectBlur();
            }
            else
            {
                algorithm = new RenderScriptBlur(this);
            }

            return algorithm;
        }
    }

    class ViewPagerAdapter : FragmentPagerAdapter
    {
        private readonly List<BaseFragment> pages;

        public ViewPagerAdapter(AndroidX.Fragment.App.FragmentManager fragmentManager)
            : base(fragmentManager, BehaviorResumeOnlyCurrentFragment)
        {
            pages = new List<BaseFragment>
            {
                new ScrollFragment(),
                new ListFragment(),
                new ImageFragment()
            };
        }

        public override int Count => pages.Count;

        public override AndroidX.Fragment.App.Fragment GetItem(int position)
        {
            return pages[position];
        }

        public override ICharSequence GetPageTitleFormatted(int position)
        {
            return new String(pages[position].Title);
        }
    }
}
