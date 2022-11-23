using Android.Content;
using Android.Views;
using AndroidX.RecyclerView.Widget;

namespace SampleApp
{
    public class ListFragment : BaseFragment
    {
        RecyclerView recyclerView;

        public override string Title => "RecyclerView";

        protected override int LayoutId => Resource.Layout.fragment_list;

        public override void OnViewCreated(View view, Android.OS.Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            Init();
        }

        private void Init()
        {
            recyclerView = View.FindViewById<RecyclerView>(Resource.Id.recyclerView);
            recyclerView?.SetAdapter(new ExampleListAdapter(Context));
            recyclerView?.SetLayoutManager(new LinearLayoutManager(Context));
        }
    }

    public class ExampleListAdapter : RecyclerView.Adapter
    {
        const int ITEMS_COUNT = 64;
        LayoutInflater inflater;

        public ExampleListAdapter(Context context)
        {
            inflater = LayoutInflater.From(context);
        }

        public override int ItemCount => ITEMS_COUNT;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            return new Holder(inflater.Inflate(Resource.Layout.list_item, parent, false));
        }

        class Holder : RecyclerView.ViewHolder
        {
            public Holder(View itemView) : base(itemView) { }
        }
    }
}
