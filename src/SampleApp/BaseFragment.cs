using Android.Support.V4.App;

namespace SampleApp
{
    public abstract class BaseFragment : Fragment
    {
        public abstract string Title { get; }
        protected abstract int LayoutId { get; }

        public override Android.Views.View OnCreateView(Android.Views.LayoutInflater inflater, Android.Views.ViewGroup container, Android.OS.Bundle savedInstanceState)
        {
            return inflater.Inflate(LayoutId, container, false);
        }
    }
}
