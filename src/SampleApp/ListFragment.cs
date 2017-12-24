using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;

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

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Android.OS.Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            recyclerView = view.FindViewById<RecyclerView>(Resource.Id.recyclerView);

            return view;
        }

        void Init()
        {
            recyclerView.SetAdapter(new ExampleListAdapter(Context));
            recyclerView.SetLayoutManager(new LinearLayoutManager(Context));
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
            public Holder(View itemView) : base(itemView) {}
        }
    }
}
